using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Drifter.DrifterCode.Cards.Tokens;

[Pool(typeof(TokenCardPool))]
public class SecretPath() : DrifterCard(0,
    CardType.Skill, CardRarity.Token,
    TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new GoldVar(25)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<RoyaltiesPower>(choiceContext, Owner.Creature, DynamicVars.Gold.BaseValue, Owner.Creature,
            this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Gold.UpgradeValueBy(5);
    }
}