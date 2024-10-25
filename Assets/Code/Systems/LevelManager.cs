using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GA.GArkanoid
{
	public class LevelManager : MonoBehaviour
	{
		private Inputs _inputs;

		public Ball CurrentBall { get; private set; }
		public Paddle CurrentPaddle { get; private set; }
		public Block[] Blocks { get; private set; }
		public Wall[] Walls { get; private set; }

		private void Awake()
		{
			_inputs = new Inputs();

			if (CurrentBall == null)
			{
				CurrentBall = FindObjectOfType<Ball>();
			}

			if (CurrentPaddle == null)
			{
				CurrentPaddle = FindObjectOfType<Paddle>();
			}

			if (Blocks == null)
			{
				Blocks = FindObjectsOfType<Block>();
			}

			if (Walls == null)
			{
				Walls = FindObjectsOfType<Wall>();
			}
		}

		private void OnEnable()
		{
			_inputs.Game.Enable();
			_inputs.Game.Pause.performed += OnPausePerformed;
		}

		private void OnDisable()
		{
			_inputs.Game.Pause.performed -= OnPausePerformed;
			_inputs.Game.Disable();
		}

		private void Start()
		{
			CurrentBall.Setup(this);
			CurrentPaddle.Setup(this);

			foreach (Block block in Blocks)
			{
				block.Setup(this);
			}

			foreach (Wall wall in Walls)
			{
				wall.Setup(this);
			}
		}

		private void OnPausePerformed(InputAction.CallbackContext context)
		{
			GameManager.ChangeState(State.StateType.Pause);
		}

		public void ResetBall()
		{
			CurrentBall.Stop();
			CurrentPaddle.ResetBall();
		}
	}
}
