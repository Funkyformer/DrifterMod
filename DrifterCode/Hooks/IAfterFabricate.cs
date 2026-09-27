using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Hooks;

public interface IAfterFabricate
{
    public Task AfterFabricate(PlayerChoiceContext choiceContext, Player creator, Player recipient, CardModel card);
}