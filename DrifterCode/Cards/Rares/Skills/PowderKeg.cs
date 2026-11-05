using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Powers.Varied;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Drifter.DrifterCode.Cards.Rares.Skills;

public class PowderKeg() : DrifterCard(2,
    CardType.Skill, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("Explosive1", 11),
        new ("Explosive2", 9),
        new ("Explosive3", 7),
        new ("Explosive4", 5)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<ExplosivePower>(choiceContext, play.Target, DynamicVars["Explosive1"].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<ExplosivePower>(choiceContext, play.Target, DynamicVars["Explosive2"].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<ExplosivePower>(choiceContext, play.Target, DynamicVars["Explosive3"].BaseValue, Owner.Creature, this);
        if (IsUpgraded)
            await PowerCmd.Apply<ExplosivePower>(choiceContext, play.Target, DynamicVars["Explosive4"].BaseValue, Owner.Creature, this);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ExplosivePower>()];
}