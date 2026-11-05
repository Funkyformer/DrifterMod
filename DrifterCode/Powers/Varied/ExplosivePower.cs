using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using Drifter.DrifterCode.Cards.Commons.Attacks;
using Drifter.DrifterCode.Hooks;
using Drifter.DrifterCode.Nodes.Vfx;
using Drifter.DrifterCode.Powers.Uncommons;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Powers.Varied;

public class ExplosivePower() : DrifterPower, IHasSecondAmount
{
    public override PowerType Type =>
        PowerType.Debuff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    private ICombatState _state;

    private Vector2 _position;
    
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(12, ValueProp.Unpowered),
        new ("Display", 12)
    ];

    public override Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        Applier = applier;
        _state = Owner.CombatState;
        _position = NCombatRoom.Instance.GetCreatureNode(Owner).VfxSpawnPosition;
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
        var boom = NSpecialGrenadeVfx.Create(_position);
        NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(boom);
        IEnumerable<DamageResult> damageResults =
            await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), targets, DrifterHooks.ModifyExplosiveDamage(CombatState, DynamicVars.Damage.BaseValue, Applier),
                ValueProp.Unpowered, Applier, null, null);
            await DrifterHooks.AfterExplosiveRemoved(_state, new ThrowingPlayerChoiceContext(), Applier, Owner, this);
    }

    public string GetSecondAmount() => DynamicVars["Display"].ToString();

    public override Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource)
    {
        if (power is not VolatilePower || applier != Applier)
            return Task.CompletedTask;
        var newDam = DrifterHooks.ModifyExplosiveDamage(CombatState, DynamicVars.Damage.BaseValue, Applier);
        if (newDam == DynamicVars.Damage.BaseValue)
            return Task.CompletedTask;
        DynamicVars["Display"].BaseValue = newDam;
        this.InvokeSecondAmountChanged();
        return Task.CompletedTask;
    }
}