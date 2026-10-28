using BaseLib.Utils;
using Drifter.DrifterCode.Powers.Varied;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Rares.Attacks;

public class Detonator() : DrifterCard(1,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(7, ValueProp.Move)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [DrifterEnums.Blade];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        // var powers = play.Target.Powers.Where(p => p.Applier == Owner.Creature && p.);
        var powers = play.Target.GetPowerInstances<ExplosivePower>();
        if (IsUpgraded)
        {
            await PowerCmd.Remove(powers.MaxBy(p => p.Amount));
            return;
        }
        await PowerCmd.Remove(powers.MinBy(p => p.Amount));
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ExplosivePower>()];
}