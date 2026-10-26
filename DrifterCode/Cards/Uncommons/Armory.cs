using BaseLib.Abstracts;
using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Commands;
using Drifter.DrifterCode.DynamicVars;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Drifter.DrifterCode.Cards.Uncommons;

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
            await CustomResources<ChargeResource>.Get(player.PlayerCombatState)
                .Spend<ChargeResource>(CombatState, card, -cost, false);
        }
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}