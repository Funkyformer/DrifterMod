using BaseLib.Abstracts;
using BaseLib.Utils;
using Drifter.DrifterCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Commons;

public class ShotgunBlast : DrifterCard
{
    public ShotgunBlast() : base(0,
        CardType.Attack, CardRarity.Common,
        TargetType.AnyEnemy)
    {
        CustomResources<ChargeResource>.SetCanonicalCost(this, 6);
    }

    public override IEnumerable<CardKeyword> CanonicalKeywords => [DrifterEnums.Gun];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(8, ValueProp.Move)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        await Cmd.Wait(0.25f);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3);
    }
}