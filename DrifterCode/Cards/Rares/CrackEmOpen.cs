using BaseLib.Abstracts;
 using Drifter.DrifterCode.Character;
 using Drifter.DrifterCode.DynamicVars;
 using Drifter.DrifterCode.Hooks;
 using Drifter.DrifterCode.Relics;
 using MegaCrit.Sts2.Core.Commands;
 using MegaCrit.Sts2.Core.Commands.Builders;
 using MegaCrit.Sts2.Core.Entities.Cards;
 using MegaCrit.Sts2.Core.Entities.Creatures;
 using MegaCrit.Sts2.Core.GameActions.Multiplayer;
 using MegaCrit.Sts2.Core.Localization.DynamicVars;
 using MegaCrit.Sts2.Core.Models;
 using MegaCrit.Sts2.Core.ValueProps;
 
 namespace Drifter.DrifterCode.Cards.Rares;
 
 public class CrackEmOpen : DrifterCard
 {
 
     private int _baseCost;
 
     public CrackEmOpen() : base(0, CardType.Attack,
         CardRarity.Rare, TargetType.AnyEnemy)
     {
         CustomResources<ChargeResource>.SetXCost(this);
     }
 
     protected override IEnumerable<DynamicVar> CanonicalVars =>
         [new ModularVar(9), new DamageVar(8, ValueProp.Move)];
 
     public override IEnumerable<CardKeyword> CanonicalKeywords => [DrifterEnums.Gun];
 
     protected override async Task OnPlay(
         PlayerChoiceContext context,
         CardPlay play)
     {
         AttackContext attackContext =
             await AttackCommand.CreateContextAsync(CombatState, context, play);
         IEnumerable<DamageResult> damageResults;
         var numRepeats = (int)(CustomResources<ChargeResource>.Cost(this).ResolveXValue() /
                                DynamicVars[ModularVar.Key].BaseValue);
         if (Owner.Relics.Contains(ModelDb.Relic<PinksHelmet>())) numRepeats += 1;
         for (int i = 1; i <= numRepeats; i++)
         {
             await CreatureCmd.TriggerAnim(Owner.Creature, "attack", 200);
             damageResults = await CreatureCmd.Damage(context, play.Target, DynamicVars.Damage.BaseValue * (i), ValueProp.Move, Owner.Creature, this, play);
             attackContext.AddHit(damageResults);
             await DrifterHooks.AfterBarrageRepeat(CombatState, context, this, i);
         }
 
         if (attackContext != null) await attackContext.DisposeAsync();
         attackContext = null;
         
     }
 
     protected override void OnUpgrade()
     {
         DynamicVars.Damage.UpgradeValueBy(2);
     }
 }