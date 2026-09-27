using BaseLib.Hooks;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Powers;

public class LeaveNoOpeningPower() : DrifterPower, IAfterSpendResource<ChargeResource>
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;


    public async Task AfterSpendResource(ICombatState combatState, ChargeResource resource, AbstractModel? spender, int amount)
    {
        if (amount > 0)
            await CreatureCmd.GainBlock(Owner, new BlockVar(Amount, ValueProp.Unpowered), null, true);
    }
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(StaticHoverTip.Block)];
}