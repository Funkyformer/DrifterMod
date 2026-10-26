using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Hooks;
using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Powers;

public class CoveringFirePower() : DrifterPower, IAfterModularRepeat
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Player.Creature != Applier || !cardPlay.Card.Keywords.Contains(DrifterEnums.Gun))
            return;
        await CreatureCmd.GainBlock(Owner, Amount, ValueProp.Unpowered, null);
    }

    public async Task AfterModularRepeat(PlayerChoiceContext choiceContext, Player player, CardModel card, int count)
    {
        if (player.Creature != Applier)
            return;
        await CreatureCmd.GainBlock(Owner, Amount, ValueProp.Unpowered, null);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(StaticHoverTip.Block), HoverTipFactory.FromKeyword(DrifterEnums.Gun), HoverTipFactory.Static(DrifterEnums.ModularStatic)];
}