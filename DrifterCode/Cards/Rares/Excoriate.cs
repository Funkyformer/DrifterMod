using BaseLib.Cards.Variables;
using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Cards.Statuses;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.DynamicVars;
using Drifter.DrifterCode.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Rares;

public class Excoriate() : DrifterCard(2,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new JudgmentVar(1),
        new DamageVar(10, ValueProp.Move), 
        new ("HitsBase",1),
        new ("HitsExtra", 1),
        new CustomCalculatedVar("Hits").WithMultiplier(
            (Func<CardModel, Creature?, decimal>)((card, target) => 
                JudgmentUtils.JudgmentLossThisCombat(card.Owner.Creature)))
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [DrifterEnums.Blade];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromCard<WrackingCough>()];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play, (int)((CustomCalculatedVar)DynamicVars["Hits"]).CalculateCustom(play.Target)).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3);
    }
}