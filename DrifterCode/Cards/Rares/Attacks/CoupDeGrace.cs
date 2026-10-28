using BaseLib.Utils;
using Drifter.DrifterCode.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Drifter.DrifterCode.Cards.Rares.Attacks;

public class CoupDeGrace() : DrifterCard(2,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [..MakeCalculatedDamage(4, (card, target) => WeavingUtils.WeavesThisCombat(card.Owner.Creature), 2)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [DrifterEnums.Blade];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play, play.Target, (CalculatedDamageVar)DynamicVars["CalculatedDamage"]).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["ExtraDamage"].UpgradeValueBy(1);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(DrifterEnums.Weaving)];
}