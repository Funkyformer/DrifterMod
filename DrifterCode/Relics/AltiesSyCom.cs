using BaseLib.Abstracts;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.DynamicVars;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;


namespace Drifter.DrifterCode.Relics;

public class AltiesSyCom() : DrifterRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Starter;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new ChargeVar(4)];
    
    

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (!participants.Contains(Owner.Creature))
            return;
        await CustomResources<ChargeResource>.Get(Owner.PlayerCombatState).Spend<ChargeResource>(combatState, this, -(int)DynamicVars[ChargeVar.Key].BaseValue, true);
    }
}