using BaseLib.Extensions;
using Drifter.DrifterCode.Cards.Statuses;
using Drifter.DrifterCode.Commands;
using Drifter.DrifterCode.DynamicVars;
using Drifter.DrifterCode.Powers.Rares;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Drifter.DrifterCode.Cards.Rares.Powers;

public class Possession() : DrifterCard(2,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new JudgmentVar(3), new PowerVar<PossessionPower>(100)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await JudgmentCmd.Judgment(CombatState, choiceContext, this, DynamicVars[JudgmentVar.Key].BaseValue,
            Owner);
        
        await PowerCmd.Apply<PossessionPower>(choiceContext, Owner.Creature, DynamicVars.Power<PossessionPower>().BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<PossessionPower>().UpgradeValueBy(50);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromCard<WrackingCough>()];
}