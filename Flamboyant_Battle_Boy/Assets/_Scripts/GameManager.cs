using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	//public Timer timer;
	public int combo, maxCombo, score;
	public float comboTime;
	private float comboStart;
	private bool firstAttack;
	
	public delegate void PointEventHandler(int points);
	public static event PointEventHandler ScoreChanged, ComboChanged;
	
	//public event 
	
	void Awake() {
		GameEventManager.LevelStart += LevelStart;
		GameEventManager.LevelComplete += LevelComplete;		
	}
	
	// Use this for initialization
	void Start () {
		GameEventManager.TriggerLevelStart();
	}
	
	// Update is called once per frame
	void Update () {
		float currentTime  = Time.time;
		if (currentTime > (comboStart + comboTime)){
			resetCombo();	
		}
		
	}
	
	private void LevelStart() {
		resetScore();
		resetCombo();	
	}
	
	private void LevelComplete() {
		
	}
	
	public void updateScore (int points) {
		int multiplier = maxCombo - combo;
		score += (points * multiplier);

		if (ScoreChanged != null) {
			ScoreChanged(score);	
		}
	}
	
	public void updateCombo (int points) {
		if (firstAttack) {
			comboStart = Time.time;	
			firstAttack = false;
		} 
		
		combo = Mathf.Clamp((combo+points),0,maxCombo);
		if (ComboChanged != null) {
			ComboChanged(combo);	
		}
	}
	
	public void resetScore() {
		updateScore (0);	
	}
	
	public void resetCombo() {
		combo = maxCombo;
		updateCombo(combo);
		firstAttack = true;
	}
}
