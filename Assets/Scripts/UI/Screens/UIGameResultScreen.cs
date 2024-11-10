using GameName.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace GameName.UI
{
	public class UIGameResultScreen : UIScreen
	{
		[Space]
		[SerializeField]
		private TMP_Text m_messageText;

		[Space]
		[SerializeField]
		private Button m_restartButton;

		private GameFlow _gameFlow;

		[Inject]
		public void Construct(
			GameFlow gameFlow)
		{
			_gameFlow = gameFlow;
		}

		private void OnEnable()
		{
			m_restartButton.onClick.AddListener(RestartButtonOnClick);
		}
		private void OnDisable()
		{
			m_restartButton.onClick.RemoveListener(RestartButtonOnClick);
		}

		public void SetMessage(string message)
		{
			m_messageText.text = message;
		}

		private void RestartButtonOnClick()
		{
			SelfHideInternal();

			_gameFlow.ReStart();
		}
	}
}