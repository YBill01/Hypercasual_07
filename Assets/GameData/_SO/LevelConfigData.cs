using UnityEngine;

namespace GameName.Data
{
	[CreateAssetMenu(menuName = "Game/LevelConfigData", fileName = "LevelConfig", order = 0)]
	public class LevelConfigData : ScriptableObject
	{
		public int numObstacles;

		public float playerEnergy;
		public Vector3 playerStartPosition;

		public GameObject obstaclePrefab;
	}
}