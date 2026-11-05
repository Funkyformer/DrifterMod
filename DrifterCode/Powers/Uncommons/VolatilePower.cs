using Drifter.DrifterCode.Hooks;
using Drifter.DrifterCode.Powers.Varied;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Powers.Uncommons;

public class VolatilePower() : DrifterPower, IModifyExplosiveDamage
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    protected override  IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ExplosivePower>()];
    
    public decimal ModifyExplosiveDamage(Decimal damage, Creature applier)
    {
        return applier == Owner ? Amount : 0;
    }
}