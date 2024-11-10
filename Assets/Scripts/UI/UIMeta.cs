using GameName.Core;
using UnityEngine;
using VContainer;

namespace GameName.UI
{
	public class UIMeta : MonoBehaviour
	{
		[SerializeField]
		private UIScreenController m_uiController;

		private UIMetaMainMenuScreen _mainMenuScreen;

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
			m_uiController.OnShow.AddListener<UIMetaMainMenuScreen>(UIMainMenuScreenOnShow);
			m_uiController.OnHide.AddListener<UIMetaMainMenuScreen>(UIMainMenuScreenOnHide);
		}
		private void OnDisable()
		{
			m_uiController.OnShow.RemoveListener<UIMetaMainMenuScreen>();
			m_uiController.OnHide.RemoveListener<UIMetaMainMenuScreen>();
		}

		private void UIMainMenuScreenOnShow(UIScreen screen)
		{
			UIMetaMainMenuScreen mainMenuScreen = screen as UIMetaMainMenuScreen;

			_resolver.Inject(mainMenuScreen);

			_mainMenuScreen = mainMenuScreen;
		}
		private void UIMainMenuScreenOnHide(UIScreen screen)
		{
			UIMetaMainMenuScreen mainMenuScreen = screen as UIMetaMainMenuScreen;

			_mainMenuScreen = null;
		}
	}
}