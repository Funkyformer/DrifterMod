using Drifter.DrifterCode.DynamicVars;
using Drifter.DrifterCode.Powers.Uncommons;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Uncommons.Attacks;

public class Stunlock() : ModularCard(0,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy, 3, 3)
{
    protected override IEnumerable<DynamicVar> BonusVars => [new DamageVar(2, ValueProp.Move), new DynamicVar("StrengthLoss", 1)];
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
        await PowerCmd.Apply<StunlockPower>(choiceContext, play.Target, DynamicVars["StrengthLoss"].BaseValue,
            Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars[ModularVar.Key].UpgradeValueBy(-1);
    }
}