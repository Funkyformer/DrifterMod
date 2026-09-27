using BaseLib.Abstracts;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Extensions;
using Drifter.DrifterCode.Hooks;
using Drifter.DrifterCode.Utils;
using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Singletons;

public class BladeSingleton() : CustomSingletonModel(HookType.Combat)
{
    public override async Task AfterDamageGiven(
        PlayerChoiceContext choiceContext, 
        Creature? dealer, 
        DamageResult result, 
        ValueProp props,
        Creature target, 
        CardModel? cardSource)
    {
        // return base.AfterDamageGiven(choiceContext, dealer, result, props, target, cardSource);
        if (dealer is null || !dealer.IsPlayer || cardSource is null ||
            !cardSource.Keywords.Contains(DrifterEnums.Blade))
            return;
        await CustomResources<ChargeResource>.Get(dealer.Player.PlayerCombatState)
            .Spend<ChargeResource>(dealer.CombatState, cardSource, -2, false);
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await WeavingUtils.Add(choiceContext, cardPlay, WeaveEnum.Blade);
    }
}