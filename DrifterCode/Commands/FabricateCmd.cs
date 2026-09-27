using BaseLib.Abstracts;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Hooks;
using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Commands;

public static class FabricateCmd
{
    public static async Task Fabricate(ICombatState combatState, PlayerChoiceContext choiceContext, Player creator, Player recipient)
    {
        CardModel card = CardFactory.GetDistinctForCombat(
            recipient, 
            ModelDb.CardPool<DrifterCardPool>().GetUnlockedCards(
                    recipient.UnlockState, 
                    recipient.RunState.CardMultiplayerConstraint).
                Where(
                    c => c.Keywords.Contains(DrifterEnums.Gun)), 
            1, recipient.RunState.Rng.CombatCardGeneration).FirstOrDefault();
        if (card != null)
        {
            CardCmd.ApplyKeyword(card, CardKeyword.Ethereal);
            CustomResources<ChargeResource>.SetToFreeThisTurn(card);
            // card.Tags.Append(DrifterCardTags.Fabricated);
            CardCmd.ApplyKeyword(card, DrifterEnums.Fabricated);
            await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, creator);
            await DrifterHooks.AfterFabricate(combatState, choiceContext, creator, recipient, card);
        }
    }
}