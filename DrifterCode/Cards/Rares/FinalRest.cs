using BaseLib.Extensions;
using Drifter.DrifterCode.DynamicVars;
using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Drifter.DrifterCode.Cards.Rares;

public class FinalRest() : DrifterCard(0,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<FinalRestPower>(2), new ChargeVar(4), new EnergyVar(2), new CardsVar(3)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        FinalRestPower power = await PowerCmd.Apply<FinalRestPower>(choiceContext, Owner.Creature,
            DynamicVars.Power<FinalRestPower>().BaseValue, Owner.Creature, this);
        power.ModifyNumbers(DynamicVars.Energy.BaseValue, DynamicVars[ChargeVar.Key].BaseValue, DynamicVars.Cards.BaseValue);
        await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
        await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
    
}