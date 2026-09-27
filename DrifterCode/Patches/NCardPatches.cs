// using BaseLib.Utils.Patching;
// using Drifter.DrifterCode.Character;
// using Drifter.DrifterCode.Nodes.Resources;
// using HarmonyLib;
// using MegaCrit.Sts2.Core.Entities.Cards;
// using MegaCrit.Sts2.Core.Nodes.Cards;
//
// namespace Drifter.DrifterCode.Patches;
//
// [HarmonyPatch(typeof(NCard), nameof(NCard.UpdateVisuals))]
// internal class NCardUpdateVisualsPatch
// {
//     [HarmonyTranspiler]
//     private static List<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
//     {
//         return new InstructionPatcher(instructions).Match(new InstructionMatcher()
//             .ldarg_0()
//             .ldarg_1()
//             .call(typeof(NCard), nameof(NCard.UpdateVisuals), [typeof(PileType)])
//         ).Insert([
//             CodeInstruction.LoadArgument(0),
//             CodeInstruction.LoadArgument(1),
//             CodeInstruction.Call(typeof(NCardUpdateVisualsPatch), nameof(UpdateDrifterVisuals))
//         ]);
//     }
//
//     private static void UpdateDrifterVisuals(NCard instance, PileType pileType)
//     {
//         var chargeIcon = ChargeResource.NChargeIcon[instance];
//         chargeIcon.UpdateChargeCostVisuals(pileType);
//     }
// }