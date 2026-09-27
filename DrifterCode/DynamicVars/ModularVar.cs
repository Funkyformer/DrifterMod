using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Drifter.DrifterCode.DynamicVars;

public class ModularVar : DynamicVar
{
    public const string Key = "Modular";

    public ModularVar(decimal modularCost) : base(Key, modularCost)
    {
        this.WithTooltip();
    }

}