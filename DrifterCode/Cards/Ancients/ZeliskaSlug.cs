using BaseLib.Abstracts;
using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Ancients;

public class ZeliskaSlug : DrifterCard
{
    public ZeliskaSlug() : base(1,
        CardType.Attack, CardRarity.Ancient,
        TargetType.AnyEnemy)
    {
        CustomResources<ChargeResource>.SetCanonicalCost(this, 3);
    }

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(5, ValueProp.Move)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [DrifterEnums.Gun];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
    }
}