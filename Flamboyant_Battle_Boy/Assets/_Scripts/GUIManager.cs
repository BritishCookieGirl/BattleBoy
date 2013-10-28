using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	
	public GUIText comboText, scoreText, timerText, comboCounter;
	public Timer timer;
	
	void Awake() {
		GameEventManager.LevelStart += LevelStart;
		GameEventManager.LevelComplete += LevelComplete;
		GameManager.ComboChanged += UpdateCombo;
		GameManager.ScoreChanged += UpdateScore;
		
	}
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		double displayTime = System.Math.Round(timer.remainingTime,2);
		timerText.text = displayTime.ToString("0.00");
	}
	
	private void LevelStart() {

	}
	
	private void LevelComplete() {
		
	}
	
	private void UpdateCombo(int points) {
		comboCounter.text = new string('*',points);
	}
	
	private void UpdateScore(int points) {
		scoreText.text = points.ToString();	
	}
}
