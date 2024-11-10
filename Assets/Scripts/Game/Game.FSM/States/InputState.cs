using UnityEngine;
using YB.HFSM;

namespace GameName.Game.HFSM
{
	public class InputState : State
	{
		private GameInstance _game;
		private PlayerBehaviour _player;
		private InputController _inputController;

		public InputState(GameInstance game, PlayerBehaviour player, InputController inputController)
		{
			_game = game;
			_player = player;
			_inputController = inputController;
		}

		protected override void OnEnter()
		{
			_inputController.OnAttack += OnAttack;
		}
		protected override void OnExit()
		{
			_inputController.OnAttack -= OnAttack;
		}

		private void OnAttack(bool value)
		{
			if (_game.IsPaused)
			{
				return;
			}

			if (value)
			{
				_player.ShootStart();
			}
			else
			{
				_player.ShootEnd();
			}
		}
	}
}