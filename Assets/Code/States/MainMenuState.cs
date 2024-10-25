namespace GA.GArkanoid.State
{
	public class MainMenuState : GameStateBase
	{
		public override StateType Type { get { return StateType.MainMenu; } }

		public override string SceneName => "MainMenu";

		public MainMenuState()
		{
			// TODO: Maybe some more targets?
			AddTargetState(StateType.Level);
			AddTargetState(StateType.Options);
		}
	}
}