using Drifter.DrifterCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Powers.Rares;

public class AltieFormPower() : DrifterPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new("WeaveStr", 0)];

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
        // amt *= _amount;
        if (cardPlay.IsWeave()) amt += (int)DynamicVars["WeaveStr"].BaseValue;
        await PowerCmd.Apply<AltieFormTemporaryStrengthPower>(choiceContext, Owner, amt, Owner, null);
    }

    public void IncreaseWeaveStr(decimal amt)
    {
        DynamicVars["WeaveStr"].BaseValue += amt;
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
}