// using BaseLib.Abstracts;
// using HarmonyLib;
// using MegaCrit.Sts2.Core.Entities.Cards;
// using MegaCrit.Sts2.Core.Entities.Players;
// using MegaCrit.Sts2.Core.Models;
//
// namespace Drifter.DrifterCode.Patches;
//
// [HarmonyPatch(typeof(CustomResourceCost<>), nameof(CustomResourceCost<T>.GetAmountToSpend))]
// public class CustomResourceCostSpendPatch<T> where T : CustomResource, new()
// {
//     [HarmonyPostfix]
//     public int GetAmountToSpend()
//     {
//         if (!CostsX)
//             return Math.Max(0, this.GetWithModifiers(CostModifiers.All));
//         PlayerCombatState playerCombatState = this._card.Owner.PlayerCombatState;
//         return playerCombatState == null ? 0 : CustomResources<T>.Get(playerCombatState).Amount;
//     }
//     
// }