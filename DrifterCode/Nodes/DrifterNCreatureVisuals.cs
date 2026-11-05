using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace Drifter.DrifterCode.Nodes;

[GlobalClass]
public partial class DrifterNCreatureVisuals : NCreatureVisuals
{
	public Marker2D Muzzle { get; private set; }

	public override void _Ready()
	{
		base._Ready();
		Muzzle = GetNode<Marker2D>("Muzzle");
	}
}
