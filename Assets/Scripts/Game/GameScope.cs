using GameName.UI;
using VContainer;
using VContainer.Unity;

namespace GameName.Game
{
	public class GameScope : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterComponentInHierarchy<UIGame>();

			builder.RegisterComponentInHierarchy<VCamera>();
			builder.RegisterComponentInHierarchy<GameInstance>();
			builder.RegisterComponentInHierarchy<GameWorld>();
			builder.RegisterComponentInHierarchy<PlayerBehaviour>();
			builder.RegisterComponentInHierarchy<BulletsBehaviour>();
			builder.RegisterComponentInHierarchy<ObstaclesBehaviour>();
			builder.RegisterComponentInHierarchy<TargetBehaviour>();
			builder.RegisterComponentInHierarchy<RoadBehaviour>();
			builder.RegisterComponentInHierarchy<InputController>();

			builder.RegisterEntryPoint<GameFlow>()
				.AsSelf();
		}
	}
}