using BaseLib.Abstracts;
using Drifter.DrifterCode.Cards;
using Drifter.DrifterCode.Character;
using Godot;
using MegaCrit.Sts2.addons.mega_text;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.UI;
using MegaCrit.Sts2.Core.Nodes.Cards;

namespace Drifter.DrifterCode.Nodes.Resources;

public partial class NChargeIcon : TextureRect
{
	private Font _font = ResourceLoader.Load<Font>("res://Drifter/fonts/kreon_bold.ttf");
	
	private MegaLabel _label = new();
	
	public NCard? NCard { get; private set; }

	public NChargeIcon WithData(NCard nCard)
	{
		NCard = nCard;
		return this;
	}

	public override void _Ready()
	{
		_label = CreateLabel();
		AddChild(_label);
		_label.Position = new Vector2(10,5);
	}

	public void UpdateChargeCostVisuals(PileType pileType)
	{
		var label = _label;
		if (NCard!.Visibility != ModelVisibility.Visible || CustomResources<ChargeResource>.CanonicalCost(NCard.Model) == -1)
		{
			label.SetTextAutoSize(string.Empty);
			label.AddThemeColorOverride("font_color", new Color("DFDFD4"));
			label.AddThemeColorOverride("font_outline_color", new Color("660066"));
			Visible = false;
			return;
		}

		var chargeCost = CustomResources<ChargeResource>.Cost(NCard.Model);
		if (chargeCost is null)
		{
			Visible = false;
			return;
		};
		label.SetTextAutoSize(chargeCost.GetWithModifiers(CostModifiers.All).ToString());
		Visible = chargeCost.GetWithModifiers(CostModifiers.None) > 0;
	}
	
	private MegaLabel CreateLabel()
	{
		var label = new MegaLabel();
		label.MaxFontSize = 22;
		label.MinFontSize = 16;
		label.AutoSizeEnabled = false;
		label.HorizontalAlignment = HorizontalAlignment.Center;
		label.VerticalAlignment = VerticalAlignment.Center;
		label.LayoutMode = 0; //position
		label.Size = new Vector2(28, 36);
		label.SetAnchorsPreset(LayoutPreset.Center);
		label.AddThemeColorOverride("font_color", new Color("DFDFD4"));
		label.AddThemeColorOverride("font_shadow_color", new Color("00000030"));
		label.AddThemeColorOverride("font_outline_color", new Color("660066"));
		label.AddThemeConstantOverride("shadow_offset_x", 2);
		label.AddThemeConstantOverride("shadow_offset_y", 2);
		label.AddThemeConstantOverride("outline_size", 12);
		label.AddThemeConstantOverride("shadow_outline_size", 12);
		label.AddThemeFontOverride("font", _font);
		label.AddThemeFontSizeOverride("font_size", 22);
		label.Text = "0";

		return label;
	}
}
