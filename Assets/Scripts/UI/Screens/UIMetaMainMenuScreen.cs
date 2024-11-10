using GameName.Core;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace GameName.UI
{
	public class UIMetaMainMenuScreen : UIScreen
	{
		[Space]
		[SerializeField]
		private GameObject m_menuPanel;

		[Space]
		[SerializeField]
		private Button m_exitButton;

		[Space]
		[SerializeField]
		private Button m_newGameButton;
		
		private App _app;
		private Profile _profile;
		private CoreFlow _coreFlow;

		[Inject]
		public void Construct(
			App app,
			Profile profile,
			CoreFlow coreFlow)
		{
			_app = app;
			_profile = profile;
			_coreFlow = coreFlow;
		}

		private void OnEnable()
		{
			m_exitButton.onClick.AddListener(ExitButtonOnClick);

			m_newGameButton.onClick.AddListener(NewGameButtonOnClick);
		}
		private void OnDisable()
		{
			m_exitButton.onClick.RemoveListener(ExitButtonOnClick);

			m_newGameButton.onClick.RemoveListener(NewGameButtonOnClick);
		}

		private void ExitButtonOnClick()
		{
			_app.Quit();
		}

		private void NewGameButtonOnClick()
		{
			_coreFlow.StartGame();
		}
		private void ContinueGameButtonOnClick()
		{
			_coreFlow.StartGame();
		}
	}
}