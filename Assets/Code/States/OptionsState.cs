namespace GA.GArkanoid.State
{
	public class OptionsState : GameStateBase
	{
		public override StateType Type => StateType.Options;

		public override string SceneName => "Options";

		public override bool IsAdditive => true;

		public OptionsState() : base()
		{
			AddTargetState(StateType.MainMenu);
		}
	}
}