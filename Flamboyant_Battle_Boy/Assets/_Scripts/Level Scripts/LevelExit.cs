using UnityEngine;
using System.Collections;

public class LevelExit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Triggers level end when entered by Player
	void OnTriggerEnter(Collider other) {
		if (other.name == "Player") {
			GameManager.TriggerLevelComplete();
		}
	}
	
}
