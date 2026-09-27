using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;

namespace Drifter.DrifterCode.Cards;

public class DrifterEnums
{
    [CustomEnum, KeywordProperties(AutoKeywordPosition.Before)] public static CardKeyword Blade;
    [CustomEnum, KeywordProperties(AutoKeywordPosition.Before)] public static CardKeyword Gun;
    [CustomEnum, KeywordProperties(AutoKeywordPosition.None)] public static CardKeyword Fabricate;
    [CustomEnum, KeywordProperties(AutoKeywordPosition.None)] public static CardKeyword Fabricated;
    [CustomEnum] public static StaticHoverTip Weaving;
    // [CustomEnum] public static StaticHoverTip Modular;
    // [CustomEnum] public static int BladeWeave;
    // [CustomEnum] public static int GunWeave;
}