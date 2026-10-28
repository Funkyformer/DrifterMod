using Drifter.DrifterCode.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace Drifter.DrifterCode.Powers.Varied;

public class ChargeNextTurnPower() : DrifterPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterEnergyReset(Player player)
    {
        await ChargeUtils.Gain(Owner.Player, Amount, this);
        await PowerCmd.Remove(this);
    }
}