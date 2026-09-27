using BaseLib.Abstracts;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.CombatHistoryEntries;
using Drifter.DrifterCode.Extensions;
using Drifter.DrifterCode.Hooks;
using Drifter.DrifterCode.Utils;
using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Singletons;

public class GunSingleton() : CustomSingletonModel(HookType.Combat)
{
    public override CardLocation ModifyCardPlayResultLocation(CardModel card, bool isAutoPlay, ResourceInfo resources,
        CardLocation cardLocation)
    {
        if (cardLocation.pileType == PileType.Exhaust || !card.Keywords.Contains(DrifterEnums.Gun))
        {
            return cardLocation;
        }
        return new CardLocation(card.Owner, PileType.Hand, CardPilePosition.Bottom);
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await WeavingUtils.Add(choiceContext, cardPlay, WeaveEnum.Gun);
    }
}