using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

	//MERGED TIMER INTO THIS CLASS, MIGHT MOVE IT BACK OUT, MIGHT BREAK OFF POINT,COMBO,SCORE MANAGEMENT TO SEPARATE MANAGEMENT OBJECTS

	public float bonusTime, comboTime;
	public static float currentTime;
	public int currentCombo, maxCombo, score;
	private float startTime, endTime, remainingTime, comboStart;
	private bool firstAttack;


	//Event Declarations
	public delegate void PointEventHandler (int points);

	public static event PointEventHandler ScoreChanged, ComboChanged;

	public delegate void TimeEventHandler (float time);

	public static event TimeEventHandler TimeChanged;

	//End Events Declarations

	void Awake ()
	{
		GameEventManager.LevelStart += LevelStart;
		GameEventManager.LevelComplete += LevelComplete;
	}

	// Use this for initialization
	void Start ()
	{
		GameEventManager.TriggerLevelStart ();
	}

	// Update is called once per frame
	void Update ()
	{
		UpdateTime (Time.time);
	}

	// Calculates score and raises event after score changes
	public void UpdateScore (int points)
	{
		int multiplier = maxCombo - currentCombo;
		score += (points * (multiplier + 1));

		if (ScoreChanged != null) {
			ScoreChanged (score);
		}
	}

	// Calculates combo and raises event after combo changes
	public void UpdateCombo (int points)
	{
		if (firstAttack) {
			comboStart = Time.time;
			firstAttack = false;
		}

		currentCombo = Mathf.Clamp ((currentCombo + points), 0, maxCombo);
		if (ComboChanged != null) {
			ComboChanged (currentCombo);
		}
	}

	// Calculates time and raises event after combo changes
	public void UpdateTime (float time)
	{

		currentTime = time;
		remainingTime = bonusTime - currentTime;

		// Check if combo entry time has expired
		if (currentTime > (comboStart + comboTime)) {
			ResetCombo ();
		}

		// Check for remaining time in level
		if (remainingTime < 0) {
			GameEventManager.TriggerLevelComplete ();
		}

		// Fire time event with current time
		if (TimeChanged != null) {
			TimeChanged (remainingTime);
		}
	}

	// Called Automatically anytime level starts - set default variables here
	private void LevelStart ()
	{
		startTime = Time.time;
		ResetScore ();
		ResetCombo ();
	}

	// Called automatically anytime level finishes - set win/lose conditions here
	private void LevelComplete ()
	{
		endTime = Time.time;
		CalculateFinalScore ();
	}

	// Manually reset score
	public void ResetScore ()
	{
		UpdateScore (0);
	}

	// Manually reset combo
	public void ResetCombo ()
	{
		currentCombo = maxCombo;
		UpdateCombo (currentCombo);
		firstAttack = true;
	}

	// Calculations for end of game bonus points
	public void CalculateFinalScore ()
	{
		ResetCombo ();
		int timeBonus = (int)remainingTime * 100;
		UpdateScore (timeBonus);
	}
}