using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Nodes.GodotExtensions;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.TestSupport;

namespace Drifter.DrifterCode.Nodes.Vfx;

public partial class NPistolShotVfx : Node2D
{
	private static string _scenePath = "res://Drifter/scenes/vfx/vfx_pistol_shot.tscn";
	public static IEnumerable<string> AssetPaths => [_scenePath];

	private CancellationTokenSource? _cts;
	private AnimatedSprite2D _bullet;
	private AnimatedSprite2D _muzzleFlash;
	private Vector2 _target;
	private Vector2 _muzzle;
	

	public static NPistolShotVfx? Create(Creature target, Creature player)
	{
		if (TestMode.IsOn || player.Player?.Character is not Character.Drifter)
			return null;
		var nCreatureTarget = NCombatRoom.Instance?.GetCreatureNode(target);
		var nCreaturePlayer = NCombatRoom.Instance?.GetCreatureNode(player);
		if (nCreaturePlayer is null || nCreatureTarget is null)
			return null;
		var muzzlePos = ((Node2D)nCreaturePlayer.Visuals.FindChild("Muzzle")).GlobalPosition;
		return Create(nCreatureTarget.VfxSpawnPosition, muzzlePos);
	}

	public static NPistolShotVfx? Create(Vector2 targetPosition, Creature player)
	{
		if (TestMode.IsOn || player.Player?.Character is not Character.Drifter)
			return null;
		var nCreaturePlayer = NCombatRoom.Instance?.GetCreatureNode(player);
		if (nCreaturePlayer is null)
			return null;
		var muzzlePos = ((Node2D)nCreaturePlayer.Visuals.FindChild("Muzzle")).GlobalPosition;
		return Create(targetPosition, muzzlePos);
	}

	public static NPistolShotVfx? Create(Vector2 targetPosition, Vector2 muzzlePosition)
	{
		if (TestMode.IsOn)
			return null;
		var nBulletVfx = PreloadManager.Cache.GetScene(_scenePath).Instantiate<NPistolShotVfx>();
		nBulletVfx.GlobalPosition = muzzlePosition;
		nBulletVfx._target = targetPosition;
		nBulletVfx._muzzle = muzzlePosition;
		return nBulletVfx;
	}

	public override void _Ready()
	{
		_bullet = GetNode<AnimatedSprite2D>("Bullet");
		_muzzleFlash = GetNode<AnimatedSprite2D>("MuzzleFlash");
		TaskHelper.RunSafely(PlayAnim());
	}

	public override void _ExitTree()
	{
		_cts?.Cancel();
	}


	private async Task PlayAnim()
	{
		_cts = new CancellationTokenSource();
		var difference = _target - _muzzle;
		_bullet.Rotation = (float)Math.Atan(difference.Y / difference.X);
		var timer = 0.0f;
		const float maxDur = 0.25f;
		while (timer < maxDur)
		{
			var frameTime = await this.AwaitProcessFrame();
			_bullet.GlobalPosition += difference * (frameTime / maxDur);
			timer += frameTime;
		}
		this.QueueFreeSafely();
	}
}
