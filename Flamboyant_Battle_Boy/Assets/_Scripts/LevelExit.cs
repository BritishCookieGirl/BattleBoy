using UnityEngine;
using System.Collections;

public class LevelExit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.name == "Player") {
			other.gameObject.GetComponent<CharacterController2D>().canControl = false;
			EndLevel();	
		}
	}
	
	void EndLevel() {
		print ("Level Complete!!!");
		Time.timeScale = 0.0f;
	}
}
