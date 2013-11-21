using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour
{

	public Texture[] HUDStates;

	
	private Texture currentHUD;
	public int HUDIndex;
	public GUIStyle scoreStyle,scoreShaddowStyle;
	
	private int score;
	void Awake() {
		GameManager.LevelStart += LevelStart;
		GameManager.LevelComplete += LevelComplete;
		ComboManager.ComboChanged += DisplayCombo;
		ScoreManager.ScoreChanged += DisplayScore;	
		//GameManager.TimeChanged +=  DisplayTime;
	}

	// Use this for initialization
	void Start ()
	{
		print (HUDStates.Length + " images in HUD array");
	}

	// Update is called once per frame
	void Update ()
	{
		ComboManager.UpdateCombo();
	}

	// Called Automatically anytime level starts - set default variables here
	private void LevelStart ()
	{
		currentHUD = HUDStates[0];
		HUDIndex = 0;
	}

	// Called automatically anytime level finishes - set win/lose conditions here
	private void LevelComplete ()
	{

	}

	// Called automatically anytime combo changes - updates combo GUI text

	private void DisplayCombo (int points)
	{
		
		HUDIndex = Mathf.Clamp(points,0,HUDStates.Length-1);
		//print ("Displaying HUD frame #"+HUDIndex);
		currentHUD = HUDStates[HUDIndex];
		//comboCounter.text = new string ('*', points);
	}

	// Called automatically anytime score changes - updates score GUI text
	private void DisplayScore (int points)
	{
		score = points;
//		if (scoreText != null) {
//			scoreText.text = points.ToString ();
//		}
	}

	// Called automatically anytime time changes - updates time GUI text
	private void DisplayTime (float time)
	{
//		double displayTime = System.Math.Round (time, 2);
//		timerText.text = displayTime.ToString ("0.00");
	}
	
	void OnGUI() {
		
		GUI.Label(new Rect(0,0,256,128), currentHUD);
		GUI.Label(new Rect(0,50,220,128), score.ToString(), scoreShaddowStyle);
		GUI.Label(new Rect(0,50,220,128), score.ToString(), scoreStyle);
		
	}
	
}
