using UnityEngine;
using UnityEngine.UI;

namespace GameName.UI
{
	public class UILoadingScreen : UIFadeFrontScreen
	{
		[Space]
		[SerializeField]
		private Slider m_slider;

		protected override void OnInit()
		{
			_fadeInDuration = 0.125f;
			_fadeOutDuration = 0.25f;

			SetProgress(0.0f);
		}

		public void SetProgress(float value)
		{
			m_slider.SetValueWithoutNotify(value);
		}
	}
}