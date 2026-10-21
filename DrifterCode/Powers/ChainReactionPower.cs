using BaseLib.Extensions;
using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Drifter.DrifterCode.Powers;

public class ChainReactionPower() : DrifterPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<ExplosivePower>(7)];

    public override async Task BeforeSideTurnEndEarly(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner) || side != Owner.Side)
        {
            return;
        }
        var enemy = Owner.Player.RunState.Rng.CombatTargets.NextItem(CombatState.HittableEnemies);
        for (var i = 0; i < Amount; i++)
        {
            if (i != 0) await Cmd.Wait(200);
            Flash();
            await PowerCmd.Apply<ExplosivePower>(choiceContext, enemy, DynamicVars.Power<ExplosivePower>().BaseValue,
                Owner, null);
        }
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ExplosivePower>()];
}