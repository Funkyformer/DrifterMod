using System.Buffers;
using BaseLib.Extensions;
using BaseLib.Utils;
using Drifter.DrifterCode.Character;
using Drifter.DrifterCode.DynamicVars;
using Drifter.DrifterCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using NSpecialGrenadeVfx = Drifter.DrifterCode.Nodes.Vfx.NSpecialGrenadeVfx;

namespace Drifter.DrifterCode.Cards.Undecideds;

public class SpecialGrenade() : DrifterCard(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AllEnemies)
{
    private Texture2D Image => ResourceLoader.Load<Texture2D>("res://Drifter/images/vfx/grenade_toss.png");
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(8, ValueProp.Move), new(
        nameof(SpecialGrenade), 20)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ICombatState combatState = Owner.Creature.CombatState;
        await CommonActions.CardAttack(this, play).BeforeDamage((Func<Task>) (async () =>
            { 
                if (combatState != null)
                {
                    NCreature creature1 = NCombatRoom.Instance?.GetCreatureNode(Owner.Creature);
                    Vector2 zero = Vector2.Zero;
                    Vector2 targetPosition;
                    IReadOnlyList<Creature> targets = combatState.GetCreaturesOnSide(CombatSide.Enemy).Where(c => c.IsHittable).ToList();
                    foreach (Creature target1 in targets)
                    {
                        NCreature creature2 = NCombatRoom.Instance?.GetCreatureNode(target1);
                        zero += creature2 != null ? creature2.VfxSpawnPosition : Vector2.Zero;
                    }
                    targetPosition = zero / targets.Count;
                    NItemThrowVfx child = NItemThrowVfx.Create(creature1 != null ? creature1.VfxSpawnPosition : Vector2.Zero, targetPosition, Image);
                    NCombatRoom instance = NCombatRoom.Instance;
                    if (instance != null)
                        instance.CombatVfxContainer.AddChildSafely(child);
                    await Cmd.Wait(0.5f);
                    NSpecialGrenadeVfx child2 = NSpecialGrenadeVfx.Create(targetPosition);
                        if (instance != null)
                            instance.CombatVfxContainer.AddChildSafely(child2);
                }
            })).WithAttackerAnim(Owner.Character is Character.Drifter ? "SpecialGrenade" : "attack", 0.1f)
            .Execute(choiceContext);
        (await PowerCmd.Apply<SpecialGrenadePower>(choiceContext, Owner.Creature, DynamicVars[nameof(SpecialGrenade)].BaseValue, Owner.Creature, this)).SetSelectedCard(this);
        await CardCmd.Exhaust(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars[nameof(SpecialGrenade)].UpgradeValueBy(-4);
    }
}