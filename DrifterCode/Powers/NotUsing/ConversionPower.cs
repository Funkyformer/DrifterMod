using BaseLib.Abstracts;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Hooks;
using Drifter.DrifterCode.Powers.Varied;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace Drifter.DrifterCode.Powers.NotUsing;

public class ConversionPower() : DrifterPower, IAfterExplosiveRemoved
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;


    public async Task AfterExplosiveRemoved(PlayerChoiceContext choiceContext, Creature creature, ExplosivePower explosivePower)
    {
        await CustomResources<ChargeResource>.Get(Owner.Player.PlayerCombatState).Spend<ChargeResource>(Owner.CombatState, this, -Amount, false);
    }
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ExplosivePower>()];
}