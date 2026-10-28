using BaseLib.Abstracts;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Commands;
using Drifter.DrifterCode.DynamicVars;
using Drifter.DrifterCode.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Drifter.DrifterCode.Cards.Uncommons.Skills;

public class Armory() : DrifterCard(2, CardType.Skill,
    CardRarity.Uncommon, TargetType.AllAllies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new FabricateVar(false), new ChargeVar(1)];

    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        foreach (var player in CombatState.Players)
        {
            var card = await FabricateCmd.Fabricate(CombatState, choiceContext, player, player, DynamicVars[FabricateVar.Key]);
            if (card is null) 
                return;
            var cost = CustomResources<ChargeResource>.Cost(card).GetWithModifiers(CostModifiers.None);
            await ChargeUtils.Gain(player, cost);
        }
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}