using Drifter.DrifterCode.Extensions;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Cards.Holders;

namespace Drifter.DrifterCode.Patches;

[HarmonyPatch(typeof(NHandCardHolder), nameof(NHandCardHolder.UpdateCard))]
public static class NHandCardHolderUpdateCardPatch
{
    [HarmonyPostfix]
    private static void Postfix(NHandCardHolder __instance)
    {
        var card = __instance.CardNode?.Model;
        if (card == null || !card.CanPlay() || card.ShouldGlowGold || card.ShouldGlowRed)
            return;

        if (card.ShouldGlowGreen())
            if (__instance.CardNode != null)
                __instance.CardNode.CardHighlight.Modulate = new Color(0.0f, 0.95f, 0.29f, 0.98f);
    }
}

[HarmonyPatch(typeof(NHandCardHolder), nameof(NHandCardHolder.Flash))]
public static class NHandCardHolderFlashPatch
{
    [HarmonyPostfix]
    private static void Postfix(NHandCardHolder __instance)
    {
        var card = __instance.CardNode?.Model;
        if (card == null || !card.CanPlay() || card.ShouldGlowGold || card.ShouldGlowRed)
            return;

        if (card.ShouldGlowGreen()) __instance._flash.Modulate = new Color(0.0f, 0.95f, 0.29f, 0.98f);
    }
}