using BaseLib.Utils;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace Drifter.DrifterCode.Cards.Statuses;

[Pool(typeof(StatusCardPool))]
public class WrackingCough() : DrifterCard(-1,
    CardType.Status, CardRarity.Status,
    TargetType.None)
{
    public override int MaxUpgradeLevel => 0;
    public override bool HasTurnEndInHandEffect => true;
    public override bool CanBeGeneratedInCombat => false;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new MaxHpVar(1)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Unplayable];

    protected override async Task OnTurnEndInHand(PlayerChoiceContext choiceContext)
    {
        await CreatureCmd.LoseMaxHp(choiceContext, Owner.Creature, DynamicVars.MaxHp.BaseValue, true);
    }
}