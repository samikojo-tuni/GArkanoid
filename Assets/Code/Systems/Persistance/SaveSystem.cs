using System;
using System.IO;
using UnityEngine;

namespace GA.GArkanoid.Persistance
{
	public class SaveSystem
	{
		public const string FileExtension = ".save";

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
	}
}