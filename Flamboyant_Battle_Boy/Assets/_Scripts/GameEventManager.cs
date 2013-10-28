using UnityEngine;
using System.Collections;

public static class GameEventManager {

	public delegate void GameEvent();
	
	public static event GameEvent LevelStart, LevelComplete;
	
	public static void TriggerLevelStart() {
		if (LevelStart != null) {
			LevelStart();	
		}
	}
	
	public static void TriggerLevelComplete() {
		if (LevelComplete != null) {
			LevelComplete();	
		}
	}
	
	
}
