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
    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (!cardPlay.Card.Keywords.Contains(DrifterEnums.Gun))
            return;
        await WeavingUtils.Add(choiceContext, cardPlay, WeaveEnum.Gun);
    }
}