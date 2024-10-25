using UnityEngine;

namespace GA.GArkanoid
{
	public class Mover : MonoBehaviour, IMover
	{
		[field: SerializeField]
		public float Speed
		{
			get;
			private set;
		}

		/// <summary>
		/// Move a GameObject.
		/// </summary>
		/// <param name="direction">Direction of the movement. The vector has to
		/// be normalized!</param>
		public void Move(Vector2 direction)
		{
			transform.position += (Vector3)direction * Speed * Time.deltaTime;
		}
	}
}