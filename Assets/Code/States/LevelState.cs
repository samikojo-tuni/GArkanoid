using UnityEngine;
using UnityEngine.SceneManagement;

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

		protected override void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadMode)
		{
			base.OnSceneLoaded(loadedScene, loadMode);

			// TODO: Load the correct level prefab.
			LevelManager levelPrefab = GameManager.GetLevelPrefab(GameManager.LoadedLevelIndex);
			if (levelPrefab == null)
			{
				Debug.LogError($"Can't find a prefab for a level with index {GameManager.LoadedLevelIndex}");
				return;
			}

			Object.Instantiate(levelPrefab, Vector3.zero, Quaternion.identity);
		}
	}
}