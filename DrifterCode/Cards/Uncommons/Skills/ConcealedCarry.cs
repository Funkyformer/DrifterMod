using BaseLib.Extensions;
using Drifter.DrifterCode.Commands;
using Drifter.DrifterCode.DynamicVars;
using Drifter.DrifterCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Drifter.DrifterCode.Cards.Uncommons.Skills;

public class ConcealedCarry() : DrifterCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new FabricateVar(false), new PowerVar<VigorPower>(4)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<VigorPower>(choiceContext, play.Target, DynamicVars.Power<VigorPower>().BaseValue,
            Owner.Creature, this);
        await FabricateCmd.Fabricate(CombatState, choiceContext, Owner, Owner, DynamicVars.Fabricate());
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Fabricate().UpgradeValueBy(1);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<VigorPower>()];
}