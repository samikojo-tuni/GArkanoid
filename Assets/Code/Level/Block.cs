using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GA.GArkanoid
{
	public class Block : LevelObject
	{
		[SerializeField] private int _score = 1;

		private SpriteRenderer _renderer;

		public int Score { get { return _score; } }

		// TODO: Implement breaking blocks and adding score!
		// TODO: Use Health for the block!

		private void Awake()
		{
			_renderer = GetComponent<SpriteRenderer>();
		}

		private void Update()
		{
			if (CollisionCheck(LevelManager.CurrentBall))
			{
				// There was a collision, destroy the block!
				Break();
			}
		}

		private bool CollisionCheck(Ball ball)
		{
			Bounds bounds = _renderer.bounds;
			Physics.Hit hit = Physics.Intersects(bounds, ball.transform.position);

			if (hit == null)
			{
				return false;
			}

			// The ball hit this block! Let's bounce it
			LevelManager.CurrentBall.Bounce(hit.Normal);
			return true;
		}

		public void Break()
		{
			// TODO: Destroying blocks may not be the best idea...
			Destroy(gameObject);
			GameManager.Score += _score;
		}
	}
}
