using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

	//MERGED TIMER INTO THIS CLASS, MIGHT MOVE IT BACK OUT, 
	//Did BREAK OFF COMBO/SCORE MANAGEMENT TO SEPARATE MANAGEMENT OBJECTS

	public static float bonusTime;
	public static float currentTime, remainingTime, bonusEndTime;
	private static float startTime, endTime;
	private static bool levelRunning;
	//Event Declarations

	public delegate void TimeEventHandler (float time);
	public static event TimeEventHandler TimeChanged;
	
	public delegate void GameEvent();
	public static event GameEvent GameStart, GameOver, GameWon; 
	public static event GameEvent LevelStart, LevelComplete;
	public static event GameEvent StoreOpen, StoreClosed, CreditsOpen, CreditsClose;
	
	private static bool victory;
	//End Events Declarations

	void Awake ()
	{

	}

	// Use this for initialization
	void Start ()
	{
		bonusTime = 30;
		levelRunning = false;
		victory = false;
		bonusEndTime = bonusTime;
		TriggerGameStart ();
	}

	// Update is called once per frame
	void Update ()
	{
		UpdateTime (Time.time);
	}


	// Calculates game time 
	public void UpdateTime (float time)
	{

		currentTime = time;
		
		remainingTime = Mathf.Clamp(bonusEndTime - currentTime, 0, bonusEndTime);

		// Check for remaining time in level
//		if (levelRunning && remainingTime < 0) {
//			print ("out of time!");
//			TriggerLevelComplete ();
//		}

		// Fire time event with current time
		if (levelRunning && TimeChanged != null) {
			TimeChanged (remainingTime);
		}
	}
	
	public static void TriggerGameStart() {
		if (GameStart != null) {
			//print ("GameStart Event Dispatched");
			GameStart();
		}
	}
	
	public static void TriggerCreditsStart() {
		if (CreditsOpen != null) {
			//print ("CreditsOpen Event Dispatched");
			CreditsOpen();
		}
	}
	
	public static void TriggerCreditsClose() {
		if (CreditsClose != null) {
			//print ("CreditsOpen Event Dispatched");
			CreditsClose();
		}
	}
	
	public static void TriggerLevelStart()
    {	
		startTime = Time.time;
		bonusEndTime = startTime + bonusTime;
		levelRunning = true;
		
		if (LevelStart != null) {
			//print ("LevelStart Event Dispatched");
			LevelStart();	
		}
	}
	
		// Use to notify subscribed objects that level has finished
	public static void TriggerLevelComplete() {
		
		endTime = Time.time;
		levelRunning = false;
		
		if (LevelComplete != null) {
			//print ("LevelComplete Event Dispatched");
			LevelComplete();	
		}

	}
	
	public static void TriggerStoreActive() {
		
		if (StoreOpen != null) {
			//print ("StoreOpen Event Dispatched");
			StoreOpen();	
		}
	}
	
	public static void TriggerStoreClosed() {
		
		if (StoreClosed != null) {
			//print ("StoreClosed Event Dispatched");
			StoreClosed();	
		}
	}
	
	public static void TriggerWinGame() {
		if (GameWon != null) {
			GameWon();
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
