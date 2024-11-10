using UnityEngine;

namespace GameName.Data
{
	[CreateAssetMenu(menuName = "Game/ObstacleConfigData", fileName = "ObstacleConfig", order = 3)]
	public class ObstacleConfigData : ScriptableObject
	{
		public float damageDuration = 1.0f;
		public AnimationCurve damageTimeCurve;
	}
}