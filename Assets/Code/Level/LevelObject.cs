using GA.GArkanoid.Persistance;
using UnityEngine;

namespace GA.GArkanoid
{
	public abstract class LevelObject : MonoBehaviour, ISaveable
	{
		[SerializeField]
		private string _id = null;

		private LevelManager _levelManager;

		public LevelManager LevelManager => _levelManager;

		public string ID => _id;

		public virtual void Load(BinarySaver reader)
		{
		}

		public virtual void Save(BinarySaver writer)
		{
		}

		public virtual void Setup(LevelManager levelManager)
		{
			_levelManager = levelManager;
		}

#if UNITY_EDITOR
// This code block will be removed from the build.
		public void ResetID()
		{
			_id = System.Guid.NewGuid().ToString();
			UnityEditor.EditorUtility.SetDirty(this);
		}
#endif
	}
}