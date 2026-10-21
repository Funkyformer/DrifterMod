using BaseLib.Extensions;
using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Drifter.DrifterCode.Cards.Rares;

public class Unyielding() : DrifterCard(1,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<UnyieldingPower>(3)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<UnyieldingPower>(choiceContext, Owner.Creature,
            DynamicVars.Power<UnyieldingPower>().BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<UnyieldingPower>().UpgradeValueBy(2);
    }
}