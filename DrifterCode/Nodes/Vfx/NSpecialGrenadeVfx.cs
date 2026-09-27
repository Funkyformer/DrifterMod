using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.TestSupport;

namespace Drifter.DrifterCode.Nodes.Vfx;

public partial class NSpecialGrenadeVfx : Node2D
{
	private AnimatedSprite2D _bomb;
	private CancellationTokenSource? _cts;
	
	public static IEnumerable<string> AssetPaths => [ScenePath];

	public override void _ExitTree() => _cts?.Cancel();

	// private static string ScenePath => SceneHelper.GetScenePath("vfx/vfx_bomb_anim");
	private static string ScenePath = "res://Drifter/scenes/vfx/vfx_bomb_anim.tscn";

	public static NSpecialGrenadeVfx? Create(Vector2 targetPosition)
	{
		if (TestMode.IsOn)
			return null;
		var nspecialGrenadeVfx = PreloadManager.Cache.GetScene(ScenePath).Instantiate<NSpecialGrenadeVfx>();
		nspecialGrenadeVfx.GlobalPosition = targetPosition;
		return nspecialGrenadeVfx;
	}

	public override void _Ready()
	{
		_bomb = GetNode<AnimatedSprite2D>((NodePath) "Explosion");
		TaskHelper.RunSafely(DeleteAfterComplete());
	}

	private async Task DeleteAfterComplete()
	{	
		_cts = new CancellationTokenSource();
		await Task.Delay(500, _cts.Token);
		this.QueueFreeSafely();
	}
}
