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
            var rng = player.RunState.Rng.Shuffle.NextInt(3);
            PileType pile;
            CardPilePosition pos;
            switch (rng)
            {
                case 0:
                    pile = PileType.Hand;
                    pos = CardPilePosition.Bottom;
                    break;
                case 1:
                    pile = PileType.Discard;
                    pos = CardPilePosition.Random;
                    break;
                case 2:
                    pile = PileType.Draw;
                    pos = CardPilePosition.Random;
                    break;
                default:
                    pile = PileType.Hand;
                    pos = CardPilePosition.Bottom;
                    break;
            }

            var card = source.CombatState.CreateCard<WrackingCough>(player);
            var res = await CardPileCmd.AddGeneratedCardToCombat(card, pile, player, pos);
            CardCmd.PreviewCardPileAdd(res, 0.5f);
        }
    }
}