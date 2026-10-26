using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Hooks;

public interface IAfterModularRepeat
{
    public Task AfterModularRepeat(PlayerChoiceContext choiceContext, Player player, CardModel card, int count);
}