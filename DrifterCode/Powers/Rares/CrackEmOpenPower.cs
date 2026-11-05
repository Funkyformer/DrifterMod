using Drifter.DrifterCode.Hooks;
using Drifter.DrifterCode.Powers;
using Drifter.DrifterCode.Powers.Varied;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace Drifter.DrifterCode.Powers.Rares;

public class CrackEmOpenPower() : DrifterPower, IAfterExplosiveRemoved
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;


    public async Task AfterExplosiveRemoved(PlayerChoiceContext choiceContext, Creature applier, Creature debuffed,
        ExplosivePower explosivePower)
    {
        if (applier != Owner || !debuffed.IsAlive)
            return;
        await PowerCmd.Apply<CrackEmOpenEnemyPower>(choiceContext, debuffed, Amount, Owner, null);
    }
    
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ExplosivePower>()];
}