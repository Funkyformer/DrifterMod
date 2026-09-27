using Drifter.DrifterCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Enchantments;

namespace Drifter.DrifterCode.Relics;

public class ModuleSet() : DrifterRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Rare;

    public override async Task AfterCardGeneratedForCombat(CardModel card, Player? creator)
    {
        if (creator == null || creator.Creature != Owner.Creature || !card.Keywords.Contains(DrifterEnums.Gun))
            return;
        Flash();
        CardCmd.Enchant<Glam>(card, 1);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => HoverTipFactory.FromEnchantment<Glam>();
}