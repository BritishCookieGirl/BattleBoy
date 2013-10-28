using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public GameManager gm;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			gm.updateCombo(1);
			gm.updateScore(100);
		}
	}
	
}
