using BaseLib.Cards.Variables;
using Drifter.DrifterCode.Cards.Statuses;
using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Powers;

public class PossessionPower() : DrifterPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override int DisplayAmount => (int)((CustomCalculatedVar)DynamicVars["Judgment"]).CalculateCustom(null);


    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("JudgmentBase", 0), 
        new ("JudgmentExtra", 25), 
        new CustomCalculatedVar("Judgment").WithMultiplier((Func<PowerModel, Creature, Decimal>) 
            ((power, target) => 
                power.Owner.Player.PlayerCombatState.AllCards.Count(c => c is WrackingCough && c.Pile.Type != PileType.Exhaust)))];

    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer,
        CardModel? cardSource, CardPlay? cardPlay)
    {
        if (target == null || dealer != Owner || props == ValueProp.Unpowered || cardSource == null || cardSource.Type != CardType.Attack)
            return 1;
        return 1 + (((CustomCalculatedVar)DynamicVars["Judgment"]).CalculateCustom(null) / 100.0m);
    }

    public override Task AfterCardEnteredCombat(CardModel card)
    {
        if (card is WrackingCough && card.Owner == Owner.Player)
            InvokeDisplayAmountChanged();
        return Task.CompletedTask;
    }

    public override Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource)
    {
        DynamicVars["JudgmentExtra"].BaseValue = Amount;
        InvokeDisplayAmountChanged();
        return Task.CompletedTask;
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromCard<WrackingCough>()];
}