using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Drifter.DrifterCode.Hooks;

public interface IAfterVolatileApplied
{
    public Task AfterVolatileApplied(PlayerChoiceContext choiceContext, Creature applier);
}