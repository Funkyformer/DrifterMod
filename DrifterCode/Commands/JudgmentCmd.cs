using Drifter.DrifterCode.Cards.Statuses;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Commands;

public class JudgmentCmd
{
    public static async Task Judgment(ICombatState combatState, PlayerChoiceContext choiceContext, CardModel source, Decimal amount,  Player player)
    {
        for (int i = 0; i < amount; i++)
        {
            var card = combatState.CreateCard<WrackingCough>(player);
            var res = await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Discard, player, CardPilePosition.Top);
            CardCmd.PreviewCardPileAdd(res, 0.5f);
        }
    }
}