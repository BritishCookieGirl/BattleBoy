using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

	//MERGED TIMER INTO THIS CLASS, MIGHT MOVE IT BACK OUT, 
	//Did BREAK OFF COMBO/SCORE MANAGEMENT TO SEPARATE MANAGEMENT OBJECTS

	public float bonusTime;
	public static float currentTime, remainingTime;
	private static float startTime, endTime;

	//Event Declarations

	public delegate void TimeEventHandler (float time);
	public static event TimeEventHandler TimeChanged;
	
	public delegate void GameEvent();
	public static event GameEvent LevelStart, LevelComplete;

	//End Events Declarations

	void Awake ()
	{

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


	// Calculates time and raises event after combo changes
	public void UpdateTime (float time)
	{

		currentTime = time;
		remainingTime = bonusTime - currentTime;

		// Check for remaining time in level
		if (remainingTime < 0) {
			TriggerLevelComplete ();
		}

		// Fire time event with current time
		if (TimeChanged != null) {
			TimeChanged (remainingTime);
		}
	}
	
	public static void TriggerLevelStart() {
		
		startTime = Time.time;
		
		if (LevelStart != null) {
			LevelStart();	
		}
	}
	
		// Use to notify subscribed objects that level has finished
	public static void TriggerLevelComplete() {
		
		endTime = Time.time;
		
		if (LevelComplete != null) {
			LevelComplete();	
		}
	}

//	// Called Automatically anytime level starts - set default variables here
//	private void LevelStart ()
//	{
//		startTime = Time.time;
//	}
//
//	// Called automatically anytime level finishes - set win/lose conditions here
//	private void LevelComplete ()
//	{
//		endTime = Time.time;
//	}

}
