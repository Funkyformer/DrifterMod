using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Cards.Ancients;

public class Overflow() : DrifterCard(1,
    CardType.Power, CardRarity.Ancient,
    TargetType.Self)
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<OverflowPower>(2)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<OverflowPower>(choiceContext, Owner.Creature, DynamicVars.Power<OverflowPower>().BaseValue,
            Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<OverflowPower>().UpgradeValueBy(1);
    }
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(DrifterEnums.Weaving)];
}