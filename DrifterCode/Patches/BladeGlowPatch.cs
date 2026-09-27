using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using HarmonyLib;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Patches;

[HarmonyPatch(typeof(CardModel),"ShouldGlowGold", MethodType.Getter)]
class BladeGlowPatch
{
    [HarmonyPostfix]
    static void MakeGlowGold(CardModel __instance, ref bool __result)
    {
        if (DrifterSpireFields.Plinking.Get(__instance.Owner.PlayerCombatState))
        {
            if (__instance.Keywords.Contains(DrifterEnums.Blade))
            {
                __result = true;
            }
        }
    }
}