using BaseLib.Abstracts;
using Drifter.DrifterCode.Cards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Drifter.DrifterCode.Singletons;

public class FabricatedSingleton() : CustomSingletonModel(HookType.Combat)
{
    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side != CombatSide.Player)
            return;
        foreach (var creature in participants.Where(c => c.Player is not null))
        {
            foreach (var card in creature.Player.PlayerCombatState.AllCards.Where(c => c.Pile.Type != PileType.Exhaust && c.Keywords.Contains(DrifterEnums.Fabricated)).ToList())
            {
                await CardCmd.Exhaust(choiceContext, card);
            }
        }
    }
}