using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public float speed = 5f;
	public bool canMove = true;
	
	// Use this for initialization
	void Start () {
		
		GameEventManager.LevelComplete += LevelComplete;
		transform.parent = GameObject.Find("Enemy Spawn").transform;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (canMove) {
			transform.Translate(Vector3.left * speed * Time.deltaTime);
		}
	}
	
	private void LevelComplete() {
		canMove = false;	
	}
	
}
