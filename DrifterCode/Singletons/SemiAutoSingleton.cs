using BaseLib.Abstracts;
using Drifter.DrifterCode.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Singletons;

public class SemiAutoSingleton() : CustomSingletonModel(HookType.Combat)
{
    public override CardLocation ModifyCardPlayResultLocation(CardModel card, bool isAutoPlay, ResourceInfo resources,
        CardLocation cardLocation)
    {
        if (cardLocation.pileType == PileType.Exhaust || !card.Keywords.Contains(DrifterEnums.SemiAuto))
        {
            return cardLocation;
        }
        return new CardLocation(card.Owner, PileType.Hand, CardPilePosition.Bottom);
    }
}