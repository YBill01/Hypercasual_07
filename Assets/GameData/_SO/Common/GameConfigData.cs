using UnityEngine;

namespace GameName.Data
{
	[CreateAssetMenu(menuName = "Game/Common/GameConfigData", fileName = "GameConfig", order = 0)]
	public class GameConfigData : ScriptableObject
	{
		public LevelConfigData[] levels;
	}
}