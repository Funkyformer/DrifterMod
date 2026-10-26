using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;

namespace Drifter.DrifterCode.Cards;

public class DrifterEnums
{
    [CustomEnum, KeywordProperties(AutoKeywordPosition.Before)] public static CardKeyword Blade;
    [CustomEnum, KeywordProperties(AutoKeywordPosition.After)] public static CardKeyword SemiAuto;
    [CustomEnum, KeywordProperties(AutoKeywordPosition.Before)] public static CardKeyword Gun;
    [CustomEnum, KeywordProperties(AutoKeywordPosition.Before)] public static CardKeyword Fabricated;
    [CustomEnum] public static StaticHoverTip Weaving;
    [CustomEnum] public static StaticHoverTip ModularStatic;
}