using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GA.GArkanoid
{
	public class Ball : LevelObject
	{
		[SerializeField] private float _speed = 5;
		private Inputs _inputs;
		private Transform _transform;
		private Vector2 _velocity = Vector2.zero;

		private void Awake()
		{
			_inputs = new Inputs();
			_transform = transform;
		}

		private void OnEnable()
		{
			_inputs.Game.Enable();
		}

		private void OnDisable()
		{
			_inputs.Game.Disable();
		}

		// Update is called once per frame
		// Implement game logic here
		void Update()
		{
			if (_inputs.Game.Launch.WasPerformedThisFrame())
			{
				Launch();
			}

			_transform.position += new Vector3(_velocity.x, _velocity.y, 0) * _speed * Time.deltaTime;
		}

		private void Launch()
		{
			transform.parent = null;
			_velocity = (Random.insideUnitCircle + Vector2.up * 1.5f).normalized;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			Wall wall = other.GetComponent<Wall>();
			if (wall == null)
			{
				// Did not collide with wall, exit.
				return;
			}

			// Was the wall hazard? If so, destroy the ball!
			if (wall.IsHazard)
			{
				LevelManager.ResetBall();
				GameManager.Lives--;
				return;
			}

			Vector2 normal = wall.Normal;
			Bounce(normal);
		}

		#region Public interface

		public void Bounce(Vector2 normal)
		{
			Vector2 u = Vector2.Dot(_velocity, normal) * normal;
			Vector2 w = _velocity - u;
			_velocity = w - u;
		}

		public void Stop()
		{
			// Stop ball's movement
			_velocity = Vector2.zero;
		}

		#endregion Public interface
	}
}