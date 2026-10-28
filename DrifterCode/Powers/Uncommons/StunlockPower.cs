using BaseLib.Abstracts;
using Drifter.DrifterCode.Cards.Uncommons.Attacks;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Drifter.DrifterCode.Powers.Uncommons;

public class StunlockPower() : CustomTemporaryPowerModelWrapper<Stunlock, StrengthPower>
{
    public override PowerType Type =>
        PowerType.Debuff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    protected override bool InvertInternalPowerAmount => true;
}