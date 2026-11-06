using Godot;
using Godot.Collections;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.TestSupport;

namespace Drifter.DrifterCode.Nodes.Vfx;

public partial class NShotgunBlastVfx : Node2D
{
	private static string _scenePath = "res://Drifter/scenes/vfx/vfx_shotgun_blast.tscn";

	private CancellationTokenSource? _cts;
	private Vector2 _muzzle;
	private AnimatedSprite2D _muzzleFlash;

	// [Export]
	// private Array<GpuParticles2D> _particles { get; set; }
	private GpuParticles2D _particles;


	public static NShotgunBlastVfx? Create(Creature player)
	{
		if (TestMode.IsOn)
			return null;
		var nCreature = NCombatRoom.Instance?.GetCreatureNode(player);
		if (nCreature is null)
			return null;
		var muzzlePos = ((Node2D)nCreature.Visuals.FindChild("Muzzle")).GlobalPosition;
		return Create(muzzlePos);
	}

	public static NShotgunBlastVfx? Create(Vector2 muzzlePosition)
	{
		if (TestMode.IsOn)
			return null;
		var nShotgunVfx = PreloadManager.Cache.GetScene(_scenePath).Instantiate<NShotgunBlastVfx>();
		nShotgunVfx.GlobalPosition = muzzlePosition;
		return nShotgunVfx;
	}

	public override void _ExitTree()
	{
		_cts?.Cancel();
	}

	public override void _Ready()
	{
		_muzzleFlash = GetNode<AnimatedSprite2D>("MuzzleFlash");
		_particles = GetNode<GpuParticles2D>("Pellets");
		TaskHelper.RunSafely(PlaySequence());
	}

	// private async Task PlaySequence()
	// {
	// 	_cts = new CancellationTokenSource();
	// 	for (int i = 0; i < _particles.Count; i++)
	// 	{
	// 		MainFile.Logger.Warn(i.ToString());
	// 		_particles[i].Restart();
	// 	}
	// 	await Cmd.Wait(2f, _cts.Token);
	// 	this.QueueFreeSafely();
	// }
	private async Task PlaySequence()
	{
		_cts = new CancellationTokenSource();
		_particles.Restart();
		await Cmd.Wait(2f, _cts.Token);
		this.QueueFreeSafely();
	}
}
