using GA.GArkanoid.State;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GA.GArkanoid.UI
{
	public class OptionsController : MonoBehaviour
	{
		[SerializeField] private Button[] _navigationButtons;

		private void OnEnable()
		{
			GameManager.EnteredState += OnEnteredState;
			GameManager.ExitedState += OnExitedState;
		}

		private void OnDisable()
		{
			GameManager.EnteredState -= OnEnteredState;
			GameManager.ExitedState -= OnExitedState;
		}

		private void Start()
		{
			ActivateNavigation();
		}

		private void OnExitedState(StateType type)
		{
			if (type == StateType.Options)
			{
				DeactivateNavigation();
			}
		}

		private void OnEnteredState(StateType type)
		{
			if (type == StateType.Options)
			{
				ActivateNavigation();
			}
		}

		private void ActivateNavigation()
		{
			for (int i = 0; i < _navigationButtons.Length; ++i)
			{
				Button button = _navigationButtons[i];
				button.navigation = new Navigation
				{
					mode = Navigation.Mode.Explicit,
					selectOnLeft = i == 0
						? _navigationButtons[_navigationButtons.Length - 1]
						: _navigationButtons[i - 1],
					selectOnRight = i == _navigationButtons.Length - 1
						? _navigationButtons[0]
						: _navigationButtons[i + 1]
				};
			}
			
			EventSystem.current.SetSelectedGameObject(_navigationButtons.Length > 0 
				? _navigationButtons[0].gameObject 
				: null);
		}

		private void DeactivateNavigation()
		{
			foreach (Button button in _navigationButtons)
			{
				button.navigation = new Navigation
				{
					mode = Navigation.Mode.None
				};
			}
		}

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
