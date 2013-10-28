using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public float speed = 5f;
	public bool canMove = true;
	public int health = 100;
	public int defense = 0;
	
	// Use this for initialization
	void Start () {
		
		GameEventManager.LevelStart += LevelStart;
		GameEventManager.LevelComplete += LevelComplete;
		transform.parent = GameObject.Find("Enemy Spawn").transform;
		
	}
	
	// Update is called once per frame
	void Update () {
		
//		if (canMove) {
//			transform.Translate(Vector3.left * speed * Time.deltaTime);
//		}
	}
	
	private void LevelStart() {
		canMove = true;	
	}
	
	private void LevelComplete() {
		canMove = false;	
	}
	
	public int TakeDamage(int attackStrength) {
		int damageTaken = attackStrength - defense;
		health -= damageTaken;
		if (health <= 0) {
			print ("Enemy defeated");
			Destroy(gameObject, 1);	
		}
		
		return damageTaken;
		
	}
	
	private void OnDestroy() {
		EnemySpawner.currentEnemies--;	
	}
	
	
}
