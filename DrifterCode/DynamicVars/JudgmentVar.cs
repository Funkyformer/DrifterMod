using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Drifter.DrifterCode.DynamicVars;

public class JudgmentVar : DynamicVar
{
    public const string Key = "Judgment";
    public JudgmentVar(decimal judgmentCount) : base(Key, judgmentCount)
    {
        this.WithTooltip();
    }
}