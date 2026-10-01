using BaseLib.Abstracts;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Hooks;

namespace Drifter.DrifterCode.Extensions;

public static class CardPlayExtensions
{
    public static bool IsWeave(this CardPlay cardPlay, WeaveEnum weave)
    {
        var history = CombatManager.Instance.History.CardPlaysFinished.Where(c =>
            c.CardPlay.Player == cardPlay.Player && c.HappenedThisTurn(c.CardPlay.Card.CombatState));
        if (history.Count() <= 1) return false;
        var lastCard = history.SkipLast(1).Last().CardPlay;
        return (weave == WeaveEnum.Blade && cardPlay.ValidBlade() && lastCard.ValidGun(true)) ||
               (weave == WeaveEnum.Gun && cardPlay.ValidGun(true) && lastCard.ValidBlade());
    }

    public static bool ValidBlade(this CardPlay card)
    {
        return card.Card.Keywords.Contains(DrifterEnums.Blade);
    }

    public static bool ValidGun(this CardPlay card, bool requireSpent = false)
    {
        return card.Card.Keywords.Contains(DrifterEnums.Gun) && (!requireSpent || requireSpent &&(CustomResources<ChargeResource>.Cost(card.Card) is null || !CustomResources<ChargeResource>.Cost(card.Card).CostsX || CustomResources<ChargeResource>.AmountSpent(card) >= 1));
    }
}