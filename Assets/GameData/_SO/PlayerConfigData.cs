using UnityEngine;

namespace GameName.Data
{
	[CreateAssetMenu(menuName = "Game/PlayerConfigData", fileName = "PlayerConfig", order = 1)]
	public class PlayerConfigData : ScriptableObject
	{
		public float energyChargeSpeed;
		public float minEnergyChargeStart;

		[Space]
		public float moveToDuration;
		public float moveToHeight;
		public AnimationCurve moveToPositionCurve;
		public AnimationCurve moveToHeightCurve;

	}
}