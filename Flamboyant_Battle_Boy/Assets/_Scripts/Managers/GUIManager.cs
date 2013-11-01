using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour
{

	public GUIText comboText, scoreText, timerText, comboCounter;
	
	void Awake() {
		GameManager.LevelStart += LevelStart;
		GameManager.LevelComplete += LevelComplete;
		ComboManager.ComboChanged += DisplayCombo;
		ScoreManager.ScoreChanged += DisplayScore;	
		GameManager.TimeChanged +=  DisplayTime;
	}

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	// Called Automatically anytime level starts - set default variables here
	private void LevelStart ()
	{

	}

	// Called automatically anytime level finishes - set win/lose conditions here
	private void LevelComplete ()
	{

	}

	// Called automatically anytime combo changes - updates combo GUI text
	private void DisplayCombo (int points)
	{
		comboCounter.text = new string ('*', points);
	}

	// Called automatically anytime score changes - updates score GUI text
	private void DisplayScore (int points)
	{
		scoreText.text = points.ToString ();
	}

	// Called automatically anytime time changes - updates time GUI text
	private void DisplayTime (float time)
	{
		double displayTime = System.Math.Round (time, 2);
		timerText.text = displayTime.ToString ("0.00");
	}
}
