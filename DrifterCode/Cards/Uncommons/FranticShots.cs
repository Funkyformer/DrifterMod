using BaseLib.Abstracts;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.DynamicVars;
using Drifter.DrifterCode.Hooks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Uncommons;

public class FranticShots : DrifterCard
{
    public FranticShots() : base(0,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.RandomEnemy) 
    { 
        CustomResources<ChargeResource>.SetXCost(this);
    }

    public override IEnumerable<CardKeyword> CanonicalKeywords => [DrifterEnums.Gun];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(2, ValueProp.Move), new BarrageVar(4), new PowerVar<WeakPower>(1), new ModularVar(2)];

    protected override async Task OnPlay(
        PlayerChoiceContext context,
        CardPlay play)
    {
        AttackContext attackContext = await AttackCommand.CreateContextAsync(CombatState, context, play);
        IEnumerable<DamageResult> damageResults;
        var numRepeats = (int)(CustomResources<ChargeResource>.Cost(this).ResolveXValue() /
                               DynamicVars[ModularVar.Key].BaseValue);
        for (int i = 1; i <= numRepeats; i++)
        {
            await CreatureCmd.TriggerAnim(Owner.Creature, "attack", 200);
            Creature enemy = Owner.RunState.Rng.CombatTargets.NextItem(CombatState.HittableEnemies);
            damageResults = await CreatureCmd.Damage(context, enemy,
                DynamicVars.Damage, Owner.Creature, this, play);
            attackContext.AddHit(damageResults);
            if (i % DynamicVars[BarrageVar.Key].BaseValue == 0)
            {
                foreach (Creature hittable in CombatState.HittableEnemies)
                    await PowerCmd.Apply<WeakPower>(context, hittable, DynamicVars.Weak.BaseValue, Owner.Creature, this);
            }
            await DrifterHooks.AfterBarrageRepeat(CombatState, context, this, i);
        }

        if (attackContext != null) await attackContext.DisposeAsync();
        attackContext = null;
    }

    protected override void OnUpgrade()
    {
        DynamicVars[BarrageVar.Key].UpgradeValueBy(-1);
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<WeakPower>()];
}