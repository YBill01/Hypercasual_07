using UnityEngine;

namespace GameName.UI
{
	public class UIApp : MonoBehaviour
	{
		private void Awake()
		{
			DontDestroyOnLoad(this);
		}
	}
}