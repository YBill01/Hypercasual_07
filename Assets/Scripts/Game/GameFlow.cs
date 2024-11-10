using GameName.Core;
using GameName.Data;
using VContainer;
using VContainer.Unity;

namespace GameName.Game
{
	public class GameFlow : IStartable, IPostStartable
	{
		private IObjectResolver _resolver;

		private Profile _profile;
		private GameConfigData _config;
		private CoreFlow _coreFlow;
		private GameInstance _game;
		private GameWorld _gameWorld;

		public GameFlow(
			IObjectResolver resolver,
			Profile profile,
			GameConfigData config,
			CoreFlow coreFlow,
			GameInstance game,
			GameWorld gameWorld)
		{
			_resolver = resolver;

			_profile = profile;
			_config = config;
			_coreFlow = coreFlow;
			_game = game;
			_gameWorld = gameWorld;
		}

		public void Start()
		{
			
		}

		public void PostStart()
		{
			_game.StartGame(_config.levels[0]).Forget();
		}

		public void SetPauseValue(bool value)
		{
			_game.SetPause(value);
		}

		public void ReStart()
		{
			_game.EndGame();
			_game.StartGame(_config.levels[0]).Forget();
		}
	}
}