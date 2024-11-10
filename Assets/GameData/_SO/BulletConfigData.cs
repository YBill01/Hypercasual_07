using UnityEngine;

namespace GameName.Data
{
	[CreateAssetMenu(menuName = "Game/BulletConfigData", fileName = "BulletConfig", order = 2)]
	public class BulletConfigData : ScriptableObject
	{
		public float speed = 1.0f;

		[Space]
		public float explosionSizeMultiplier = 1.0f;
		public float explosionDuration = 1.0f;
		[Range(0.0f, 1.0f)]
		public float explosionDelayRatio = 1.0f;
		public AnimationCurve explosionScaleCurve;
	}
}