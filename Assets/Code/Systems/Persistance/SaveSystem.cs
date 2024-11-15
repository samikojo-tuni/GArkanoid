using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GA.GArkanoid.Persistance
{
	public class SaveSystem
	{
		public const string FileExtension = ".save";
		public const string QuickSaveSlot = "QuickSave";

		private BinarySaver _saver = null;

		public string SaveFolder 
		{
			get 
			{
				return Path.Combine(Application.persistentDataPath, "Save");
			}
		}

		public SaveSystem()
		{
			try
			{
				// Create the save folder if it doesn't exist.
				if (!Directory.Exists(SaveFolder))
				{
					Directory.CreateDirectory(SaveFolder);
				}
			}
			catch(IOException e)
			{
				Debug.LogException(e);
			}
			catch(Exception e)
			{
				// Gotta catch 'em all!
				Debug.LogException(e);
			}
		}

		public void Save(string saveSlot)
		{
			_saver = new BinarySaver();
			string saveFilePath = Path.Combine(SaveFolder, saveSlot + FileExtension);
			_saver.PrepareWrite(saveFilePath);

			GameManager.Save(_saver);

			// TODO: Get rid of the Current variable.
			LevelManager.Current.Save(_saver);

			_saver.FinalizeWrite();
		}

		public void Load(string saveSlot)
		{
			_saver = new BinarySaver();
			string saveFilePath = Path.Combine(SaveFolder, saveSlot + FileExtension);
			if (!_saver.PrepareRead(saveFilePath))
			{
				Debug.LogError("Can't read the save file!");
				return;
			}

			GameManager.Load(_saver);

			// Continue load after the level is loaded.
			LevelManager.LevelInitialized += LoadLevel;

			// Let's load the level state
			GameManager.ChangeState(State.StateType.Level);
		}

		private void LoadLevel(LevelManager levelManager)
		{
			LevelManager.LevelInitialized -= LoadLevel;
			if (levelManager != null)
			{
				levelManager.Load(_saver);
			}

			_saver.FinalizeRead();
			_saver = null;
		}
	}
}