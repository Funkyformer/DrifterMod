using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Drifter.DrifterCode.DynamicVars;

public class BarrageVar : DynamicVar
{
    public const string Key = "Barrage";
    public BarrageVar(decimal barrageCount) : base(Key, barrageCount)
    {
        this.WithTooltip();
    }
}