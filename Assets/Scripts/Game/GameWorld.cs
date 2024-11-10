using GameName.Core;
using GameName.Data;
using UnityEngine;
using VContainer;

public class GameWorld : MonoBehaviour, IPausable, IUpdatable
{
	private IObjectResolver _resolver;

	private Profile _profile;
	private PlayerBehaviour _player;
	private ObstaclesBehaviour _obstacles;
	private BulletsBehaviour _bullets;
	private TargetBehaviour _target;
	private VCamera _camera;

	[Inject]
	public void Construct(
		IObjectResolver resolver,
		Profile profile,
		PlayerBehaviour player,
		ObstaclesBehaviour obstacles,
		BulletsBehaviour bullets,
		TargetBehaviour target,
		VCamera camera)
	{
		_resolver = resolver;

		_profile = profile;
		_player = player;
		_obstacles = obstacles;
		_bullets = bullets;
		_target = target;
		_camera = camera;
	}

	public void Init(LevelConfigData config)
	{
		_target.Init();
		_obstacles.Generate(config);
		_player.Init();
		_player.SetEnergy(config.playerEnergy);
		_player.transform.position = config.playerStartPosition;
		_camera.SetFollowTarget(_player.CameraTarget);
	}

	public void Clear()
	{
		_obstacles.ClearAll();
		_bullets.ClearAll();
	}

	public void SetPause(bool pause)
	{
		_target.SetPause(pause);
	}

	public void OnUpdate(float deltaTime)
	{
		_player.OnUpdate(deltaTime);
		_bullets.OnUpdate(deltaTime);
		_obstacles.OnUpdate(deltaTime);
	}
}