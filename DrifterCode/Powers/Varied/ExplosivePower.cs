using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using Drifter.DrifterCode.Cards.Commons.Attacks;
using Drifter.DrifterCode.Hooks;
using Drifter.DrifterCode.Powers.Uncommons;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Powers.Varied;

public class ExplosivePower() : DrifterPower, IHasSecondAmount, IAfterVolatileApplied
{
    public override PowerType Type =>
        PowerType.Debuff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    private ICombatState _state;
    
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("DamageBase", 12),
        new ("DamageExtra", 1),
        new CustomCalculatedDamageVar("Damage", ValueProp.Unpowered).WithMultiplier((power, owner) => power.Applier.GetPowerAmount<VolatilePower>())
    ];

    public override Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        Applier = applier;
        _state = Owner.CombatState;
        return Task.CompletedTask;
    }

    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        if (target != Owner || dealer != Applier)
            return;

        switch (cardSource)
        {
            case Primer:
                var amount = cardSource.DynamicVars["ExplosiveRemoval"].BaseValue;
                await PowerCmd.ModifyAmount(choiceContext, this, -Math.Min(amount + 1, Amount), cardSource.Owner.Creature, cardSource);
                break;
            default:
                await PowerCmd.Decrement(this);
                break;
        }
    }

    public override async Task AfterRemoved(Creature oldOwner)
    {
        IReadOnlyList<Creature> targets = _state.HittableEnemies;
        IEnumerable<DamageResult> damageResults =
            await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), targets, CalculatedDamage,
                ValueProp.Unpowered, Applier, null, null);
        if (oldOwner.IsAlive)
        {
            await DrifterHooks.AfterExplosiveRemoved(oldOwner.CombatState, new ThrowingPlayerChoiceContext(), Owner, this);
        }
    }

    private decimal CalculatedDamage => ((CustomCalculatedDamageVar)DynamicVars["Damage"]).CalculateCustom(null);

    // private int CalculateTotalDamage => (int)DynamicVars.Damage.BaseValue + (Applier?.GetPowerAmount<VolatilePower>() ?? 0);
    public string GetSecondAmount() => CalculatedDamage.ToString();

    public Task AfterVolatileApplied(PlayerChoiceContext choiceContext, Creature applier)
    {
        if (applier == Applier)
            this.InvokeSecondAmountChanged();
        return Task.CompletedTask;
    }
}