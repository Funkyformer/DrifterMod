using BaseLib.Abstracts;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Uncommons;

public class RailgunSnipe : DrifterCard
{
    public RailgunSnipe() : base(0,
        CardType.Attack, CardRarity.Uncommon,
        TargetType.AnyEnemy)
    {
        CustomResources<ChargeResource>.SetCanonicalCost(this,5);
    }
    //
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CalculationBaseVar(6), 
        new ("SecretPath",1), 
        new ("Divisor", 5), 
        new ExtraDamageVar(2), 
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier(static (card, target) => card.Owner.Creature.Block/card.DynamicVars["Divisor"].BaseValue)];
    // protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(8, ValueProp.Move), 
    //     new DynamicVar("SecretPath",1), 
    //     new DynamicVar("Divisor", 5), 
    //     new DamageVar("Bonus", 2, ValueProp.Move)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [DrifterEnums.Gun];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        AttackCommand attackCommand = await DamageCmd.Attack(DynamicVars.CalculatedDamage).FromCard(this, play)
            .Targeting(play.Target).Execute(choiceContext);
        // AttackCommand attackCommand = await DamageCmd.Attack(DynamicVars.Damage.BaseValue + Owner.Creature.Block / DynamicVars["Divisor"].BaseValue * DynamicVars["Bonus"].BaseValue).FromCard(this, play).Targeting(play.Target).Execute(choiceContext);
        if (Owner.RunState.Rng.CombatTargets.NextInt(100) == 0)
        {
            // await PowerCmd.Apply(choiceContext, new SecretPathPower(), Owner.Creature, 1, Owner.Creature, this, true);
            SecretPathPower? power = await PowerCmd.Apply<SecretPathPower>(choiceContext, Owner.Creature,
                DynamicVars["SecretPath"].BaseValue, Owner.Creature, this, true);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.ExtraDamage.UpgradeValueBy(1);
        // DynamicVars.CalculationBase.UpgradeValueBy(2);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(StaticHoverTip.Block)];
}