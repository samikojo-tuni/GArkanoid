using UnityEngine;

namespace GA.GArkanoid
{
	public class Wall : MonoBehaviour
	{
		[SerializeField] private Vector2 _normal;

		private void Start()
		{
			_normal.Normalize();
		}

		public Vector2 Normal { get { return _normal; } }
	}
}