using Drifter.DrifterCode.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Drifter.DrifterCode.Powers.Uncommons;

public class TakeABreatherPower() : DrifterPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    private const string _chKey = "Charge";

    protected override IEnumerable<DynamicVar> CanonicalVars => [new (_chKey, 0)];

    public override decimal ModifyHandDraw(Player player, decimal count)
    {
        return player != Owner.Player ? count : count + Amount;
    }

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (!participants.Contains(Owner) || side != Owner.Side) return;

        await ChargeUtils.Gain(Owner.Player, DynamicVars[_chKey], this, true);
        Flash();
        await PowerCmd.Remove(this);
    }

    public void IncrementCharge(decimal count)
    {
        AssertMutable();
        DynamicVars[_chKey].BaseValue += count;
    }
}