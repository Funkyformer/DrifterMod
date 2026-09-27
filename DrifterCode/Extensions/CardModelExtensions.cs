using BaseLib.Abstracts;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Extensions;

public static class CardModelExtensions
{
    public static bool IsWeave(this CardModel card, WeaveEnum weave)
    {
        var history = CombatManager.Instance.History.CardPlaysFinished.Where(c =>
            c.CardPlay.Player == card.Owner && c.HappenedThisTurn(c.CardPlay.Card.CombatState));
        if (history.Count() <= 1) return false;
        var lastCard = history.SkipLast(1).Last().CardPlay;
        return (weave == WeaveEnum.Blade && card.IsBlade() && lastCard.ValidGun(true)) ||
               (weave == WeaveEnum.Gun && card.IsGun(true) && lastCard.ValidBlade());
    }
    
    public static bool IsBlade(this CardModel card)
    {
        return card.Keywords.Contains(DrifterEnums.Blade);
    }
    
    public static bool IsGun(this CardModel card, bool requireSpent = false)
    {
        return card.Keywords.Contains(DrifterEnums.Gun) && 
               (!requireSpent || requireSpent && (!CustomResources<ChargeResource>.Cost(card).CostsX || (CustomResources<ChargeResource>.Get(card.Owner.PlayerCombatState).Amount) >= 1));
    }
}