using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Drifter.DrifterCode.DynamicVars;

public class FabricateVar : BoolVar
{
    public const string Key = "Fabricate";

    public FabricateVar(bool value) : base(Key, value)
    {
        this.WithTooltip();
    }
}