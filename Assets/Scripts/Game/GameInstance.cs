using Cysharp.Threading.Tasks;
using GameName.Core;
using GameName.Data;
using GameName.Game.HFSM;
using GameName.UI;
using UnityEngine;
using VContainer;

public class GameInstance : MonoBehaviour, IPausable
{
	private bool _isPlayGame = false;
	public bool IsPlayGame => _isPlayGame;

	private bool _isPaused = false;
	public bool IsPaused => _isPaused;

	private GameStateMachine _stateMachine;

	private Profile _profile;
	private UIGame _uiGame;
	private GameWorld _gameWorld;
	private PlayerBehaviour _player;
	private BulletsBehaviour _bullets;
	private ObstaclesBehaviour _obstacles;
	private TargetBehaviour _target;
	private RoadBehaviour _road;
	private InputController _inputController;

	[Inject]
	public void Construct(
		Profile profile,
		UIGame uiGame,
		GameWorld gameWorld,
		PlayerBehaviour player,
		BulletsBehaviour bullets,
		ObstaclesBehaviour obstacles,
		TargetBehaviour target,
		RoadBehaviour road,
		InputController inputController)
	{
		_profile = profile;
		_uiGame = uiGame;
		_gameWorld = gameWorld;
		_player = player;
		_bullets = bullets;
		_obstacles = obstacles;
		_target = target;
		_road = road;
		_inputController = inputController;
	}

	public async UniTaskVoid StartGame(LevelConfigData config)
	{
		await UniTask.NextFrame();

		_gameWorld.Init(config);

		InputState inputState = new InputState(this, _player, _inputController);
		ShootState shootState = new ShootState(_bullets, _obstacles);
		ResultState resultState = new ResultState(this, _player, _road, _uiGame, _target);

		_stateMachine = new GameStateMachine(inputState, shootState, resultState);

		_player.OnShoot += inputState.AddEventTransition(shootState);
		_player.OnEnergyGone += inputState.AddEventTransition(resultState);
		_bullets.OnBulletDispose += shootState.AddEventTransition(resultState);
		_obstacles.OnDamage += shootState.AddEventTransition(resultState);
		resultState.OnResultComplete += resultState.AddEventTransition(inputState);

		_stateMachine.Init();

		await UniTask.NextFrame();

		SetPause(false);
		_isPlayGame = true;
	}
	public void SetPause(bool pause)
	{
		_isPaused = pause;

		_gameWorld.SetPause(pause);
	}
	public void EndGame()
	{
		_stateMachine.Dispose();

		_gameWorld.Clear();

		_isPlayGame = false;
	}

	private void Update()
	{
		if (!_isPlayGame || _isPaused)
		{
			return;
		}

		float deltaTime = Time.deltaTime;

		_gameWorld.OnUpdate(deltaTime);
		_stateMachine.Update();
	}
}