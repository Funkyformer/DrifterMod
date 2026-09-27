using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Drifter.DrifterCode.Hooks;

public interface IAfterExplosiveRemoved
{
    public Task AfterExplosiveRemoved(PlayerChoiceContext choiceContext, Creature creature, ExplosivePower explosivePower);
}