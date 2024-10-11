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
		}
	}
}