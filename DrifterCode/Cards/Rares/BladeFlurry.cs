// using BaseLib.Utils;
// using Drifter.DrifterCode.Cards;
// using Drifter.DrifterCode.Character;
// using MegaCrit.Sts2.Core.Combat;
// using MegaCrit.Sts2.Core.Entities.Cards;
// using MegaCrit.Sts2.Core.GameActions.Multiplayer;
// using MegaCrit.Sts2.Core.Localization.DynamicVars;
// using MegaCrit.Sts2.Core.ValueProps;
//
// namespace Drifter.DrifterCode.Cards.Rares;
//
// public class BladeFlurry() : DrifterCard(2,
//     CardType.Attack, CardRarity.Rare,
//     TargetType.AnyEnemy)
// {
//     protected override IEnumerable<DynamicVar> CanonicalVars => [
//         new CalculationBaseVar(6),
//         new ExtraDamageVar(2),
//         new CalculatedDamageVar(ValueProp.Move).WithMultiplier(Math.Min(((card, target) => CombatManager.Instance.History.CardPlaysFinished.Count(play => play.CardPlay.Card == this)),2))];
//
//     protected override async Task OnPlay(
//         PlayerChoiceContext choiceContext,
//         CardPlay play)
//     {
//         await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
//     }
//
//     protected override void OnUpgrade()
//     {
//
//     }
// }