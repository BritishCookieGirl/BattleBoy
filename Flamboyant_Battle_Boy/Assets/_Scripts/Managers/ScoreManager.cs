using UnityEngine;
using System.Collections;

public static class ScoreManager {
	
	public static int score; 
	
	public delegate void PointEventHandler(int points);
	public static event PointEventHandler ScoreChanged;
	
	static ScoreManager() {
		GameManager.LevelStart += LevelStart;
		GameManager.LevelComplete += LevelComplete;
	}

	// Calculates score and raises event after score changes
	public static void UpdateScore (int points) {
		
		score += (int)(points * ComboManager.multiplier);
		
		if (ScoreChanged != null) {
			ScoreChanged(score);	
		}
	}
	
	// Manually reset score 
	public static void ResetScore() {
		UpdateScore (0);	
	}
	
	// Calculations for end of game bonus points
	public static void CalculateFinalScore() {
		
		int timeBonus = (int)GameManager.remainingTime * 100;
		UpdateScore(timeBonus);
	}
	
	private static void LevelStart() {
		ResetScore();
	}
	
	private static void LevelComplete() {
		CalculateFinalScore();
	}
	
}
