using Drifter.DrifterCode.Cards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;

namespace Drifter.DrifterCode.CombatHistoryEntries;

public class WeavingEntry : CombatHistoryEntry
{
    public WeaveEnum WeaveType { get; }
    
    public override string Description => this.Actor.Name + " wove";

    public WeavingEntry(
        Creature caster,
        WeaveEnum weaveType,
        int roundNumber,
        CombatSide currentSide,
        CombatHistory history,
        IEnumerable<Player> players) 
        : base(caster, roundNumber, currentSide, history, players)
    {
        WeaveType = weaveType;
    }
}