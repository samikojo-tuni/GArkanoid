using UnityEngine;

namespace GA.GArkanoid.Data
{
	[CreateAssetMenu(fileName = "LevelData", menuName = "Level Data")]
	public class LevelData : ScriptableObject
	{
		[SerializeField]
		private LevelManager[] _levelPrefabs;

		/// <summary>
		/// Returns the level prefab matching the index provided.
		/// </summary>
		/// <param name="index">Level's index</param>
		/// <returns>The level prefab or null in any error case.</returns>
		public LevelManager GetLevelPrefab(int index)
		{
			if (_levelPrefabs.Length == 0)
			{
				Debug.LogError("Level prefabs list is empty!");
				return null;
			}

			if (index < 0 || index >= _levelPrefabs.Length)
			{
				Debug.LogError("There's no level that matches the index.");
				return null;
			}

			return _levelPrefabs[index];
		}
	}
}