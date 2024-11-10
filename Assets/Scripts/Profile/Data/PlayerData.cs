using BayatGames.SaveGameFree.Types;
using System;

namespace GameName.Core
{
	[Serializable]
	public class PlayerData : IProfileData
	{
		public DateTime localTime;

		

		public void SetDefault()
		{
			localTime = new DateTime();

			
		}

		
	}
}