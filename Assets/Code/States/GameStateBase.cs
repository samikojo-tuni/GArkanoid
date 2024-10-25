using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

namespace GA.GArkanoid.State
{
	public abstract class GameStateBase
	{
		public abstract StateType Type { get; }

		public abstract string SceneName { get; }

		// Should the associated scene be loaded additively or not
		public virtual bool IsAdditive { get { return false; } }

		private List<StateType> _targetStates = new List<StateType>();

		protected GameStateBase()
		{
			// TODO: Do any initialization here
		}

		protected void AddTargetState(StateType targetState)
		{
			if (!_targetStates.Contains(targetState))
			{
				_targetStates.Add(targetState);
			}
		}

		protected bool RemoveTargetState(StateType targetState)
		{
			return _targetStates.Remove(targetState);
		}

		/// <summary>
		/// Activates the state. Loads associated scene by default.
		/// </summary>
		/// <param name="forceLoad">The loading of the scene is forced even if
		/// the target scene is already loaded.</param>
		public virtual void OnEnter(bool forceLoad = false)
		{
			Scene currentScene = SceneManager.GetActiveScene();

			// If forceLoad is true or target state is not currently loaded, load the target state.
			if (forceLoad || currentScene.name.ToLower() != SceneName.ToLower())
			{
				// Give more resources for the loading functionality
				Application.backgroundLoadingPriority = ThreadPriority.High;

				LoadSceneMode loadMode;
				// if (IsAdditive == true)
				// {
				// 	loadMode = LoadSceneMode.Additive;
				// }
				// else
				// {
				// 	loadMode = LoadSceneMode.Single;
				// }
				// Same as above!
				loadMode = IsAdditive
					? LoadSceneMode.Additive
					: LoadSceneMode.Single;

				SceneManager.sceneLoaded += OnSceneLoaded;
				SceneManager.LoadSceneAsync(SceneName, loadMode);
			}
		}

		public virtual void OnExit()
		{
			// This system requires you to unload additively loaded scenes manually.
			if (IsAdditive)
			{
				SceneManager.UnloadSceneAsync(SceneName);
			}
		}

		public bool IsValidTargetState(StateType targetState)
		{
			return _targetStates.Contains(targetState);
		}

		protected virtual void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadMode)
		{
			// Unsubscribe from sceneLoaded event when event is triggered.
			SceneManager.sceneLoaded -= OnSceneLoaded;

			Application.backgroundLoadingPriority = ThreadPriority.Normal;
		}
	}
}