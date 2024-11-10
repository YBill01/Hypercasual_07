using TMPro;
using UnityEngine;

namespace GameName.UI.Utilities
{
	[RequireComponent(typeof(TMP_Text))]
	public class UIVersionComponent : MonoBehaviour
	{
		private void Start()
		{
			GetComponent<TMP_Text>().text = $"v{Application.version}";
		}
	}
}