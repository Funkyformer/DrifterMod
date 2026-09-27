using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Players;

namespace Drifter.DrifterCode.Character;

public class DrifterSpireFields
{
    public static readonly SpireField<PlayerCombatState, bool> Plinking = new(() => false);
}