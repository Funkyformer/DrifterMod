using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Commons;

public class Repurpose() : DrifterCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1), new BlockVar(5, ValueProp.Move), new BlockVar("SecondaryBlock", 9, ValueProp.Move)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, (int)DynamicVars.Cards.BaseValue);
        CardModel card = (await CardSelectCmd.FromHand(choiceContext, Owner, prefs, null, this)).FirstOrDefault();
        if (card == null)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
            return;
        }
        var isStatusOrCurse = card.Type is CardType.Curse or CardType.Status;
        CardPileAddResult? nullable = await CardCmd.Exhaust(choiceContext, card);
        await CreatureCmd.GainBlock(Owner.Creature, isStatusOrCurse ? (BlockVar)DynamicVars["SecondaryBlock"] : DynamicVars.Block, play);
        
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3);
        DynamicVars["SecondaryBlock"].UpgradeValueBy(4);
    }
}