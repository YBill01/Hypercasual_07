using GameName.Core.HFSM;
using GameName.Services;
using GameName.UI;
using System;
using VContainer.Unity;

namespace GameName.Core
{
	public class CoreFlow : IInitializable, IStartable, ITickable
	{
		public event Action OnStartGame;
		public event Action OnEndGame;

		private SceneLoader _sceneLoader;
		private UILoader _uiLoader;

		private CoreStateMachine _stateMachine;

		public CoreFlow(
			SceneLoader sceneLoader,
			UILoader uiLoader)
		{
			_sceneLoader = sceneLoader;
			_uiLoader = uiLoader;
		}

		public void Initialize()
		{
			MetaState metaState = new MetaState(_sceneLoader, _uiLoader);
			GameState gameState = new GameState(_sceneLoader, _uiLoader);

			_stateMachine = new CoreStateMachine(metaState, gameState);

			OnStartGame += metaState.AddEventTransition(gameState);
			OnEndGame += gameState.AddEventTransition(metaState);
		}

		public void Start()
		{
			_stateMachine.Init();
		}

		public void Tick()
		{
			_stateMachine.Update();
		}

		public void StartGame()
		{
			OnStartGame?.Invoke();
		}
		public void EndGame()
		{
			OnEndGame?.Invoke();
		}
	}
}