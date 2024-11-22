using System.Collections;
using System.Collections.Generic;
using GA.GArkanoid.Error;
using GA.GArkanoid.Persistance;
using UnityEngine;

namespace GA.GArkanoid
{
	public class Block : LevelObject
	{
		[SerializeField] private int _score = 1;
		[SerializeField] private ParticleSystem _breakEffectPrefab;
		private AudioSource _audioSource;

		private SpriteRenderer _renderer;
		private bool _isEnabled = true;

		public int Score { get { return _score; } }

		public bool IsEnabled
		{
			get => _isEnabled;
			set
			{
				_isEnabled = value;
				_renderer.enabled = _isEnabled;
			}
		}
		// TODO: Use Health for the block!

		private void Awake()
		{
			_renderer = GetComponent<SpriteRenderer>();
			_audioSource = GetComponent<AudioSource>();
		}

		private void Update()
		{
			if (!IsEnabled) return;
			
			Physics.Hit hit = CollisionCheck(LevelManager.CurrentBall);
			if (hit != null)
			{
				// The ball hit this block! Let's bounce it
				LevelManager.CurrentBall.Bounce(hit.Normal);
				// There was a collision, destroy the block!
				Break();

				if (_breakEffectPrefab != null)
				{
					ParticleSystem breakEffect = Instantiate(_breakEffectPrefab, transform.position, Quaternion.identity);
					breakEffect.transform.up = hit.Normal;
					ParticleSystem.MainModule main = breakEffect.main;
					main.startColor = _renderer.color;
					breakEffect.Play();

					// Destroys the effect after the duration of the particle system
					// Destroy(breakEffect.gameObject, main.duration);

					if (_audioSource != null)
					{
						// TODO: When health is implemented for blocks, implement playing hit sound when 
						// the block is not destroyed.
						AudioClip clip = GameManager.GetAudioClip(AudioType.Destroy);
						if (clip != null)
						{
							_audioSource.clip = clip;
							_audioSource.PlayOneShot(clip);
						}
					}
				}
			}
		}

		private Physics.Hit CollisionCheck(Ball ball)
		{
			Bounds bounds = _renderer.bounds;
			return Physics.Intersects(bounds, ball.transform.position);
		}

		public void Break()
		{
			IsEnabled = false;
			GameManager.Score += _score;
		}

		public override void Save(BinarySaver writer)
		{
			writer.WriteString(ID);
			writer.WriteBool(IsEnabled);
		}

		public override void Load(BinarySaver reader)
		{
			bool isActive = reader.ReadBool();
			IsEnabled = isActive;
		}
	}
}
