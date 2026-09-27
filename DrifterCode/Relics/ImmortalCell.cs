using BaseLib.Utils;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Enchantments;
using Drifter.DrifterCode.Relics;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;


namespace Drifter.DrifterCode.Relics;

public class ImmortalCell() : DrifterRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Shop;
    public override bool HasUponPickupEffect => true;

    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => HoverTipFactory.FromEnchantment<HyperLight>();

    public override async Task AfterObtained()
    {
        CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1);
        HyperLight canonicalMomentum = ModelDb.Enchantment<HyperLight>();
        foreach (CardModel card in await CardSelectCmd.FromDeckForEnchantment(this.Owner, (EnchantmentModel) canonicalMomentum, 1, prefs))
        {
            CardCmd.Enchant(canonicalMomentum.ToMutable(), card, 1M);
            CardCmd.Preview(card);
        }
        canonicalMomentum = null;
    }
}