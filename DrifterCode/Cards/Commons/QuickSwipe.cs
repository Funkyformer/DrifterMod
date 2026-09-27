using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Hooks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Commons;

public class QuickSwipe() : DrifterCard(0,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy), IAfterWeave
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(5, ValueProp.Move), new CardsVar(1)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [DrifterEnums.Blade];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
    }

    public async Task AfterWeave(PlayerChoiceContext choiceContext, CardPlay play, Player player, WeaveEnum type)
    {
        if (play.Card == this)
        {
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, player);
        }
    }
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(DrifterEnums.Weaving)];
}