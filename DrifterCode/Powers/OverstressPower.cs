using Drifter.DrifterCode.Extensions;
using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Powers;

public class OverstressPower() : DrifterPower
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new ("Replay", 1)];

    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    
    public override int ModifyCardPlayCount(CardModel card, Creature? target, int playCount)
    {
        return card.Owner.Creature != Owner || !card.IsGun() || card.BaseReplayCount != 0 ? playCount : playCount + 1;
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Player == Owner.Player && cardPlay.ValidGun() && cardPlay.Card.BaseReplayCount == 0)
        {
            CardCmd.ApplyKeyword(cardPlay.Card, CardKeyword.Ethereal);
            cardPlay.Card.BaseReplayCount += DynamicVars["Replay"].IntValue;
            await PowerCmd.Decrement(this);
        }
    }
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromKeyword(CardKeyword.Ethereal), HoverTipFactory.Static(StaticHoverTip.ReplayStatic)];
}