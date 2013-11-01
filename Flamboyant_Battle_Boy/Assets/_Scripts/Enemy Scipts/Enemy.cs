using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public float speed = 5f;
	public bool canMove = true;
	public int health = 100;
	public int defense = 0;
	public int pointValue = 100;
	
//	public delegate void DamageEventHandler(int points);
//	public static event DamageEventHandler TookDamage;
	
	// Called before Start
	void Awake() {
		GameEventManager.LevelStart += LevelStart;
		GameEventManager.LevelComplete += LevelComplete;		
	}
	
	// Use this for initialization
	void Start () {
		transform.parent = GameObject.Find("Enemy Spawn").transform;

	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	// Called Automatically anytime level starts - set default variables here
	private void LevelStart() {
		canMove = true;	
	}
	
	// Called automatically anytime level finishes - set win/lose conditions here
	private void LevelComplete() {
		canMove = false;	
	}
	
	// Use by external objects to apply damage to enemy instance
	public void TakeDamage(int attackStrength) {
		int damageTaken = attackStrength - defense;
		health -= damageTaken;
		if (health <= 0) {
			Destroy(gameObject);	
		}
		
		ScoreManager.UpdateScore(damageTaken);

	}
	
	// Called before object death - use to tidy lose ends
	private void OnDestroy() {
		EnemySpawner.currentEnemies--;
		ScoreManager.UpdateScore(pointValue);
	}
	
	
}
