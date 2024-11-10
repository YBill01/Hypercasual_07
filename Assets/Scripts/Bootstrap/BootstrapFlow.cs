using GameName.Services;
using GameName.Utilities;
using System;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace GameName.Core
{
	public class BootstrapFlow : IStartable
	{
		private SceneLoader _sceneLoader;
		private Profile _profile;

		public BootstrapFlow(
			SceneLoader sceneLoader,
			Profile profile)
		{
			_sceneLoader = sceneLoader;
			_profile = profile;
		}

		public void Start()
		{
			AppData appData = _profile.Get<AppData>().Load();
			appData.lastEntryDate = DateTime.UtcNow;
			PlayerData playerData = _profile.Get<PlayerData>().Load();

			LoadNext();
		}

		private void LoadNext()
		{
			(int, LoadSceneMode) scene = RuntimeConstants.Scenes.LOADING;

			_sceneLoader.Load(scene.Item1, scene.Item2);
		}
	}
}