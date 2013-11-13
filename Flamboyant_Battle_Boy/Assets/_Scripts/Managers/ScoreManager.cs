using UnityEngine;
using System.Collections;

public static class ScoreManager {
	
	public static int score; 
	public static int totalScore;
	
	public delegate void PointEventHandler(int points);
	public static event PointEventHandler ScoreChanged;
	
	static ScoreManager() {
		GameManager.LevelStart += LevelStart;
		GameManager.LevelComplete += LevelComplete;
		GameManager.StoreOpen += StoreOpen;
	}


	// Calculates score and raises event after score changes
	public static void AddToScore (int points) {
		if (ComboManager.multiplier > 1) {
			points = (int)(points * ComboManager.multiplier);
		}
		
		score += points;
		UpdateScore(score);		
		
	}
	
	public static void UpdateScore (int points) {
				
		if (ScoreChanged != null) {
			ScoreChanged(points);	
		}
	}
	
	public static bool PurchaseItem(int points) {
		//score += 1000;
		if (totalScore < points) {
			return false;
		}
		
		totalScore -= points;
		
		if (ScoreChanged != null) {
			ScoreChanged(totalScore);	
		}
		
		return true;
	}
	
	// Manually reset score 
	public static void ResetScore() {
		score = 0;
		
		if (ScoreChanged != null) {
			ScoreChanged(score);	
		}
	}
	
	// Calculations for end of game bonus points
	public static int CalculateFinalScore() {
		
		int timeBonus = (int)GameManager.remainingTime * 100;
		
		totalScore += timeBonus;
		totalScore += score;
		
		UpdateScore(totalScore);
		
		return totalScore;
	}
	
	private static void LevelStart() {
		ResetScore();
	}
	
	private static void LevelComplete() {

	}
	
	private static void StoreOpen() {
		MonoBehaviour.print("score updating at store open to: " + totalScore);
		UpdateScore(totalScore);
	}
}
