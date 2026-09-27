using Drifter.DrifterCode.Cards.Commons;
using Drifter.DrifterCode.Hooks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Powers;

public class ExplosivePower() : DrifterPower
{
    public override PowerType Type =>
        PowerType.Debuff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public ICombatState _state;
    
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(12, ValueProp.Unpowered)];

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
        {
            return;
        }

        if (cardSource is Primer)
        {
            var amount = cardSource.DynamicVars["ExplosiveRemoval"].BaseValue;
            await PowerCmd.ModifyAmount(choiceContext, this, -Math.Min(amount + 1, Amount), cardSource.Owner.Creature, cardSource);
        }
        else
        {
            await PowerCmd.Decrement(this);
        }
    }

    public override async Task AfterRemoved(Creature oldOwner)
    {
        IReadOnlyList<Creature> targets = _state.HittableEnemies;
        IEnumerable<DamageResult> damageResults =
            await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), targets, CalculateTotalDamage,
                ValueProp.Unpowered, Applier, null, null);
        if (oldOwner.IsAlive)
        {
            await DrifterHooks.AfterExplosiveRemoved(oldOwner.CombatState, new ThrowingPlayerChoiceContext(), Owner, this);
        }
    }

    public int CalculateTotalDamage => (int)DynamicVars.Damage.BaseValue + _state.PlayerCreatures.Where(c => c.IsAlive).Sum(a => a.GetPowerAmount<VolatilePower>());
}