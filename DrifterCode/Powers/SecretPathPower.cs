using Drifter.DrifterCode.Cards.Tokens;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Drifter.DrifterCode.Powers;

public class SecretPathPower() : DrifterPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Single;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        await CardPileCmd.AddGeneratedCardToCombat(new SecretPath(), PileType.Hand, Owner.Player);
        await PowerCmd.Remove(this);
    }
}