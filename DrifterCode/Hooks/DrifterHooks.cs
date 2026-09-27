using BaseLib.Hooks;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Relics;

namespace Drifter.DrifterCode.Hooks;

public static class DrifterHooks
{
    private static async Task Dispatch<T>(ICombatState combatState, PlayerChoiceContext choiceContext,
        Func<T, Task> action) where T : class
    {
        foreach (var model in combatState.IterateHookListeners().OfType<T>())
        {
            var abstractModel = (AbstractModel)(object)model;
            choiceContext.PushModel(abstractModel);
            await action(model);
            abstractModel.InvokeExecutionFinished();
            choiceContext.PopModel(abstractModel);
        }
    }

    public static Task AfterExplosiveRemoved(
        ICombatState combatState, 
        PlayerChoiceContext choiceContext, 
        Creature creature, 
        ExplosivePower power)
    {
        return Dispatch<IAfterExplosiveRemoved>(combatState, choiceContext, model => model.AfterExplosiveRemoved(choiceContext, creature, power));
    }

    public static Task AfterBarrageRepeat(
        ICombatState combatState,
        PlayerChoiceContext choiceContext,
        CardModel cardModel,
        int count)
    {
        return Dispatch<IAfterBarrageRepeat>(combatState, choiceContext,
            model => model.AfterBarrageRepeat(choiceContext, cardModel, count));
    }

    public static Task AfterFabricate(
        ICombatState combatState,
        PlayerChoiceContext choiceContext,
        Player creator,
        Player recipient,
        CardModel card)
    {
        return Dispatch<IAfterFabricate>(combatState, choiceContext, model => model.AfterFabricate(choiceContext, creator, recipient, card));
    }

    public static Task AfterWeave(
        ICombatState combatState,
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay,
        Player player,
        WeaveEnum type)
    {
        return Dispatch<IAfterWeave>(combatState, choiceContext, model => model.AfterWeave(choiceContext, cardPlay, player, type));
    }

}