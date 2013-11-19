using UnityEngine;
using System.Collections;

public class PointTally : MonoBehaviour {
	
	private float height = 450;
	private float width = 400;
	private bool windowOpen;
	private Rect windowRect;
	
	private bool gameWon;
	
	private float enemies, time, level, previous, total;
	private float enemiesD, timeD, levelD, previousD, totalD;
	
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
		ScoreManager.WinReached += Win;
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

    private float timeElapsed = 0f;
    private float timeLimit = 4f;
    private float totalTimeElapsed = 0f;
	
	void OnGUI ()
    {
        float timegap = 1;
        timeElapsed += Time.deltaTime;
        totalTimeElapsed += Time.deltaTime;

        if (timeElapsed > timegap && totalTimeElapsed <= timeLimit)
        {
            timeElapsed = 0f;
            GameObject.FindGameObjectWithTag("Audio").SendMessage("PlaySoundEffect", "Coin");
        }

		if (windowOpen) {
			windowRect = GUI.Window(0, windowRect, TallyWindow, "", windowStyle);
		}
		
		if (gameWon) {
			GUI.Label(new Rect(0,0,Screen.width,Screen.height), "YOU WON!!", titleStyle);
		}
	}
	
	 private void TallyWindow(int windowID) {
		GUI.Label(new Rect(50,10,100,20), "Level Complete", titleStyle);
		GUI.Label(new Rect(20, 60, 100, 20), "Enemies Killed", itemStyle);
		GUI.Label(new Rect(330, 60, 100, 20), enemiesD.ToString("#,0"), pointStyle);
		GUI.Label(new Rect(20, 120, 100, 20), "Time Bonus", itemStyle);
		GUI.Label(new Rect(330, 120, 100, 20), timeD.ToString("#,0"), pointStyle);
		GUI.Label(new Rect(20, 150,300,20), "_________________________", seperatorStyle);
		GUI.Label(new Rect(20, 180, 100, 20), "Level Total", itemStyle);
		GUI.Label(new Rect(330, 180, 100, 20), levelD.ToString("#,0"), pointStyle);
		GUI.Label(new Rect(20, 240, 100, 20), "Previous Score", itemStyle);
		GUI.Label(new Rect(330, 240, 100, 20), previousD.ToString("#,0"), pointStyle);
		GUI.Label(new Rect(20, 270,300,20), "_________________________", seperatorStyle);
		GUI.Label(new Rect(20, 300, 100, 20), "Total Score", totalStyle);
		GUI.Label(new Rect(330, 300, 100, 20), totalD.ToString("#,0"), pointStyle);
		
		if (GUI.Button(new Rect(100,350,200,75),"",buttonStyle)){
			if (gameWon) {
				GameManager.TriggerWinGame();
			} else {
				CloseWindow();
			}
		}
		
		GUI.Label(new Rect(115,355, 200,75),"Continue",bTextShaddowStyle);
		GUI.Label(new Rect(115,355, 200,75),"Continue",bTextStyle);		
	}
	
	void CountUpPoints ()
		{
		float i = 60.0f;
		enemiesD = (enemiesD < enemies) ? (enemiesD + (enemies / i)) : enemies;
		timeD = (timeD < time) ? (timeD + (time/i)) : time;
		levelD = (levelD < level) ? (levelD + (level/i)) : level;
		previousD = (previousD < previous) ? (previousD + (previous/i)) : previous;
		totalD = (totalD < total) ? (totalD + (total/i)) : total;
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
	
	private void Win(int points) {
		gameWon = true;
	}
	
}
