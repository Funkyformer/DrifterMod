using Drifter.DrifterCode.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Drifter.DrifterCode.Hooks;

public interface IAfterWeave
{
    public Task AfterWeave(PlayerChoiceContext choiceContext, CardPlay cardPlay, Player player, WeaveEnum type);
}