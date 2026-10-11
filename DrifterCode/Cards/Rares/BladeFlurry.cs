using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Rares;

public class BladeFlurry() : DrifterCard(2,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CalculationBaseVar(6),
        new ExtraDamageVar(2),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier(
            (card, target) => Math.Min(CombatManager.Instance.History.CardPlaysFinished.Count(
                hist => 
                    hist.HappenedThisTurn(card.CombatState) 
                    && hist.CardPlay.Card == card),2))
    ];

    public override CardLocation ModifyCardPlayResultLocation(CardModel card, bool isAutoPlay, ResourceInfo resources,
        CardLocation cardLocation)
    {
        return (card == this && cardLocation.pileType != PileType.Exhaust && PlaysThisTurn(this) < 2)
            ? new CardLocation(card.Owner, PileType.Hand, CardPilePosition.Top)
            : cardLocation;
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay.Target).Execute(choiceContext);
        EnergyCost.SetThisTurn(0);
        if (PlaysThisTurn(this) >= 2)
            EnergyCost.SetThisTurn(EnergyCost.Canonical);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.ExtraDamage.UpgradeValueBy(1);
    }

    private int PlaysThisTurn(CardModel card) => CombatManager.Instance.History.CardPlaysFinished.Count(hist =>
        hist.HappenedThisTurn(card.CombatState) && hist.CardPlay.Card == this);
}