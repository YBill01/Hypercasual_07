using GameName.Core;
using GameName.Game;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace GameName.UI
{
	public class UIGameMainMenuScreen : UIScreen
	{

		[Space]
		[SerializeField]
		private Button m_homeButton;

		[SerializeField]
		private Button m_restartButton;

		[Space]
		[SerializeField]
		private UIToggleButton m_pauseToggleButton;


		private Profile _profile;
		private CoreFlow _coreFlow;
		private GameFlow _gameFlow;
		private GameInstance _game;

		[Inject]
		public void Construct(
			Profile profile,
			CoreFlow coreFlow,
			GameFlow gameFlow,
			GameInstance game)
		{
			_profile = profile;
			_coreFlow = coreFlow;
			_gameFlow = gameFlow;
			_game = game;
		}

		private void OnEnable()
		{
			m_homeButton.onClick.AddListener(HomeButtonOnClick);
			m_restartButton.onClick.AddListener(RestartButtonOnClick);

			m_pauseToggleButton.onValueChanged += PauseToggleButtonOnValueChanged;
		}
		private void OnDisable()
		{
			m_homeButton.onClick.RemoveListener(HomeButtonOnClick);
			m_restartButton.onClick.RemoveListener(RestartButtonOnClick);

			m_pauseToggleButton.onValueChanged += PauseToggleButtonOnValueChanged;
		}

		private void HomeButtonOnClick()
		{
			_coreFlow.EndGame();
		}
		private void RestartButtonOnClick()
		{
			m_pauseToggleButton.Value = false;
			_gameFlow.ReStart();
		}

		private void PauseToggleButtonOnValueChanged(bool value)
		{
			_gameFlow.SetPauseValue(value);
		}
	}
}