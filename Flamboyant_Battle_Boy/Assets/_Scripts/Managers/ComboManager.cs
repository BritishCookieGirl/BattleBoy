using UnityEngine;
using System.Collections;

public static class ComboManager {
	
	public static int currentCombo, maxCombo;
	public static float multiplier;
	private static bool firstAttack;
	private static float comboStart, comboTime;
	
	public delegate void ComboEventHandler(int points);
	public static event ComboEventHandler ComboChanged;
	
	static ComboManager() {
		maxCombo = 5;
		comboTime = 3;
		GameManager.LevelStart += LevelStart;
		GameManager.LevelComplete += LevelComplete;
	}
	
	// Calculates combo and raises event after combo changes
	public static void UpdateCombo (int points) {
		int tempCombo = currentCombo + points;
		
		if (ComboExpired() || tempCombo > 5) {
			ResetCombo();
		} else {			
			currentCombo = Mathf.Clamp(tempCombo,0,maxCombo);
		}
		
		if (firstAttack) {
			comboStart = GameManager.currentTime;	
			firstAttack = false;
		} 
		
		CalculateMultiplier();
		
		if (ComboChanged != null) {
			ComboChanged(currentCombo);	
		}
	}
	
	// Check if combo entry time has expired
	private static bool ComboExpired() {
	if (GameManager.currentTime > (comboStart + comboTime)) {
		return true;
		}
		return false;
	}
	
	// Manually reset combo
	public static void ResetCombo() {
		currentCombo = 0;
		firstAttack = true;
		
		if (ComboChanged != null) {
			ComboChanged(currentCombo);	
		}
	}
	
	private static void CalculateMultiplier() {
		multiplier = Mathf.Clamp(currentCombo, 1, maxCombo);
	}
	
	private static void LevelStart() {
		ResetCombo();
	}
	
	private static void LevelComplete() {
		
	}
}
