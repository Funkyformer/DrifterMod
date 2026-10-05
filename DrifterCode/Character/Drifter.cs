using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using Drifter.DrifterCode.Cards.Basics;
using Drifter.DrifterCode.Cards.NotUsing;
using Drifter.DrifterCode.Extensions;
using Drifter.DrifterCode.Relics;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Character;


public class Drifter : PlaceholderCharacterModel
{
    public const string CharacterId = "Drifter";

    public static readonly Color Color = new("FC16B7");

    public override Color NameColor => Color;
    public override Color MapDrawingColor => Color;
    public override Color DialogueColor => Color;
    public override CharacterGender Gender => CharacterGender.Masculine;
    public override int StartingHp => 74;

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<StrikeDrifter>(),
        ModelDb.Card<StrikeDrifter>(),
        ModelDb.Card<StrikeDrifter>(),
        ModelDb.Card<StrikeDrifter>(),
        ModelDb.Card<StrikeDrifter>(),
        ModelDb.Card<DefendDrifter>(),
        ModelDb.Card<DefendDrifter>(),
        ModelDb.Card<DefendDrifter>(),
        ModelDb.Card<DefendDrifter>(),
        ModelDb.Card<PistolShot>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<DriftersSyCom>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<DrifterCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<DrifterRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<DrifterPotionPool>();

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets.
        These are just some of the simplest assets, given some placeholders to differentiate your character with.
        You don't have to, but you're suggested to rename these images. */
    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }

    public override string CustomIconTexturePath => "character_icon_drifter.png".CharacterUiPath();
    // public override string CustomVisualPath => "res://Drifter/scenes/creature_visuals/drifter_with_anims_redux.tscn";
    public override string CustomVisualPath => "res://Drifter/scenes/creature_visuals/drifter_anims.tscn";
    public override string CustomCharacterSelectIconPath => "char_select_drifter.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_drifter.png".CharacterUiPath();
    public override string CustomCharacterSelectBg => "res://Drifter/scenes/drifter_character_select.tscn";


    // my own stuff below
    // public override NCreatureVisuals CreateCustomVisuals()
    // {
    //     return new DrifterNCreatureVisuals();
    // }
    public override CreatureAnimator? SetupCustomAnimationStates(MegaSprite controller)
    {
        CreatureAnimator animator = new CreatureAnimator(new AnimState("Idle", isLooping: true), controller);
        AddState(animator, "Idle");
        AddState(animator, "Die");
        AddState(animator, "Hurt");
        AddState(animator, "SpecialGrenade");
        return animator;
    }

    private void AddState(CreatureAnimator animator, String state)
    {
        animator.AddAnyState(state, new AnimState(state.ToLower()));
    }
}