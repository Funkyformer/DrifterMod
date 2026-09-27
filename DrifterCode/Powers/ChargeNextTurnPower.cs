using BaseLib.Abstracts;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace Drifter.DrifterCode.Powers;

public class ChargeNextTurnPower() : DrifterPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterEnergyReset(Player player)
    {
        await CustomResources<ChargeResource>.Get(Owner.Player.PlayerCombatState).Spend<ChargeResource>(Owner.CombatState, this, -Amount, false);
        await PowerCmd.Remove(this);
    }
}