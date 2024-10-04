using UnityEngine;

namespace GA.GArkanoid
{
	public static class GameManager
	{
		// A static constructor is used to initialize any static data, 
		// or to perform a particular action that needs to be performed once only.
		// It is called automatically before the first instance is created or any 
		// static members are referenced.
		static GameManager()
		{
			Lives = 3;
			Score = 0;
			CurrentLevel = 1;
		}

		public static int Score { get; set; }
		public static int Lives { get; set; }
		public static int CurrentLevel { get; private set; }

		// TODO: This should be done properly!
		// Implement LevelManager!
		public static Ball CurrentBall { get; set; }
		// TODO: Add Game state!
	}
}