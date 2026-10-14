using BaseLib.Abstracts;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Extensions;

public static class CardModelExtensions
{
    public static bool WillWeave(this CardModel card, WeaveEnum weave)
    {
        var history = CombatManager.Instance.History.CardPlaysFinished.Where(c =>
            c.CardPlay.Player == card.Owner && c.HappenedThisTurn(c.CardPlay.Card.CombatState));
        if (history.Count() <= 1) return false;
        var lastCard = history.Last().CardPlay;
        return (weave == WeaveEnum.Blade && card.IsBlade() && lastCard.ValidGun()) ||
               (weave == WeaveEnum.Gun && card.IsGun() && lastCard.ValidBlade());
    }
    
    public static bool IsBlade(this CardModel card)
    {
        return card.Keywords.Contains(DrifterEnums.Blade);
    }
    
    public static bool IsGun(this CardModel card)
    {
        return card.Keywords.Contains(DrifterEnums.Gun);
    }

    public static bool ShouldGlowGreen(this CardModel card)
    {
        return card.Keywords.Contains(DrifterEnums.Blade) && card.Owner.PlayerCombatState != null && DrifterSpireFields.Plinking.Get(card.Owner.PlayerCombatState);
    }
}