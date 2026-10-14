using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.CombatHistoryEntries;

public class JudgmentEntry(
    Creature actor,
    int roundNumber,
    CombatSide currentSide,
    CombatHistory history,
    IEnumerable<Player> players)
    : CombatHistoryEntry(actor, roundNumber, currentSide, history, players)
{
    public override string Description => Actor.Name + " coughed";
}