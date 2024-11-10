using GameName.Services;
using GameName.UI;
using GameName.Utilities;
using YB.HFSM;

namespace GameName.Core.HFSM
{
	public class MetaState : State
	{
		private SceneLoader _sceneLoader;
		private UILoader _uiLoader;

		public MetaState(SceneLoader sceneLoader, UILoader uiLoader)
		{
			_sceneLoader = sceneLoader;
			_uiLoader = uiLoader;
		}

		protected override void OnEnter()
		{
			_uiLoader.LoadScene(RuntimeConstants.Scenes.META);
		}
		protected override void OnExit()
		{
			_sceneLoader.Unload(RuntimeConstants.Scenes.META.Item1);
		}
	}
}