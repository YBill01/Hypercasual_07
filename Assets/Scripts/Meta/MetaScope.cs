using GameName.UI;
using VContainer;
using VContainer.Unity;

namespace GameName.Meta
{
	public class MetaScope : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterComponentInHierarchy<UIMeta>();

			builder.RegisterEntryPoint<MetaFlow>();
		}
	}
}