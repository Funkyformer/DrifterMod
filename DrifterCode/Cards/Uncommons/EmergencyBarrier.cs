using BaseLib.Abstracts;
using Drifter.DrifterCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Uncommons;

public class EmergencyBarrier() : DrifterCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CalculationBaseVar(0), new CalculationExtraVar(1), new CalculatedBlockVar(ValueProp.Move).WithMultiplier(static (card, target) => CustomResources<ChargeResource>.Get(card.Owner.PlayerCombatState).Amount)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.CalculatedBlock.Calculate(play.Target), ValueProp.Move, play);
        await CustomResources<ChargeResource>
                .Get(Owner.PlayerCombatState)
                .Spend<ChargeResource>(CombatState, this, CustomResources<ChargeResource>.Get(Owner.PlayerCombatState).Amount, false);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationExtra.UpgradeValueBy(1);
    }
}