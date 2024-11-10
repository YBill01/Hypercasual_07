using Cysharp.Threading.Tasks;
using GameName.Services;
using GameName.Utilities;
using VContainer.Unity;

namespace GameName.Loading
{
	public class LoadingFlow : IStartable
	{
		private SceneLoader _sceneLoader;

		public LoadingFlow(
			SceneLoader sceneLoader)
		{
			_sceneLoader = sceneLoader;
		}

		public void Start()
		{
			LoadNext().Forget();
		}

		private async UniTaskVoid LoadNext()
		{
			await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);

			(int, UnityEngine.SceneManagement.LoadSceneMode) scene = RuntimeConstants.Scenes.CORE;

			_sceneLoader.Load(scene.Item1, scene.Item2);
		}
	}
}