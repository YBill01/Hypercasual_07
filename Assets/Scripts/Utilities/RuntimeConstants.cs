using UnityEngine.SceneManagement;

namespace GameName.Utilities
{
	public static class RuntimeConstants
	{
		public static class Scenes
		{
			public static readonly (int, LoadSceneMode) BOOTSTRAP = (SceneUtility.GetBuildIndexByScenePath("Scenes/Bootstrap"), LoadSceneMode.Single);
			public static readonly (int, LoadSceneMode) EMPTY = (SceneUtility.GetBuildIndexByScenePath("Scenes/Empty"), LoadSceneMode.Single);
			public static readonly (int, LoadSceneMode) LOADING = (SceneUtility.GetBuildIndexByScenePath("Scenes/Loading"), LoadSceneMode.Single);
			public static readonly (int, LoadSceneMode) CORE = (SceneUtility.GetBuildIndexByScenePath("Scenes/Core"), LoadSceneMode.Single);
			public static readonly (int, LoadSceneMode) META = (SceneUtility.GetBuildIndexByScenePath("Scenes/Meta"), LoadSceneMode.Additive);
			public static readonly (int, LoadSceneMode) GAME = (SceneUtility.GetBuildIndexByScenePath("Scenes/Game"), LoadSceneMode.Additive);
		}

		public const float ENERGY_TO_SCALE = 100.0f;

		public const float NEAREST_OBSTACLE_OFFSET = 2.0f;
	}
}