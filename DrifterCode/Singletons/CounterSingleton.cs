// using BaseLib.Abstracts;
// using BaseLib.Hooks;
// using BaseLib.Patches.UI;
// using BaseLib.Utils;
// using Drifter.DrifterCode.Character;
// using Drifter.DrifterCode.Nodes.Resources;
// using Godot;
// using MegaCrit.Sts2.Core.Assets;
// using MegaCrit.Sts2.Core.Combat;
// using MegaCrit.Sts2.Core.Context;
// using MegaCrit.Sts2.Core.Entities.Players;
// using MegaCrit.Sts2.Core.Models;
// using MegaCrit.Sts2.Core.Nodes.Combat;
//
// namespace Drifter.DrifterCode.Singletons;
//
// public class CounterSingleton : ICustomResourceVisualsHandler
// {
//     // private static NChargeCounter _counter;
//     // public void AddDisplay(NCombatUi nCombatUi, PlayerCombatState playerCombatState)
//     // {
//     //     AddedNode<NEnergyCounter, NChargeCounter> NChargeCounter = new("res://Drifter/scenes/resources/ChargeCounter.tscn");
//     //     var energy = nCombatUi.GetChild(0).GetChild(0);
//     //     energy.AddChild(NChargeCounter);
//     // }
//     
//     // public static AddedNode<NCombatUi, NChargeCounter> nChargeCounter = new("res://Drifter/scenes/resources/ChargeCounter.tscn");
//     AddedNode<NEnergyCounter, NChargeCounter> nChargeCounter = new(energy =>
//     {
//         var chargeCounter= PreloadManager.Cache.GetScene("res://Drifter/scenes/resources/ChargeCounter.tscn").Instantiate<NChargeCounter>(); 
//         energy.AddChild(chargeCounter);
//         // _counter =  chargeCounter;
//         return chargeCounter;
//     });
//     
//     // currently not showing at all, but seems to be being spawned? odd.
//     public void AddDisplay(NCombatUi nCombatUi, PlayerCombatState playerCombatState)
//     {
//         nChargeCounter.Get((NEnergyCounter)nCombatUi.GetNode("EnergyCounterContainer").GetChild(0)).SetCombatState(playerCombatState);
//         // _counter.setCombatState(playerCombatState);
//     }
// }
