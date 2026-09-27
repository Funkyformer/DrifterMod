using BaseLib.Abstracts;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Hooks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Drifter.DrifterCode.Powers;

public class OverflowPower() : DrifterPower, IAfterWeave
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public async Task AfterWeave(PlayerChoiceContext choiceContext, CardPlay cardPlay, Player player, WeaveEnum type)
    {
        await PowerCmd.Apply<ChargeNextTurnPower>(choiceContext, Owner, Amount, Owner, cardPlay.Card, true);
    }
}