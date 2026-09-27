// using BaseLib.Utils;
// using Drifter.DrifterCode.Cards;
// using Drifter.DrifterCode.Character;
// using MegaCrit.Sts2.Core.Commands;
// using MegaCrit.Sts2.Core.Commands.Builders;
// using MegaCrit.Sts2.Core.Entities.Cards;
// using MegaCrit.Sts2.Core.Entities.Creatures;
// using MegaCrit.Sts2.Core.GameActions.Multiplayer;
// using MegaCrit.Sts2.Core.Localization.DynamicVars;
// using MegaCrit.Sts2.Core.ValueProps;
//
// namespace Drifter.DrifterCode.Cards.Uncommons;
//
// public class Onslaught() : DrifterCard(2,
//     CardType.Attack, CardRarity.Uncommon,
//     TargetType.AnyEnemy)
// {
//     protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(9, ValueProp.Move)];
//     public override IEnumerable<CardKeyword> CanonicalKeywords => [DrifterEnums.Blade];
//
//     protected override async Task OnPlay(
//         PlayerChoiceContext choiceContext,
//         CardPlay play)
//     {
//         AttackContext attackContext = await AttackCommand.CreateContextAsync(CombatState, choiceContext, play);
//         IEnumerable<DamageResult> damageResults;
//         var divisor = 1.0m;
//         do
//         {
//             await CreatureCmd.TriggerAnim(Owner.Creature, "attack", 200);
//             damageResults = await CreatureCmd.Damage(choiceContext, play.Target, DynamicVars.Damage.BaseValue / divisor,
//                 ValueProp.Move, Owner.Creature, this, play);
//             divisor *= 2;
//         } while (damageResults.Last().TotalDamage > 1);
//     }
//
//     protected override void OnUpgrade()
//     {
//         DynamicVars.Damage.UpgradeValueBy(2);
//     }
// }