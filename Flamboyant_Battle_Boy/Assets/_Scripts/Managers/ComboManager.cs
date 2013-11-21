using UnityEngine;
using System.Collections;

public static class ComboManager
{
    private static int currentComboString;
    public static int CurrentComboString { get { return currentComboString; } set { currentComboString = value; } }

    private static int currentCombo;
    public static int CurrentCombo { get { return currentCombo; } set { currentCombo = value; } }

    public static int maxCombo;
	public static float multiplier;
	private static bool firstAttack;
	private static float comboStart, comboTime;
	
	public delegate void ComboEventHandler(int points);
	public static event ComboEventHandler ComboChanged;
	
	static ComboManager() {
		maxCombo = 5;
		comboTime = 3;
		multiplier = 1;
		GameManager.LevelStart += LevelStart;
		GameManager.LevelComplete += LevelComplete;
	}
	
	// Calculates combo and raises event after combo changes
	public static void UpdateCombo () {
		
		if (firstAttack) {
			comboStart = GameManager.currentTime;	
			firstAttack = false;
		} 
		
		if (ComboExpired() || currentCombo > maxCombo) {
			ResetCombo();
		} else {			
			currentCombo = Mathf.Clamp(currentCombo,0,maxCombo);
		}
		
		CalculateMultiplier();
		
		if (ComboChanged != null) {
			ComboChanged(currentCombo);	
		}
	}
	
	public static void IncrementCombo() {
		
		if (currentCombo < maxCombo) {
			currentCombo += 1;
		}
		
		MonoBehaviour.print("Current Combo is " + currentCombo);
		MonoBehaviour.print("Max Combo is " + maxCombo);
		
		UpdateCombo();
	}
	
	public static void IncrementMaxCombo() {
		if (maxCombo < 10) {
			maxCombo += 1;
		}
	}
	// Check if combo entry time has expired
	public static bool ComboExpired() {
	if (GameManager.currentTime > (comboStart + comboTime)) {
		return true;
		}
		return false;
	}
	
	// Manually reset combo
	public static void ResetCombo() {
		MonoBehaviour.print("resetting combo");
		currentCombo = 0;
		firstAttack = true;
		
		if (ComboChanged != null) {
			ComboChanged(currentCombo);	
		}
	}
	
	private static void CalculateMultiplier() {
		multiplier = Mathf.Clamp(currentCombo, 1, maxCombo) ;
	}
	
	private static void LevelStart() {
		MonoBehaviour.print("Level start reseting combo");
		ResetCombo();
	}
	
	private static void LevelComplete() {
		
	}
}
