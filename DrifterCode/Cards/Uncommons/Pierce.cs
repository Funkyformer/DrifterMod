using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Cards.Statuses;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Commands;
using Drifter.DrifterCode.DynamicVars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Uncommons;

public class Pierce() : DrifterCard(1, CardType.Attack,
    CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromCard<WrackingCough>()];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new JudgmentVar(1), new DamageVar(15, ValueProp.Move)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await JudgmentCmd.Judgment(play.Card.CombatState, choiceContext, this, DynamicVars[JudgmentVar.Key].BaseValue, Owner);
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(4);
    }

}