using UnityEngine;
using System.Collections;

public static class ScoreManager {
	
	public static int score; 
	public static int totalScore;
	private static int threshold = 1000;
	private static int level = 1;
	private static int winScore = 100000;
	
	public delegate void PointEventHandler(int points);
	public static event PointEventHandler ScoreChanged;
	public static event PointEventHandler ThresholdReached;
	public static event PointEventHandler WinReached;
	
	
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
		
		if (score >= threshold) {
			
			if (ThresholdReached != null) {
				ThresholdReached(level);
			}
			
			level++;
			threshold += 1000;
		}
		
	}
	
	public static void UpdateScore (int points) {
				
		if (ScoreChanged != null) {
			ScoreChanged(points);	
		}
	}
	
	public static bool PurchaseItem(int points) {
		
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
		threshold = 500;
		level = 1;
		
		if (ScoreChanged != null) {
			ScoreChanged(score);	
		}
	}
	
	// Calculations for end of game bonus points
	public static int CalculateFinalScore() {
		
		int timeBonus = (int)GameManager.remainingTime * 10;
		
		totalScore += timeBonus;
		totalScore += score;
		
		UpdateScore(totalScore);
		
		if (totalScore >= winScore && WinReached != null) {
			WinReached(totalScore);
		}
		
		return totalScore;
	}
	
	private static void LevelStart() {
		ResetScore();
	}
	
	private static void LevelComplete() {

	}
	
	private static void StoreOpen() {
		UpdateScore(totalScore);
	}
}
