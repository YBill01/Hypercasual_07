using DG.Tweening;
using System;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIFadeScreen : UIScreen
{
	internal bool isImmediate;

	protected float _fadeInDuration;
	protected float _fadeOutDuration;

	protected CanvasGroup _canvasGroup;

	protected virtual void Awake()
	{
		_canvasGroup = GetComponent<CanvasGroup>();

		SetDefault();
	}

	protected override void OnInit()
	{
		_fadeInDuration = 0.0f;
		_fadeOutDuration = 0.0f;
	}

	protected virtual void SetDefault()
	{
		isImmediate = false;

		_canvasGroup.alpha = 0.0f;
		_canvasGroup.blocksRaycasts = false;
	}

	protected override void OnActivate(Action action)
	{
		if (isImmediate)
		{
			OnImmediate();

			base.OnActivate(action);

			_canvasGroup.alpha = 1.0f;
			_canvasGroup.blocksRaycasts = true;
		}
		else
		{
			OnPreActivate(action);
			FadeIn();
		}
	}
	protected override void OnDeActivate(Action action)
	{
		if (isImmediate)
		{
			OnImmediate();

			base.OnDeActivate(action);

			SetDefault();
		}
		else
		{
			OnPreDeActivate();
			FadeOut(action);
		}
	}

	protected virtual void OnImmediate()
	{
		_canvasGroup.DOComplete();
	}

	protected virtual void FadeIn(Action action = null)
	{
		_canvasGroup.blocksRaycasts = false;

		_canvasGroup.DOFade(1.0f, _fadeInDuration)
			.From(0.0f)
			.SetEase(Ease.OutQuad)
			.OnComplete(() =>
			{
				FadeInComplete(action);
			});
	}
	protected virtual void FadeOut(Action action = null)
	{
		_canvasGroup.blocksRaycasts = false;

		_canvasGroup.DOFade(0.0f, _fadeOutDuration)
			.From(1.0f)
			.SetEase(Ease.OutQuad)
			.OnComplete(() =>
			{
				FadeOutComplete(action);
			});
	}

	protected virtual void FadeInComplete(Action action = null)
	{
		_canvasGroup.blocksRaycasts = true;

		OnPostActivate(action);
	}
	protected virtual void FadeOutComplete(Action action = null)
	{
		OnPostDeActivate(action);
		SetDefault();
	}
}

public static class UIFadeScreenExtensions
{
	public static T IsImmediate<T>(this T t, bool value = true) where T : UIFadeScreen
	{
		t.isImmediate = value;

		return t;
	}
}