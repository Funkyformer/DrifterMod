using BaseLib.Abstracts;
using Drifter.DrifterCode.Cards.Uncommons;
using Drifter.DrifterCode.Cards.Undecideds;
using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Drifter.DrifterCode.Powers;

public class StunlockPower() : CustomTemporaryPowerModelWrapper<Stunlock, StrengthPower>
{
    public override PowerType Type =>
        PowerType.Debuff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    protected override bool InvertInternalPowerAmount => true;
}