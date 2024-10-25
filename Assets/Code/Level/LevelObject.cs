using UnityEngine;

namespace GA.GArkanoid
{
	public abstract class LevelObject : MonoBehaviour
	{
		private LevelManager _levelManager;

		public LevelManager LevelManager => _levelManager;

		public virtual void Setup(LevelManager levelManager)
		{
			_levelManager = levelManager;
		}
	}
}