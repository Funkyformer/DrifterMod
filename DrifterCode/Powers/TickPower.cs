using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Powers;

public class TickPower() : DrifterPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(1, ValueProp.Unpowered)];

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner) || side != Owner.Side) return;
        Flash();
        for (int i = 0; i < Amount; i++)
        {
            foreach (Creature hittableEnemy in CombatState.HittableEnemies.Where(
                c => c.GetPowerInstances<ExplosivePower>().Any(p => p.Applier == Owner)))
            {
                IEnumerable<DamageResult> damage = await CreatureCmd.Damage(choiceContext, hittableEnemy, DynamicVars.Damage, Owner,
                    null, null);
            }
        }
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ExplosivePower>()];
}