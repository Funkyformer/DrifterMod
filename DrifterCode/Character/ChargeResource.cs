using BaseLib.Abstracts;
using BaseLib.BaseLibScenes;
using BaseLib.Patches.UI;
using BaseLib.Utils;
using Drifter.DrifterCode.Nodes.Resources;
using Drifter.DrifterCode.Singletons;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace Drifter.DrifterCode.Character;

public class ChargeResource() : CustomResource("Charge")
{
    // public static AddedNode<NCard, NChargeIcon> NChargeIcon = new(card =>
    //     {
    //         var chargeCounter = PreloadManager.Cache.GetScene("res://Drifter/scenes/resources/ChargeCost.tscn").Instantiate<NChargeIcon>().WithData(card);
    //         var cardContainer = card.GetChild(0)!;
    //         cardContainer.AddChild(chargeCounter);
    //         cardContainer.MoveChild(chargeCounter, cardContainer.GetNode("%StarIcon").GetIndex());
    //         return chargeCounter;
    //     }
    // );

    // public static AddedNode<NEnergyCounter, NChargeCounter> NChargeCounter = new(energy =>
    // {
    //     var chargeCounter = PreloadManager.Cache.GetScene("res://Drifter/scenes/resources/ChargeCounter.tscn").Instantiate<NChargeCounter>();
    //     energy.AddChild(chargeCounter);
    //     return chargeCounter;
    // });

    // public override ICustomCostVisualsHandler CostVisualsHandler()
    // {
    //     return new CostSingleton();
    // }
    //
    // public override ICustomResourceVisualsHandler ResourceVisualsHandler()
    // {
    //     return new CounterSingleton();
    // }

    public override bool ShouldShowDisplay() => Amount > 0 || Owner.Character is Character.Drifter;
    
    
    
    public override void RegisterResourceVisuals<T>()
    {
        ValidateType<T>();

        ExtraCombatUi.RegisterCombatUiElement(static (ui, player, combatState) =>
        {
            var playerCombatState = player.PlayerCombatState;
            if (playerCombatState == null) return null;
        
            if (CustomResources<T>.Get(playerCombatState) is not CustomResource resource) return null;
            
            var tex = PreloadManager.Cache.GetTexture2D("res://Drifter/images/charui/ammo256.png");
            var display = NAdditionalResourceDisplay.Create<T>(player, resource, tex);
            display.SetPosition(new Vector2(78, -38));
            ui.AddChild(display);
            
            return display;
        }, ExtraCombatUi.CombatUiPositioning.AroundEnergy);
        
        var getDisplay = ExtraCardUi.RegisterCreateCardUiElement(ExtraCardUi.CardUiPositioning.AroundCost, 
            _ => new NAdditionalCostDisplay(Id, "res://Drifter/images/charui/ammo256.png", MainColor),
            (card, model, display) =>
            {
                if (model == null) return false;
                var cost = CustomResources<T>.Cost(model);
                if (cost == null) return false;

                display.UpdateCostVisual(card, cost, PileType.None);
                return true;
            });

        CustomResources<T>.UpdateCostVisuals += (card, cost, pileType) =>
        {
            getDisplay(card).UpdateCostVisual(card, cost, pileType);
        };
    }
    
    // private void AddCounter()
    // {
    //     MainFile.Logger.Warn("checkc ombatstate");
    //     var playerCombatState = Owner.PlayerCombatState;
    //     if (playerCombatState == null) return;
    //     
    //     MainFile.Logger.Warn("check resource");
    //     if (CustomResources<ChargeResource>.Get(playerCombatState) is not CustomResource resource) return;
    //     
    //     var tex = PreloadManager.Cache.GetTexture2D("res://Drifter/images/charui/ammo256.png");
    //     
    //     MainFile.Logger.Warn("Right before AddedNode");
    //     AddedNode<NEnergyCounter, NAdditionalResourceDisplay> nChargeCounter = new (energy => 
    //     {
    //         var display = NAdditionalResourceDisplay.Create<ChargeResource>(Owner, resource, tex);
    //         // display.SetPosition(new Vector2(78, -38));
    //         energy.AddChild(display);
    //         return display;
    //     });
    //     
    // }
}