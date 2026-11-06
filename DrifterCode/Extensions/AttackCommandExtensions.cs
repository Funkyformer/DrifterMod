using Drifter.DrifterCode.Character;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;

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

    public static AttackCommand WithDrifterVfx(this AttackCommand cmd, Func<Node2D?> createAttackerVfx, AnimHolder holder)
    {
        if (cmd.Attacker?.Player?.Character is not Character.Drifter)
            return cmd;
        return cmd.AfterAttackerAnim(async () =>
        {
            var vfxContainer = cmd.Attacker.GetVfxContainer();
            vfxContainer?.AddChildSafely(createAttackerVfx());
            await CreatureCmd.TriggerAnim(cmd.Attacker, holder.AnimName, holder.Delay);
        });
    }
}