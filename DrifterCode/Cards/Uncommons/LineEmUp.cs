using BaseLib.Abstracts;
using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Uncommons;

public class LineEmUp : DrifterCard
{
    public LineEmUp() : base(0,
        CardType.Attack, CardRarity.Uncommon,
        TargetType.AllEnemies)
    {
        CustomResources<ChargeResource>.SetCanonicalCost(this, 7);
    }

    protected override IEnumerable<DynamicVar> CanonicalVars => [new CalculationBaseVar(6), new ExtraDamageVar(2), new CalculatedDamageVar(ValueProp.Move).WithMultiplier(static (card, target) => card.CombatState.HittableEnemies.Count)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [DrifterEnums.Gun];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(2);
        DynamicVars.ExtraDamage.UpgradeValueBy(1);
    }
}