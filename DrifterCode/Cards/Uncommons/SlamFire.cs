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

public class SlamFire : DrifterCard
{

    public SlamFire() : base(0, CardType.Attack,
        CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        // CustomResources<ChargeResource>
        //     .SetCanonicalCost(this, int.MinValue);
        CustomResources<ChargeResource>.SetXCost(this);
    }

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new BarrageVar(2), new PowerVar<VulnerablePower>(2), new DamageVar(9, ValueProp.Move), new ModularVar(7)];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [DrifterEnums.Gun];

    protected override async Task OnPlay(
        PlayerChoiceContext context,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        AttackContext attackContext =
            await AttackCommand.CreateContextAsync(CombatState, context, play);
        IEnumerable<DamageResult> damageResults;
        var numRepeats = (int)(CustomResources<ChargeResource>.Cost(this).ResolveXValue() /
                         DynamicVars[ModularVar.Key].BaseValue);
        for (int i = 1; i <= numRepeats; i++)
        {
            await CreatureCmd.TriggerAnim(Owner.Creature, "attack", 200);
            damageResults = await CreatureCmd.Damage(context, play.Target, DynamicVars.Damage, Owner.Creature, this, play);
            attackContext.AddHit(damageResults);
            if (i % DynamicVars[BarrageVar.Key].BaseValue == 0)
            {
                await PowerCmd.Apply<VulnerablePower>(context, play.Target, DynamicVars.Vulnerable.BaseValue, Owner.Creature, this);
            }
            await DrifterHooks.AfterBarrageRepeat(CombatState, context, this, i);
        } 
        if (attackContext != null) await attackContext.DisposeAsync();
        attackContext = null;
    }

    protected override void OnUpgrade()
    {
        DynamicVars[ModularVar.Key].UpgradeValueBy(-1);
        CustomResources<ChargeResource>.Cost(this)!.UpgradeCostBy(-1);
    }
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<VulnerablePower>()];
}