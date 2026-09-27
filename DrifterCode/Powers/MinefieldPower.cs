using Drifter.DrifterCode.Hooks;
using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;

namespace Drifter.DrifterCode.Powers;

public class MinefieldPower() : DrifterPower, IAfterExplosiveRemoved
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new(nameof(MinefieldPower), 16)];

    public async Task AfterExplosiveRemoved(PlayerChoiceContext choiceContext, Creature creature, ExplosivePower explosivePower)
    {
        if (explosivePower.Applier != Owner) 
            return;
        for (int i = 0; i < Amount; i++)
            await PowerCmd.Apply<ExplosivePower>(choiceContext, creature, DynamicVars[nameof(MinefieldPower)].BaseValue, explosivePower.Applier, null);
    }
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ExplosivePower>()];
}