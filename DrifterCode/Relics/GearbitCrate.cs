using BaseLib.Utils;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.Relics;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace Drifter.DrifterCode.Relics;

[Pool(typeof(DrifterRelicPool))]
public class GearbitCrate() : DrifterRelic
{
    private int _roomsCompleted;

    private const string _roomsKey = "Rooms";
    
    public override RelicRarity Rarity =>
        RelicRarity.Uncommon;

    public override int DisplayAmount => this.RoomsCompleted % this.DynamicVars[_roomsKey].IntValue;

    public override bool ShowCounter => true;

    [SavedProperty]
    public int RoomsCompleted
    {
        get => this._roomsCompleted;
        set
        {
            AssertMutable();
            this._roomsCompleted = value;
            this.InvokeDisplayAmountChanged();
        }
    }

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar(_roomsKey, 4)];

    public override Task AfterRoomEntered(AbstractRoom room)
    {
        if (this.Owner.RunState.BaseRoom != room) return Task.CompletedTask;
        this.RoomsCompleted++;
        if (this.RoomsCompleted % this.DynamicVars[_roomsKey].IntValue == 0)
        {
            this.Flash();
            CardModel card = this.Owner.RunState.Rng.Niche.NextItem<CardModel>(PileType.Deck.GetPile(this.Owner).Cards.Where<CardModel>((Func<CardModel, bool>) (c => c.IsUpgradable)));
            if (card != null) CardCmd.Upgrade(card);
        }

        return Task.CompletedTask;
    }
}