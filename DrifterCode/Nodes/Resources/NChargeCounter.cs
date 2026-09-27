using BaseLib.Abstracts;
using BaseLib.Hooks;
using Drifter.DrifterCode.Character;
using Godot;
using MegaCrit.Sts2.addons.mega_text;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace Drifter.DrifterCode.Nodes.Resources;

public partial class NChargeCounter : Control
{
	private Font _font = ResourceLoader.Load<Font>("res://Drifter/fonts/kreon_bold.ttf");
	private Player? _player;
	private PlayerCombatState? _playerCombatState = null;
	private TextureRect _icon = null!;
	private MegaLabel _label = new();
	private HoverTip _hoverTip;
	private Color _outline = new("660066");

	public override void _Ready()
	{
		_label = CreateLabel();
		AddChild(_label);
		_label.Position = new Vector2(0,0);
		Visible = false;
	}

	public void SetCombatState(PlayerCombatState combatState)
	{
		MainFile.Logger.Warn("Combat State has been Set");
		_playerCombatState = combatState;
	}

	public override void _Process(double delta)
	{
		// if (_playerCombatState is null)
		// {
		// 	MainFile.Logger.Warn("Player combat state is null");
		// 	return;
		// }
		// _label.Text = CustomResources<ChargeResource>.Get(_playerCombatState).Amount.ToString();
		if (_playerCombatState is null) return;
		int amt = CustomResources<ChargeResource>.Get(_playerCombatState).Amount;
		_label.AddThemeColorOverride("font_color", amt == 0 ? StsColors.red : StsColors.cream);
		_label.AddThemeColorOverride("font_outline_color", amt == 0 ? StsColors.unplayableEnergyCostOutline : _outline);
		_label.Text = amt.ToString();
		if (!Visible && amt > 0) Visible = true;
	}


	private MegaLabel CreateLabel()
	{
		var label = new MegaLabel();
		label.MaxFontSize = 100;
		label.MinFontSize = 8;
		label.AutoSizeEnabled = false;
		label.HorizontalAlignment = HorizontalAlignment.Center;
		label.VerticalAlignment = VerticalAlignment.Center;
		label.LayoutMode = 0; //position
		label.Size = new Vector2(96, 87);
		label.SetAnchorsPreset(LayoutPreset.Center);
		label.AddThemeColorOverride("font_color", new Color("DFDFD4"));
		label.AddThemeColorOverride("font_shadow_color", new Color("00000030"));
		label.AddThemeColorOverride("font_outline_color", _outline);
		label.AddThemeConstantOverride("shadow_offset_x", 5);
		label.AddThemeConstantOverride("shadow_offset_y", 5);
		label.AddThemeConstantOverride("outline_size", 14);
		// label.AddThemeConstantOverride("shadow_outline_size", 12);
		label.AddThemeFontOverride("font", _font);
		label.AddThemeFontSizeOverride("font_size", 36);
		label.Text = "0";

		return label;
	}

	// public Task AfterSpendResource(ICombatState combatState, ChargeResource resource, AbstractModel? spender, int amount)
	// {
	// 	MainFile.Logger.Warn("AfterSpendResource has been called");
	// 	if (_playerCombatState is not null)
	// 	{
	// 		int amt = CustomResources<ChargeResource>.Get(_playerCombatState).Amount;
	// 		_label.AddThemeColorOverride("font_color", amt == 0 ? StsColors.red : StsColors.cream);
	// 		_label.AddThemeColorOverride("font_outline_color", amt == 0 ? StsColors.unplayableEnergyCostOutline : _outline);
	// 		_label.Text = amt.ToString();
	// 		if (!Visible && amt > 0) Visible = true;
	// 	}
	// 	return Task.CompletedTask;
	// }
	// public void UpdateCounter()
	// {
	// 	
	// }
}
