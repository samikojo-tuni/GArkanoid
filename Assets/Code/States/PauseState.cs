using UnityEngine;

namespace GA.GArkanoid.State
{
	public class PauseState : GameStateBase
	{
		public override StateType Type => StateType.Pause;

		public override string SceneName => "Options";

		public override bool IsAdditive => true;

		public PauseState()
		{
			AddTargetState(StateType.Level);
			AddTargetState(StateType.MainMenu);
		}

		public override void OnEnter(bool forceLoad = false)
		{
			base.OnEnter(forceLoad);

			// Pause the game
			Time.timeScale = 0;
		}
	}
}