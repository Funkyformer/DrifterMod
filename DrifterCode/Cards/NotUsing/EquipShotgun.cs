using BaseLib.Utils;
using Drifter.DrifterCode.Character;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace Drifter.DrifterCode.Cards.Undecideds;

// public class EquipShotgun() : DrifterCard(-1,
//     CardType.Skill, CardRarity.Common,
//     TargetType.None)
// {
//     public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Unplayable];
//
//     public override bool CanBeGeneratedInCombat => false;
//
//     public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
//     {
//         if (!participants.Contains(Owner.Creature) || Owner.PlayerCombatState.TurnNumber > 1)
//             return;
//         CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(
//             combatState.CreateCard<ShotgunBlast>(Owner), PileType.Draw, Owner, CardPilePosition.Random));
//         if (IsUpgraded)
//         {
//             CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(
//                 combatState.CreateCard<ShotgunBlast>(Owner), PileType.Draw, Owner, CardPilePosition.Random));
//         }
//         RemoveFromCurrentPile();
//     }
//
//     protected override IEnumerable<IHoverTip> ExtraHoverTips => HoverTipFactory.FromCardWithCardHoverTips<ShotgunBlast>();
// }