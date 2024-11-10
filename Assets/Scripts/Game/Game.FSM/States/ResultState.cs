using GameName.UI;
using GameName.Utilities;
using System;
using UnityEngine;
using YB.HFSM;

namespace GameName.Game.HFSM
{
	public class ResultState : State
	{
		public event Action OnResultComplete;

		private GameInstance _game;
		private PlayerBehaviour _player;
		private TargetBehaviour _target;
		private RoadBehaviour _road;
		private UIGame _uiGame;

		private bool _isGameOver;

		public ResultState(GameInstance game, PlayerBehaviour player, RoadBehaviour road, UIGame uiGame, TargetBehaviour target)
		{
			_game = game;
			_player = player;
			_road = road;
			_uiGame = uiGame;
			_target = target;
		}

		protected override void OnEnter()
		{
			if (_player.IsEnergyNotEnough)
			{
				_game.SetPause(true);

				GameLoss();

				return;
			}

			_player.OnMoveToComplete += OnMoveToComplete;

			if (_road.CheckObstacle(out Vector3 nearestObstaclePosition))
			{
				_isGameOver = false;
				_player.MoveTo(new Vector3(_player.transform.position.x, _player.transform.position.y, nearestObstaclePosition.z - RuntimeConstants.NEAREST_OBSTACLE_OFFSET), true);
			}
			else
			{
				_isGameOver = true;
				_player.MoveTo(_target.GoalPosition);
			}
		}
		protected override void OnExit()
		{
			_player.OnMoveToComplete -= OnMoveToComplete;
		}

		private void OnMoveToComplete()
		{
			if (_isGameOver)
			{
				GameWin();
			}
			else
			{
				OnResultComplete?.Invoke();
			}
		}

		private void GameLoss()
		{
			_uiGame.ShowResultScreen("<color=red>Loss</color>");
		}
		private void GameWin()
		{
			_uiGame.ShowResultScreen("<color=green>Win</color>");
		}
	}
}