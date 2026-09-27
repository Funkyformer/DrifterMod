using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Hooks;
using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Drifter.DrifterCode.Powers;

public class FollowthroughPower() : DrifterPower, IAfterWeave
{
    private bool _bladeTrigger = false;
    private bool _gunTrigger = false;
    
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (!participants.Contains(Owner) || side != Owner.Side) return Task.CompletedTask;
        _bladeTrigger = false;
        _gunTrigger = false;
        return Task.CompletedTask;
    }

    public async Task AfterWeave(PlayerChoiceContext choiceContext, CardPlay cardPlay, Player player, WeaveEnum type)
    {
        if (player != Owner.Player) 
            return;
        if (type == WeaveEnum.Blade && !_bladeTrigger)
        {
            await PowerCmd.Apply<StrengthPower>(choiceContext, Owner, Amount, Owner, null);
            _bladeTrigger = true;
        }

        if (type == WeaveEnum.Gun && !_gunTrigger)
        {
            await PowerCmd.Apply<DexterityPower>(choiceContext, Owner, Amount, Owner, null);
            _gunTrigger = true;
        }
    }
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(DrifterEnums.Weaving)];
}