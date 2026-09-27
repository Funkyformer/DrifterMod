using BaseLib.Abstracts;
using Drifter.DrifterCode.Extensions;
using Godot;

namespace Drifter.DrifterCode.Character;

public class DrifterRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => Drifter.Color;

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}