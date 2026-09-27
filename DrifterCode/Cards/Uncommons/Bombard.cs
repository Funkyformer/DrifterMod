using BaseLib.Abstracts;
using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.DynamicVars;
using Drifter.DrifterCode.Hooks;
using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Drifter.DrifterCode.Cards.Uncommons;

public class Bombard : DrifterCard
{

    public Bombard() : base(0, CardType.Skill,
        CardRarity.Uncommon, TargetType.RandomEnemy)
    {
        CustomResources<ChargeResource>.SetXCost(this);
    }

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ModularVar(6), 
        new BarrageVar(1),
        new PowerVar<ExplosivePower>(6), 
        new ("Bonus", 3)
    ];


    protected override async Task OnPlay(
        PlayerChoiceContext context,
        CardPlay play)
    {
        var numRepeats = (int)(CustomResources<ChargeResource>.Cost(this).ResolveXValue() /
                               DynamicVars[ModularVar.Key].BaseValue);
        decimal bonus = 0;
        for (int i = 1; i <= numRepeats; i++)
        {
            await CreatureCmd.TriggerAnim(Owner.Creature, Owner.Character is Character.Drifter ? "SpecialGrenade" : "Cast" , 400);


            Creature enemy = Owner.RunState.Rng.CombatTargets.NextItem(CombatState.HittableEnemies);

            await PowerCmd.Apply<ExplosivePower>(context, enemy, DynamicVars["ExplosivePower"].BaseValue + bonus, Owner.Creature, this);
            if (i % DynamicVars[BarrageVar.Key].BaseValue == 0)
            {
                bonus += DynamicVars["Bonus"].BaseValue;
            }

            await DrifterHooks.AfterBarrageRepeat(CombatState, context, this, i);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars[BarrageVar.Key].UpgradeValueBy(1);
    }
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ExplosivePower>()];
}