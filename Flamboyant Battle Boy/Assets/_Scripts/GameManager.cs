using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public float timer;
	public int combo, score;
	
	public delegate void PointEventHandler(int points);
	public static event PointEventHandler ScoreChanged, ComboChanged;
	
	//public event 
	
	// Use this for initialization
	void Start () {
		GameEventManager.TriggerLevelStart();
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void updateScore (int points) {
		score += points;
		if (ScoreChanged != null) {
			ScoreChanged(score);	
		}
	}
	
	public void updateCombo (int points) {
		combo += points;	
		if (ComboChanged != null) {
			ComboChanged(combo);	
		}
	}
}
