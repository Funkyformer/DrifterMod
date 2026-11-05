using Drifter.DrifterCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Creatures;

namespace Drifter.DrifterCode.Extensions;

public static class AttackCommandExtensions
{
    public static AttackCommand WithDrifterAnims(this AttackCommand cmd, string animName, float delay, Creature? visualAttacker = null)
    {
        return cmd.Attacker?.Player?.Character is not Character.Drifter ? cmd : cmd.WithAttackerAnim(animName, delay, visualAttacker);
    }
    public static AttackCommand WithDrifterAnims(this AttackCommand cmd, AnimHolder holder, Creature? visualAttacker = null)
    {
        return cmd.Attacker?.Player?.Character is not Character.Drifter ? cmd : cmd.WithAttackerAnim(holder.AnimName, holder.Delay, visualAttacker);
    }
}