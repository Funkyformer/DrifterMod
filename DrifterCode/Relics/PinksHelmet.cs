using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.DynamicVars;
using Drifter.DrifterCode.Relics;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;


namespace Drifter.DrifterCode.Relics;

public class PinksHelmet() : DrifterRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Rare;

    // protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(DrifterEnums.ModularStatic)];
}