using Drifter.DrifterCode.DynamicVars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Uncommons;

public class SlamFire() : ModularCard(0,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy, 7, 2)
{
    protected override IEnumerable<DynamicVar> BonusVars => [new DamageVar(9, ValueProp.Move), new PowerVar<VulnerablePower>(2)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [DrifterEnums.Gun];

    protected override async Task ModularEffect(
        PlayerChoiceContext choiceContext,
        CardPlay play,
        int repeats,
        AttackContext? attackContext = null,
        IEnumerable<DamageResult>? damageResults = null)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "attack", 200);
        damageResults =
            await CreatureCmd.Damage(choiceContext, play.Target, DynamicVars.Damage, Owner.Creature, this, play);
        attackContext.AddHit(damageResults);
    }

    protected override async Task BarrageEffect(
        PlayerChoiceContext choiceContext,
        CardPlay play,
        int repeats,
        AttackContext? attackContext = null,
        IEnumerable<DamageResult>? damageResults = null)
    {
        await PowerCmd.Apply<VulnerablePower>(new BlockingPlayerChoiceContext(), play.Target,
            DynamicVars.Vulnerable.BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars[ModularVar.Key].UpgradeValueBy(-1);
    }
}