using Drifter.DrifterCode.Powers.Varied;
using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Patches;

[HarmonyPatch(typeof(PowerCmd), "ModifyAmount")]
internal static class PowerCmdModifyAmountPatches
{
    [HarmonyPrefix]
    private static void Prefix(PowerModel power)
    {
        CmdCustomScaledWaitPatches.DoWaits = power is not ExplosivePower;
    }
    [HarmonyPostfix]
    private static void Postfix()
    {
        CmdCustomScaledWaitPatches.DoWaits = true;
    }
}

[HarmonyPatch(typeof(PowerCmd), "Remove", new Type[] {typeof(PowerModel)})]
internal static class PowerCmdRemovePatches
{
    [HarmonyPrefix]
    private static void Prefix(PowerModel? power)
    {
        CmdCustomScaledWaitPatches.DoWaits = power is not ExplosivePower;
    }
    [HarmonyPostfix]
    private static void Postfix()
    {
        CmdCustomScaledWaitPatches.DoWaits = true;
    }
}