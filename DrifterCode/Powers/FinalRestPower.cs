using BaseLib.Abstracts;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.DynamicVars;
using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Drifter.DrifterCode.Powers;

public class FinalRestPower() : DrifterPower
{
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(0), new ChargeVar(0), new CardsVar(0)];

    public override decimal ModifyMaxEnergy(Player player, decimal amount)
    {
        return player != Owner.Player ? amount : amount + DynamicVars.Energy.BaseValue;
    }

    public override decimal ModifyHandDraw(Player player, decimal count)
    {
        return player != Owner.Player ? count : count + DynamicVars.Cards.BaseValue;
    }

    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (!participants.Contains(Owner) || side != Owner.Side) return;

        await CustomResources<ChargeResource>.Get(Owner.Player.PlayerCombatState).Spend<ChargeResource>(combatState, this, (int)DynamicVars[ChargeVar.Key].BaseValue, false);
        
        await PowerCmd.Decrement(this);
    }

    public override async Task AfterRemoved(Creature oldOwner)
    {
        if (!oldOwner.IsAlive)
            return;
        Flash();
        await CreatureCmd.Kill(oldOwner);
    }

    public override bool ShouldTakeExtraTurn(Player player)
    {
        return player == Owner.Player && Amount == 2;
    }

    public void ModifyNumbers(decimal energy, decimal charge, decimal cards)
    {
        DynamicVars.Energy.BaseValue += energy;
        DynamicVars[ChargeVar.Key].BaseValue += charge;
        DynamicVars.Cards.BaseValue += cards;
    }
}