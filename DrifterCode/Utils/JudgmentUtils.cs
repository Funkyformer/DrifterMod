using Drifter.DrifterCode.CombatHistoryEntries;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;

namespace Drifter.DrifterCode.Utils;

public static class JudgmentUtils
{
    public static int JudgmentLossThisCombat(Creature actor) =>
        CombatManager.Instance.History.Entries.Count(e =>
            e is JudgmentEntry je
            && je.Actor == actor);
}