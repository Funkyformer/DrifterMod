using Drifter.DrifterCode.Formatters;
using HarmonyLib;
using MegaCrit.Sts2.Core.Localization;
using SmartFormat;

namespace Drifter.DrifterCode.Patches;

[HarmonyPatch(typeof(LocManager), "LoadLocFormatters")]
public class LocManagerLoadLocFormattersPatch
{
    [HarmonyPostfix]
    private static void AddCustomFormatters()
    {
        Smart.Default.AddExtensions(new ChargeIconFormatter());
    }    
}