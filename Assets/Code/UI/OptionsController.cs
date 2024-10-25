using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GA.GArkanoid.UI
{
	public class OptionsController : MonoBehaviour
	{
		public void Save()
		{
			Debug.Log("Saving settings");
		}

		public void Close()
		{
			GameManager.ChangeState(GameManager.PreviousState.Type);
		}

		public void GoToMainMenu()
		{
			GameManager.ChangeState(State.StateType.MainMenu);
		}
	}
}
