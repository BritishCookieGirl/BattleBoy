using UnityEngine;
using System.Collections;

public class PointTally : MonoBehaviour {
	
	private float height = 450;
	private float width = 400;
	private bool windowOpen;
	private Rect windowRect;
	
	private float enemies, time, level, previous, total;
	private int enemiesD, timeD, levelD, previousD, totalD;
	
	public GUIStyle windowStyle;
	public GUIStyle titleStyle;
	public GUIStyle itemStyle;
	public GUIStyle pointStyle;
	public GUIStyle seperatorStyle;
	public GUIStyle totalStyle;
	public GUIStyle buttonStyle;
	public GUIStyle bTextStyle, bTextShaddowStyle;
	
	void Awake () {
		GameManager.LevelStart += EndTally;
		GameManager.LevelComplete += StartTally;
		GameManager.StoreOpen += EndTally;
	}
	
	// Use this for initialization
	void Start () {
		windowRect = new Rect((Screen.width/2 - width/2), 75, width, height);
		windowOpen = true;
		
//		enemies = 500;
//		time = 1000;
//		level = 1500;
//		previous = 4000;
//		total = 5500;
	}
	
	// Update is called once per frame
	void Update () {
		if (windowOpen) {
			CountUpPoints ();
		}
	
	}
	
	void OnGUI () {
		if (windowOpen) {
			windowRect = GUI.Window(0, windowRect, TallyWindow, "", windowStyle);
		}
	}
	
	 private void TallyWindow(int windowID) {
		GUI.Label(new Rect(50,10,100,20), "Level Complete", titleStyle);
		GUI.Label(new Rect(20, 60, 100, 20), "Enemies Killed", itemStyle);
		GUI.Label(new Rect(330, 60, 100, 20), enemiesD.ToString(), pointStyle);
		GUI.Label(new Rect(20, 120, 100, 20), "Time Bonus", itemStyle);
		GUI.Label(new Rect(330, 120, 100, 20), timeD.ToString(), pointStyle);
		GUI.Label(new Rect(20, 150,300,20), "_________________________", seperatorStyle);
		GUI.Label(new Rect(20, 180, 100, 20), "Level Total", itemStyle);
		GUI.Label(new Rect(330, 180, 100, 20), levelD.ToString(), pointStyle);
		GUI.Label(new Rect(20, 240, 100, 20), "Previous Score", itemStyle);
		GUI.Label(new Rect(330, 240, 100, 20), previousD.ToString(), pointStyle);
		GUI.Label(new Rect(20, 270,300,20), "_________________________", seperatorStyle);
		GUI.Label(new Rect(20, 300, 100, 20), "Total Score", totalStyle);
		GUI.Label(new Rect(330, 300, 100, 20), totalD.ToString(), pointStyle);
		
		if (GUI.Button(new Rect(100,350,200,75),"",buttonStyle)){
			CloseWindow();
		}
		
		GUI.Label(new Rect(115,355, 200,75),"Continue",bTextShaddowStyle);
		GUI.Label(new Rect(115,355, 200,75),"Continue",bTextStyle);		
	}
	
	void CountUpPoints ()
		{
		float i = 60.0f;
		enemiesD = (enemiesD < enemies) ? (int)(enemiesD + (enemies / i)) : (int)enemies;
		timeD = (timeD < time) ? (int)(timeD + (time/i)) : (int)time;
		levelD = (levelD < level) ? (int)(levelD + (level/i)) : (int)level;
		previousD = (previousD < previous) ? (int)(previousD + (previous/i)) : (int)previous;
		totalD = (totalD < total) ? (int)(totalD + (total/i)) : (int)total;
	}
	
	void StartTally() {
		windowOpen = true;
		TallyPoints();
		
	}
	
	void EndTally() {
		windowOpen = false;
	}

	void TallyPoints ()
	{
		enemies = ScoreManager.score;
		time = (int)GameManager.remainingTime;
		level = enemies + time;
		previous = ScoreManager.totalScore;
		total = ScoreManager.CalculateFinalScore();
	}
	
	
	void CloseWindow() {
		GameManager.TriggerStoreActive();
	}
	
}
