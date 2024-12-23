using GA.GArkanoid.State;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using GA.GArkanoid.Data;
using GA.GArkanoid.Persistance;

namespace GA.GArkanoid
{
	public static class GameManager
	{
		private static List<GameStateBase> _states = new List<GameStateBase>();

		public static event System.Action<StateType> EnteredState;
		public static event System.Action<StateType> ExitedState;

		// Stores level prefabs.
		private static LevelData _levelData = null;
		private static AudioData _audioData = null;

		public static int LoadedLevelIndex
		{
			get; set;
		}

		public static SaveSystem SaveSystem { get; private set; }

		// A static constructor is used to initialize any static data, 
		// or to perform a particular action that needs to be performed once only.
		// It is called automatically before the first instance is created or any 
		// static members are referenced.
		static GameManager()
		{
			Lives = 3;
			Score = 0;
			LoadedLevelIndex = 0;

			InitializeStates();

			SaveSystem = new SaveSystem();
		}

		private static void InitializeStates()
		{
			_states.Add(new MainMenuState());
			_states.Add(new LevelState());
			_states.Add(new OptionsState());
			_states.Add(new PauseState());

			GameStateBase initialState = _states[0];

			// This is executed only in Unity Editor. In build, this part of the code is stripped away.
#if UNITY_EDITOR
			string activeSceneName = SceneManager.GetActiveScene().name.ToLower();

			foreach (GameStateBase state in _states)
			{
				string stateSceneName = state.SceneName.ToLower();

				if (activeSceneName == stateSceneName)
				{
					initialState = state;
					break;
				}
			}
#endif

			CurrentState = initialState;
			CurrentState.OnEnter();
		}

		public static int Score { get; set; }
		public static int Lives { get; set; }

		public static GameStateBase CurrentState { get; private set; }
		public static GameStateBase PreviousState { get; private set; }

		private static GameStateBase GetState(StateType stateType)
		{
			foreach (GameStateBase state in _states)
			{
				// If the inspected state's type matches the type received as an argument, return the state.
				if (state.Type == stateType)
				{
					return state;
				}
			}

			// If matching state is not found, return null as an error value.
			return null;
		}

		public static bool ChangeState(StateType targetStateType, bool forceLoad = false)
		{
			// Is the transition from current state to target state legal?
			if (!CurrentState.IsValidTargetState(targetStateType))
			{
				// It's not legal!
				Debug.Log($"Transition from {CurrentState.Type} to {targetStateType} is not allowed!");
				return false;
			}

			GameStateBase targetState = GetState(targetStateType);
			if (targetState == null)
			{
				// The target state does not exist!
				Debug.Log($"Target state {targetStateType} is not found.");
				return false;
			}

			// Deactivate previous state, update current state and activate the new state.
			// TODO: Implement proper transitions!

			PreviousState = CurrentState;

			CurrentState.OnExit();
			if (ExitedState != null)
			{
				ExitedState(CurrentState.Type);
			}

			CurrentState = targetState;

			CurrentState.OnEnter(forceLoad);
			if (EnteredState != null)
			{
				EnteredState(CurrentState.Type);
			}

			return true;
		}

		public static LevelManager GetLevelPrefab(int index)
		{
			if (_levelData == null)
			{
				_levelData = Resources.Load<LevelData>("LevelData");
			}

			return _levelData.GetLevelPrefab(index);
		}

		public static AudioClip GetAudioClip(AudioType type)
		{
			// Lazy load audio data, load it when it is first needed.
			if (_audioData == null)
			{
				_audioData = Resources.Load<AudioData>("AudioData");
			}

			return _audioData.GetClip(type);
		}

		public static void Save(BinarySaver writer)
		{
			writer.WriteInt32(LoadedLevelIndex);
			writer.WriteInt32(Lives);
			writer.WriteInt32(Score);
		}

		public static void Load(BinarySaver reader)
		{
			LoadedLevelIndex = reader.ReadInt32();
			Lives = reader.ReadInt32();
			Score = reader.ReadInt32();

			// TODO: This could be done much more robust way!
			// Let's fix this in the early spring!
		}
	}
}