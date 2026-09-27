using BaseLib.Extensions;
using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Drifter.DrifterCode.Cards.Uncommons;

public class Foundry() : DrifterCard(1,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<FoundryPower>(2)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<FoundryPower>(choiceContext, Owner.Creature, DynamicVars.Power<FoundryPower>().BaseValue,
            Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromKeyword(DrifterEnums.Fabricate)];
}