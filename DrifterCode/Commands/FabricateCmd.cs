using BaseLib.Abstracts;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.CombatHistoryEntries;
using Drifter.DrifterCode.DynamicVars;
using Drifter.DrifterCode.Hooks;
using Drifter.DrifterCode.Relics;
using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Enchantments;

namespace Drifter.DrifterCode.Commands;

public static class FabricateCmd
{
    public static async Task Fabricate(ICombatState combatState, PlayerChoiceContext choiceContext, Player creator, Player recipient, FabricateVar? upgraded = null)
    {
        CardModel? card = CardFactory.GetDistinctForCombat(
            recipient, 
            ModelDb.CardPool<DrifterCardPool>().GetUnlockedCards(
                    recipient.UnlockState, 
                    recipient.RunState.CardMultiplayerConstraint).
                Where(c =>
                    c.Keywords.Contains(DrifterEnums.Gun) && c is not ModularCard),
                    1, 
                    recipient.RunState.Rng.CombatCardGeneration).FirstOrDefault();
        if (card != null)
        {
            CustomResources<ChargeResource>.SetToFreeThisTurn(card);
            CardCmd.ApplyKeyword(card, DrifterEnums.Fabricated);
            if (creator.Relics.Any(r => r is ModuleSet))
                CardCmd.Enchant<Glam>(card, 1);
            if (upgraded != null && upgraded.BoolVal) 
                CardCmd.Upgrade(card);
            await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, creator);
            await DrifterHooks.AfterFabricate(combatState, choiceContext, creator, recipient, card);
            CombatManager.Instance.History.Add(
                combatState, 
                new FabricateEntry(
                    creator.Creature, 
                    card, 
                    combatState.RoundNumber,
                    creator.Creature.Side, 
                    CombatManager.Instance.History, 
                    combatState.Players));
        }
    }
}