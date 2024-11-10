using System;

public abstract class UIFadeFrontScreen : UIFadeScreen
{
	protected override void SetDefault()
	{
		base.SetDefault();

		_canvasGroup.blocksRaycasts = true;
	}

	protected override void FadeIn(Action action = null)
	{
		base.FadeIn(action);

		_canvasGroup.blocksRaycasts = true;
	}
	protected override void FadeOut(Action action = null)
	{
		base.FadeOut(action);

		_canvasGroup.blocksRaycasts = true;
	}
}