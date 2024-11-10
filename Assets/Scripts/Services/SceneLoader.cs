using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameName.Services
{
	public class SceneLoader
	{
		public Action<float> Progress;
		public Action<int> Complete;

		public int SceneIndex { get; private set; }

		public LoaderState State { get; private set; }
		public enum LoaderState
		{
			None,
			Loading,
			Completed
		}

		public bool IsValid()
		{
			if (State == LoaderState.Loading)
			{
				Debug.LogError($"Loader is already using {State} state.");

				return false;
			}

			return true;
		}

		public void Clear()
		{
			State = LoaderState.None;
			SceneIndex = -1;

			Progress = null;
			Complete = null;
		}

		public SceneLoader Load(int sceneIndex, LoadSceneMode loadSceneMode = LoadSceneMode.Single/*, float delay = 0.0f*/)
		{
			if (!IsValid())
			{
				return this;
			}

			SceneIndex = sceneIndex;

			State = LoaderState.Loading;

			LoadAsync(sceneIndex, loadSceneMode).Forget();

			return this;
		}
		private async UniTask LoadAsync(int sceneIndex, LoadSceneMode loadSceneMode)
		{
			await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);

			AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex, loadSceneMode);
			asyncOperation.allowSceneActivation = false;

			while (!asyncOperation.isDone)
			{
				Progress?.Invoke(asyncOperation.progress);

				if (asyncOperation.progress >= 0.9f)
				{
					asyncOperation.allowSceneActivation = true;
				}

				await UniTask.Yield();
			}

			State = LoaderState.Completed;

			Complete?.Invoke(sceneIndex);

			Clear();
		}

		public SceneLoader Unload(int sceneIndex)
		{
			if (!SceneManager.GetSceneByBuildIndex(sceneIndex).isLoaded)
			{
				return this;
			}

			UnloadAsync(sceneIndex).Forget();

			return this;
		}
		private async UniTask UnloadAsync(int sceneIndex)
		{
			await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);

			await SceneManager.UnloadSceneAsync(sceneIndex);
		}
	}

	public static class SceneLoaderExtensions
	{
		public static T OnProgress<T>(this T t, Action<float> action) where T : SceneLoader
		{
			t.Progress = action;

			return t;
		}
		public static T OnComplete<T>(this T t, Action<int> action) where T : SceneLoader
		{
			t.Complete = action;

			return t;
		}
	}
}