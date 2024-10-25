using System;
using UnityEngine;

namespace GA.GArkanoid
{
	public class Paddle : LevelObject
	{
		[SerializeField] private Transform _startPoint;

		private IMover _mover;
		private Inputs _inputs;

		#region Unity messages
		private void Awake()
		{
			_mover = GetComponent<IMover>();
			_inputs = new Inputs();
		}

		private void OnEnable()
		{
			_inputs.Game.Enable();
		}

		private void OnDisable()
		{
			_inputs.Game.Disable();
		}

		private void Update()
		{
			// Read the input 
			float input = _inputs.Game.Move.ReadValue<float>();
			_mover.Move(new Vector2(input, 0));
		}

		#endregion

		public void ResetBall()
		{
			LevelManager.CurrentBall.transform.parent = _startPoint;
			LevelManager.CurrentBall.transform.localPosition = Vector3.zero;
		}

		public override void Setup(LevelManager levelManager)
		{
			base.Setup(levelManager);

			ResetBall();
		}
	}
}