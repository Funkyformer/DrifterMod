using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Drifter.DrifterCode.DynamicVars;

public class ChargeVar : DynamicVar
{
    public const string Key = "Charge";
    public ChargeVar(decimal chargeAmount) : base(Key, chargeAmount)
    {
        // this.WithTooltip();
    }
    
}