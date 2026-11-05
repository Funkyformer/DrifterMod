using BaseLib.Abstracts;
using BaseLib.Extensions;
using Drifter.DrifterCode.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Drifter.DrifterCode.Powers.Rares;

public class AltieFormPower() : DrifterPower, IHasSecondAmount
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new("StrGranted", 0)];

    protected override object InitInternalData() => new Data();

    public override Task BeforeCardPlayed(CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature != Owner)
            return Task.CompletedTask;
        GetInternalData<Data>().amountsForPlayedCards.Add(cardPlay.Card, Amount);
        return Task.CompletedTask;
    }
    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner.Player || !GetInternalData<Data>().amountsForPlayedCards.Remove(cardPlay.Card, out var amt))
            return;
        Flash();
        await PowerCmd.Apply<StrengthPower>(choiceContext, Owner, amt, Owner, null, true);
        DynamicVars["StrGranted"].BaseValue += amt;
        this.InvokeSecondAmountChanged();
    }

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner))
            return;
        await PowerCmd.Apply<StrengthPower>(choiceContext, Owner, -DynamicVars["StrGranted"].BaseValue, Owner, null,
            true);
        DynamicVars["StrGranted"].BaseValue = 0;
        this.InvokeSecondAmountChanged();
    }

    public class Data
    {
        /// <summary>
        /// Keep track of the cards we've seen played and the power amount at the time they were played.
        /// This lets After Image avoid triggering on cards that started play before it was applied, and avoid gaining
        /// extra block on multiple plays of After Image.
        /// </summary>
        public readonly Dictionary<CardModel, int> amountsForPlayedCards = new ();
    }

    // public string GetSecondAmount() => (DynamicVars["StrGranted"].BaseValue - (Amount - 1)).ToString();
    public string GetSecondAmount() => DynamicVars["StrGranted"].BaseValue.ToString();
}