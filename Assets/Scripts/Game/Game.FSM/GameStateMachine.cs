using YB.HFSM;

namespace GameName.Game.HFSM
{
	public class GameStateMachine : StateMachine
	{
		public GameStateMachine(params State[] states) : base(states)
		{
		}
	}
}