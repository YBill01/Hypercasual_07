using GameName.Core;
using UnityEngine;
using VContainer;

namespace GameName.UI
{
	public class UIGame : MonoBehaviour
	{
		[SerializeField]
		private UIScreenController m_uiController;

		private UIGameMainMenuScreen _mainMenuScreen;

		private IObjectResolver _resolver;
		
		private Profile _profile;

		[Inject]
		public void Construct(
			IObjectResolver resolver,
			Profile profile)
		{
			_resolver = resolver;

			_profile = profile;
		}

		private void OnEnable()
		{
			m_uiController.OnShow.AddListener<UIGameMainMenuScreen>(UIMainMenuScreenOnShow);
			m_uiController.OnHide.AddListener<UIGameMainMenuScreen>(UIMainMenuScreenOnHide);
		}
		private void OnDisable()
		{
			m_uiController.OnShow.RemoveListener<UIGameMainMenuScreen>();
			m_uiController.OnHide.RemoveListener<UIGameMainMenuScreen>();
		}

		private void UIMainMenuScreenOnShow(UIScreen screen)
		{
			UIGameMainMenuScreen mainMenuScreen = screen as UIGameMainMenuScreen;

			_resolver.Inject(mainMenuScreen);

			_mainMenuScreen = mainMenuScreen;
		}
		private void UIMainMenuScreenOnHide(UIScreen screen)
		{
			UIGameMainMenuScreen mainMenuScreen = screen as UIGameMainMenuScreen;

			_mainMenuScreen = null;
		}

		public void ShowResultScreen(string message)
		{
			UIGameResultScreen resultScreen = m_uiController.Show<UIGameResultScreen>();
			resultScreen.SetMessage(message);

			_resolver.Inject(resultScreen);
		}
	}
}