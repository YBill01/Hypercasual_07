using YB.HFSM;

namespace GameName.Core.HFSM
{
	public class CoreStateMachine : StateMachine
	{
		public CoreStateMachine(params State[] states) : base(states)
		{
		}
	}
}