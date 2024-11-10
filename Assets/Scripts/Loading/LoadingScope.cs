using GameName.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameName.Loading
{
	public class LoadingScope : LifetimeScope
	{
		[Space]
		[SerializeField]
		private UILoader m_uiLoader;

		protected override void Awake()
		{
			base.Awake();

			DontDestroyOnLoad(this);
		}

		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterComponent(m_uiLoader);

			builder.RegisterEntryPoint<LoadingFlow>();
		}
	}
}