using UnityEngine;

namespace GA.GArkanoid.UI
{
	public class MainMenuController : MonoBehaviour
	{
		public void StartNewGame()
		{
			GameManager.ChangeState(State.StateType.Level);
		}

		public void Quit()
		{
			Application.Quit();
		}

		public void OpenOptions()
		{
			GameManager.ChangeState(State.StateType.Options);
		}
	}
}
