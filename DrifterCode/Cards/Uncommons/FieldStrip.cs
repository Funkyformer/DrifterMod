using BaseLib.Abstracts;
using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.DynamicVars;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Cards.Uncommons;

public class FieldStrip() : DrifterCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new ChargeVar(5)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CardPileCmd.Draw(choiceContext, Owner);
        CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1);
        CardModel card = (await CardSelectCmd.FromHand(choiceContext, Owner, prefs, null, this)).FirstOrDefault();
        if (card == null)
            return;
        CardPileAddResult? nullable = await CardCmd.Exhaust(choiceContext, card);
        await CustomResources<ChargeResource>.Get(Owner.PlayerCombatState)
            .Spend<ChargeResource>(CombatState, this, -DynamicVars[ChargeVar.Key].IntValue, false);
    }

    protected override void OnUpgrade()
    {
        DynamicVars[ChargeVar.Key].UpgradeValueBy(2);
    }
}