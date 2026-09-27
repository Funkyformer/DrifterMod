using Drifter.DrifterCode.Powers;
using HarmonyLib;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Patches;

[HarmonyPatch(typeof(PowerModel), "AddDumbVariablesToDescription")]
internal class PowerModelAddDumbVariablesToDescriptionPatche
{
    [HarmonyPostfix]
    private static void Postfix(PowerModel __instance, LocString description)
    {
        if (__instance is DrifterPower) description.Add("charge", 0);
    }
}