using Drifter.DrifterCode.Powers.Varied;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Drifter.DrifterCode.Cards.Uncommons.Skills;

public class Bombard() : ModularCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.RandomEnemy, 6)
{
    protected override IEnumerable<DynamicVar> BonusVars => [
        new PowerVar<ExplosivePower>(6), 
        new ("Bonus", 3),
        new ("CurBonus", 0)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [];

    protected override async Task ModularEffect(
        PlayerChoiceContext choiceContext,
        CardPlay play,
        int repeats,
        AttackContext? attackContext = null,
        IEnumerable<DamageResult>? damageResults = null)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature,
            Owner.Character is Character.Drifter ? "SpecialGrenade" : "Cast", 400);
        Creature enemy = Owner.RunState.Rng.CombatTargets.NextItem(CombatState.HittableEnemies);
        await PowerCmd.Apply<ExplosivePower>(new BlockingPlayerChoiceContext(), enemy,
            DynamicVars["ExplosivePower"].BaseValue + DynamicVars["CurBonus"].BaseValue, Owner.Creature, this);
    }

    protected override Task BarrageEffect(
        PlayerChoiceContext choiceContext,
        CardPlay play,
        int repeats,
        AttackContext? attackContext = null,
        IEnumerable<DamageResult>? damageResults = null)
    {
        DynamicVars["CurBonus"].BaseValue += DynamicVars["Bonus"].BaseValue;
        return Task.CompletedTask;
    }

    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card == this)
            DynamicVars["CurBonus"].BaseValue = 0;
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Bonus"].UpgradeValueBy(-1);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ExplosivePower>()];
}