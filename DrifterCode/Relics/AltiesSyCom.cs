using BaseLib.Abstracts;
using Drifter.DrifterCode.Character;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;


namespace Drifter.DrifterCode.Relics;

public class AltiesSyCom() : DrifterRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Starter;

    
    

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (!participants.Contains(Owner.Creature) || Owner.PlayerCombatState.TurnNumber > 1)
            return;
        await CustomResources<ChargeResource>.Get(Owner.PlayerCombatState).Spend<ChargeResource>(combatState, this, -3, true);
    }
}