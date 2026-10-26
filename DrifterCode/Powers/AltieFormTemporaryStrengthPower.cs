using BaseLib.Abstracts;
using Drifter.DrifterCode.Cards.Rares;
using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Drifter.DrifterCode.Powers;

public class AltieFormTemporaryStrengthPower : CustomTemporaryPowerModelWrapper<AltieForm, StrengthPower>
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    
}