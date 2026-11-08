using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;

namespace Drifter.DrifterCode.Patches;


[HarmonyPatch(typeof(Cmd), "CustomScaledWait")]
internal static class CmdCustomScaledWaitPatches
{
    public static bool DoWaits = true;

    // [HarmonyPrefix]
    // private static bool Prefix(ref Task __result)
    // {
    //     if (!DoWaits)
    //     {
    //         __result = Task.CompletedTask;
    //     }
    //     return DoWaits;
    // }
    [HarmonyPrefix]
    private static void Prefix(ref float fastSeconds, ref float standardSeconds)
    {
        if (DoWaits)
            return;
        fastSeconds = 0.02f;
        standardSeconds = 0.2f;
    }
}