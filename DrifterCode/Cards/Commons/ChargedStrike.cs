using BaseLib.Abstracts;
using BaseLib.Utils;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.DynamicVars;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Drifter.DrifterCode.Cards.Commons;


public class ChargedStrike() : DrifterCard(2,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];
    public override HashSet<CardKeyword> CanonicalKeywords => [DrifterEnums.Blade];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(16, ValueProp.Move), new ChargeVar(4)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        await CustomResources<ChargeResource>
            .Get(Owner.PlayerCombatState!)
            .Spend<ChargeResource>(CombatState!, this, -(int)DynamicVars[ChargeVar.Key].BaseValue, false);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(6);
    }
}