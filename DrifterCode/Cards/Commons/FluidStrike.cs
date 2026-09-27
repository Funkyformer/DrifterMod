using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Extensions;
using Drifter.DrifterCode.Hooks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Commons;

public class FluidStrike() : DrifterCard(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    private AttackContext _attackContext;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(8, ValueProp.Move)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [DrifterEnums.Blade];
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        _attackContext = await AttackCommand.CreateContextAsync(CombatState, choiceContext, play);
        var repeats = play.IsWeave(WeaveEnum.Blade) ? 2 : 1;
        while (repeats > 0)
        {
            await CreatureCmd.TriggerAnim(Owner.Creature, "attack", 200);
            var damageResults = await CreatureCmd.Damage(choiceContext, play.Target, DynamicVars.Damage.BaseValue,
                ValueProp.Move, Owner.Creature, this, play);
            _attackContext.AddHit(damageResults);
            repeats--;
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(DrifterEnums.Weaving)];
}