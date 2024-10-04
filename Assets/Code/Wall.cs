using UnityEngine;

namespace GA.GArkanoid
{
	public class Wall : MonoBehaviour
	{
		[SerializeField] private Vector2 _normal;
		[SerializeField] private bool _isHazard = false;

		private void Start()
		{
			_normal.Normalize();
		}

		public Vector2 Normal { get { return _normal; } }

		public bool IsHazard { get { return _isHazard; } }
	}
}