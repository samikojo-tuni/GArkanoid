using System;
using System.Collections;
using System.Collections.Generic;
using GA.GArkanoid.Error;
using GA.GArkanoid.Persistance;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GA.GArkanoid
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager Current;
		public static event System.Action<LevelManager> LevelInitialized;

		private Inputs _inputs;

		public Ball CurrentBall { get; private set; }
		public Paddle CurrentPaddle { get; private set; }
		public Block[] Blocks { get; private set; }
		public Wall[] Walls { get; private set; }

		private void Awake()
		{
			_inputs = new Inputs();
			Current = this;

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
			_inputs.Game.QuickSave.performed += OnQuickSavePerformed;
			_inputs.Game.QuickLoad.performed += OnQuickLoadPerformed;
		}

		private void OnDisable()
		{
			_inputs.Game.Pause.performed -= OnPausePerformed;
			_inputs.Game.QuickSave.performed -= OnQuickSavePerformed;
			_inputs.Game.QuickLoad.performed -= OnQuickLoadPerformed;
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

			if (LevelInitialized != null)
			{
				LevelInitialized(this);
			}
		}

#if UNITY_EDITOR
		[ContextMenu("Reset IDs")]
		private void ResetIDs()
		{
			LevelObject[] saveables = GetComponentsInChildren<LevelObject>(includeInactive: true);
			foreach (LevelObject saveable in saveables)
			{
				saveable.ResetID();
			}
		}
#endif

		private void OnQuickLoadPerformed(InputAction.CallbackContext context)
		{
			throw new NotImplementedException();
		}

		private void OnQuickSavePerformed(InputAction.CallbackContext context)
		{
			string quickSaveSlot = SaveSystem.QuickSaveSlot;
			GameManager.SaveSystem.Save(quickSaveSlot);
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

		public void Save(BinarySaver writer)
		{
			CurrentBall.Save(writer);
			CurrentPaddle.Save(writer);

			// Save the number of blocks so we know how many blocks
			// should be loaded.
			writer.WriteInt32(Blocks.Length);
			foreach (Block block in Blocks)
			{
				block.Save(writer);
			}
		}

		public void Load(BinarySaver reader)
		{
			CurrentBall.Load(reader);
			CurrentPaddle.Load(reader);

			int blockCount = reader.ReadInt32();
			for (int i = 0; i < blockCount; ++i)
			{
				bool blockFound = false;
				string blockId = reader.ReadString();

				for (int j = 0; j < Blocks.Length; ++j)
				{
					Block block = Blocks[j];
					if (block.ID == blockId)
					{
						block.Load(reader);
						blockFound = true;
						// Exits the inner for-loop.
						break;
					}
				}

				if (!blockFound)
				{
					throw new LoadException("A block is missing from the level!");
				}
			}
		}
	}
}
