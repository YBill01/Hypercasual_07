using GameName.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace GameName.UI
{
	public class UILoader : MonoBehaviour
	{
		[SerializeField]
		private UIScreenController m_uiController;

		private SceneLoader _sceneLoader;

		[Inject]
		public void Construct(SceneLoader sceneLoader)
		{
			_sceneLoader = sceneLoader;
		}

		private void Awake()
		{
			DontDestroyOnLoad(this);
		}

		public void LoadScene((int, LoadSceneMode) scene)
		{
			m_uiController.Show<UILoadingScreen>()
				.OnShow(x =>
				{
					UILoadingScreen uiLoadingScreen = (UILoadingScreen)x;

					_sceneLoader.Load(scene.Item1, scene.Item2)
						.OnProgress((p) =>
						{
							uiLoadingScreen.SetProgress(p);
						})
						.OnComplete((s) =>
						{
							SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(s));

							m_uiController.Hide<UILoadingScreen>();
						});
				});
		}
	}
}