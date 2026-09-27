using BaseLib.Abstracts;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.CombatHistoryEntries;
using Drifter.DrifterCode.Extensions;
using Drifter.DrifterCode.Hooks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Utils;

public static class WeavingUtils
{
    public static async Task Add(PlayerChoiceContext choiceContext, CardPlay cardPlay, WeaveEnum weave)
    {
        if (cardPlay.IsWeave(weave))
        {
            CombatManager.Instance.History.Add(cardPlay.Card.CombatState,
                new WeavingEntry(
                    cardPlay.Player.Creature,
                    WeaveEnum.Blade,
                    cardPlay.Card.CombatState.RoundNumber,
                    cardPlay.Player.Creature.Side,
                    CombatManager.Instance.History,
                    cardPlay.Card.CombatState.Players));
            await DrifterHooks.AfterWeave(cardPlay.Card.CombatState, choiceContext, cardPlay, cardPlay.Player,
                weave);
        }
    }

}