using Drifter.DrifterCode.DynamicVars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Uncommons.Attacks;

public class FranticShots() : ModularCard(0,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.RandomEnemy, 2, 4)
{
    protected override IEnumerable<DynamicVar> BonusVars => [new DamageVar(2, ValueProp.Move), new PowerVar<WeakPower>(1)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [DrifterEnums.Gun];

    protected override async Task ModularEffect(
        PlayerChoiceContext choiceContext,
        CardPlay play,
        int repeats,
        AttackContext? attackContext = null,
        IEnumerable<DamageResult>? damageResults = null)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "attack", 200);
        Creature enemy = Owner.RunState.Rng.CombatTargets.NextItem(CombatState.HittableEnemies);
        damageResults = await CreatureCmd.Damage(new BlockingPlayerChoiceContext(), enemy, DynamicVars.Damage,
            Owner.Creature, this, play);
        attackContext.AddHit(damageResults);
    }

    protected override async Task BarrageEffect(
        PlayerChoiceContext choiceContext,
        CardPlay play,
        int repeats,
        AttackContext? attackContext = null,
        IEnumerable<DamageResult>? damageResults = null)
    {
        foreach (Creature hittable in CombatState.HittableEnemies)
            await PowerCmd.Apply<WeakPower>(choiceContext, hittable, DynamicVars.Weak.BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars[BarrageVar.Key].UpgradeValueBy(-1);
    }
}