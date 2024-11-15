using System;
using GA.GArkanoid.Error;
using GA.GArkanoid.Persistance;
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

		public override void Save(BinarySaver writer)
		{
			writer.WriteString(ID);
			writer.WriteVector3(transform.position);
		}

		public override void Load(BinarySaver reader)
		{
			string id = reader.ReadString();
			if (ID != id)
			{
				// Something went wrong! The ID's don't match.
				// Throwing an exception ends this method.
				throw new LoadException("Corrupted save file!");
			}

			transform.position = reader.ReadVector3();
		}
	}
}