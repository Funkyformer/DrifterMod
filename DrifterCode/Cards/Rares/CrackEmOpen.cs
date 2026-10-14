using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Hooks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Rares;

public class CrackEmOpen() : ModularCard(0,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy, 9)
{
    protected override IEnumerable<DynamicVar> BonusVars => [new DamageVar(8, ValueProp.Move), new ("BonusDamage", 8)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [DrifterEnums.Gun];

    protected override async Task ModularEffect(
        PlayerChoiceContext choiceContext,
        CardPlay play,
        int repeats,
        AttackContext? attackContext = null,
        IEnumerable<DamageResult>? damageResults = null)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "attack", 200);
        damageResults = await CreatureCmd.Damage(choiceContext, play.Target, DynamicVars.Damage.BaseValue + DynamicVars["BonusDamage"].BaseValue * repeats,
            ValueProp.Move, this, play);
        attackContext.AddHit(damageResults);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
        DynamicVars["BonusDamage"].UpgradeValueBy(2);
    }
}