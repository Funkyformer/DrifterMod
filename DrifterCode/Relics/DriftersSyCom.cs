using BaseLib.Abstracts;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.DynamicVars;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Relics;

public class DriftersSyCom() : DrifterRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Starter;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new ChargeVar(3)];

    public override RelicModel GetUpgradeReplacement() => ModelDb.Relic<AltiesSyCom>();


    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (!participants.Contains(Owner.Creature) || Owner.PlayerCombatState.TurnNumber > 1)
            return;
        await CustomResources<ChargeResource>.Get(Owner.PlayerCombatState).Spend<ChargeResource>(combatState, this, -(int)DynamicVars[ChargeVar.Key].BaseValue, true);
    }

    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        return Task.CompletedTask;
    }
}