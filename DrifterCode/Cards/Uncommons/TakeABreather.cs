using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.DynamicVars;
using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Uncommons;

public class TakeABreather() : DrifterCard(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(11, ValueProp.Move), new (nameof(TakeABreather), 1), new ChargeVar(2)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        var power = await PowerCmd.Apply<TakeABreatherPower>(choiceContext, Owner.Creature, DynamicVars[nameof(TakeABreather)].BaseValue, Owner.Creature, this);
        power.IncrementCharge(DynamicVars[ChargeVar.Key].BaseValue);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(4);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(StaticHoverTip.Block)];
}