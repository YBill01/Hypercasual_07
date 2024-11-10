using UnityEngine;
using YB.HFSM;

namespace GameName.Game.HFSM
{
	public class ShootState : State
	{
		private BulletsBehaviour _bullets;
		private ObstaclesBehaviour _obstacles;

		public ShootState(BulletsBehaviour bullets, ObstaclesBehaviour obstacles)
		{
			_bullets = bullets;
			_obstacles = obstacles;
		}

		protected override void OnEnter()
		{
			_bullets.OnBulletExplode += OnBulletExplode;
		}
		protected override void OnExit()
		{
			_bullets.OnBulletExplode -= OnBulletExplode;
		}

		private void OnBulletExplode(Vector3 position, float radius)
		{
			_obstacles.DamageObstacleSphere(position, radius);
		}
	}
}