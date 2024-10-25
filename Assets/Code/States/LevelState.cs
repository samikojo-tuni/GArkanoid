using UnityEngine;

namespace GA.GArkanoid.State
{
	public class LevelState : GameStateBase
	{
		public override StateType Type => StateType.Level;

		public override string SceneName => "Level";

		public LevelState()
		{
			// TODO: Add the rest of the valid targets!
			AddTargetState(StateType.MainMenu);
			AddTargetState(StateType.Pause);
		}

		public override void OnEnter(bool forceLoad = false)
		{
			base.OnEnter(forceLoad);

			// Always resume game when entering the level state
			Time.timeScale = 1;
		}
	}
}