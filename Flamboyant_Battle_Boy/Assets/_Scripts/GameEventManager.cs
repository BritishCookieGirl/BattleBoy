using UnityEngine;
using System.Collections;

public static class GameEventManager
{

	public delegate void GameEvent ();

	public static event GameEvent LevelStart, LevelComplete;

	// Use to notify subscribed objects that level has reset
	public static void TriggerLevelStart ()
	{
		if (LevelStart != null) {
			LevelStart ();
		}
	}

	// Use to notify subscribed objects that level has finished
	public static void TriggerLevelComplete ()
	{
		if (LevelComplete != null) {
			LevelComplete ();
		}
	}

}
