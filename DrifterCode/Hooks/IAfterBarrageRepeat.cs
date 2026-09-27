using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Hooks;

public interface IAfterBarrageRepeat
{
    public Task AfterBarrageRepeat(PlayerChoiceContext choiceContext, CardModel cardModel, int count);
}