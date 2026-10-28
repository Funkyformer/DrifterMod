using Drifter.DrifterCode.Cards.Statuses;
using Drifter.DrifterCode.DynamicVars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Rares.Attacks;

public class AnnihilationBeam() : ModularCard(0,
    CardType.Attack, CardRarity.Rare,
    TargetType.AllEnemies, 3)
{
    protected override IEnumerable<DynamicVar> BonusVars => [new JudgmentVar(2), new DamageVar(4, ValueProp.Move)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [DrifterEnums.Gun];

    protected override async Task ModularEffect(
        PlayerChoiceContext choiceContext,
        CardPlay play,
        int repeats,
        AttackContext? attackContext = null,
        IEnumerable<DamageResult>? damageResults = null)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "attack", 200);
        damageResults = await CreatureCmd.Damage(choiceContext, CombatState.HittableEnemies, DynamicVars.Damage,
            Owner.Creature, this, play);
        attackContext.AddHit(damageResults);
    }

    protected override void OnUpgrade()
    {
        DynamicVars[ModularVar.Key].UpgradeValueBy(-1);
    }

    // protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(StaticHoverTip.Stun)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromCard<WrackingCough>()];
}