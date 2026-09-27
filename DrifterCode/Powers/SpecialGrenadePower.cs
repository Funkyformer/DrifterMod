using BaseLib.Hooks;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.DynamicVars;
using Drifter.DrifterCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Powers;

public class SpecialGrenadePower() : DrifterPower, IAfterSpendResource<ChargeResource>
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;
    protected override object InitInternalData() => new Data();

    protected override IEnumerable<DynamicVar> CanonicalVars => [new StringVar("Card")];


    public void SetSelectedCard(CardModel card)
    {
        CardModel clone = card.CreateClone();
        CardCmd.ClearAffliction(clone);
        clone.SetToFreeThisCombat();
        GetInternalData<Data>().selectedCard = clone;
        ((StringVar)DynamicVars["Card"]).StringValue = clone.Title;
    }

    private class Data
    {
        public CardModel? selectedCard;
    }

    public async Task AfterSpendResource(ICombatState combatState, ChargeResource resource, AbstractModel? spender, int amount)
    {
        if (amount <= 0) return;
        if (amount >= Amount)
        {
            CardPileAddResult combat = await CardPileCmd.AddGeneratedCardToCombat(GetInternalData<Data>().selectedCard,
                PileType.Hand, Owner.Player);
            await PowerCmd.Remove(this);
            return;
        }
        await PowerCmd.ModifyAmount(new ThrowingPlayerChoiceContext(), this, -amount, Owner.Player.Creature, null);
        // for (int i = 0; i < amount; i++)
        // {
        //     await PowerCmd.Decrement(this);
        // }
    }
}