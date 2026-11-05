using Drifter.DrifterCode.DynamicVars;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Drifter.DrifterCode.Extensions;

public static class DynamicVarExtensions
{
    public static DynamicVar Fabricate(this DynamicVarSet dynamicVars)
    {
        return dynamicVars[FabricateVar.Key];
    }
    public static DynamicVar Judgment(this DynamicVarSet dynamicVars)
    {
        return dynamicVars[JudgmentVar.Key];
    }
    public static DynamicVar Modular(this DynamicVarSet dynamicVars)
    {
        return dynamicVars[ModularVar.Key];
    }
    public static DynamicVar Barrage(this DynamicVarSet dynamicVars)
    {
        return dynamicVars[BarrageVar.Key];
    }
}