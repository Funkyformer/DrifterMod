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

public class AltieForm() : DrifterCard(3,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AltieFormPower>(1)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var power = await PowerCmd.Apply<AltieFormPower>(choiceContext, Owner.Creature, DynamicVars.Power<AltieFormPower>().BaseValue, Owner.Creature, this);
        if (IsUpgraded)
        {
            power.IncreaseWeaveStr(DynamicVars.Power<AltieFormPower>().BaseValue);
        }
    }

}