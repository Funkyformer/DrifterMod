using BaseLib.Abstracts;
using Drifter.DrifterCode.Character;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Enchantments;

public class HyperLight : CustomEnchantmentModel
{
    public override bool CanEnchant(CardModel card) => 
        CustomResources<ChargeResource>.CanonicalCost(card) <= 0 
        && card.EnergyCost.Canonical != 0;

    public override bool ShowAmount => false;

    protected override void OnEnchant()
    {
        Card.EnergyCost.SetCustomBaseCost(0);
        CustomResources<ChargeResource>.SetCanonicalCost(Card, 3 * Card.EnergyCost.Canonical);
    }
}