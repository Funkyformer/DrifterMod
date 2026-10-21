using BaseLib;
using BaseLib.Cards.Variables;
using Drifter.DrifterCode.CombatHistoryEntries;
using Drifter.DrifterCode.Powers;
using Drifter.DrifterCode.Rewards;
using Drifter.DrifterCode.Utils;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;

namespace Drifter.DrifterCode.Powers;

public class UnyieldingPower() : DrifterPower
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("HPLossBase", 0),
        new ("HPLossExtra", 1),
        new CustomCalculatedVar("HPLoss").WithMultiplier((power, owner) => JudgmentUtils.JudgmentLossThisCombat(power.Owner))
    ];

    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override Task AfterCombatEnd(CombatRoom room)
    {
        var amount = (int)Math.Min(((CustomCalculatedVar)DynamicVars["HPLoss"]).CalculateCustom(Owner), Amount);
        if (amount <= 0) return Task.CompletedTask;
        room.AddExtraReward(Owner.Player, new MaxHPReward(Owner.Player, amount));
        MainFile.Logger.Warn("Healing Occurred");
        return Task.CompletedTask;
    }
}