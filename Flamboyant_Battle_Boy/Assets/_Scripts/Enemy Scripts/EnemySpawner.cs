using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

	public Transform enemy;
	public bool respawnOnDeath;
	private bool needsSpawn;
	public Transform spawnedEnemy;
	public float spawnDelay = 5;
	private float nextSpawn;
	


	// Called before Start
	void Awake() {
		GameManager.LevelStart += LevelStart;
		GameManager.LevelComplete += LevelComplete;	
		GameManager.GameStart += LevelComplete;
	}
	
	void Start () {
		needsSpawn = false;
	}
	
	void Update() {
		if (needsSpawn && (GameManager.currentTime >= nextSpawn)){
			Spawn();
		}
	}

	//Spawn new enemy from prefab
	public void Spawn ()
	{
	    spawnedEnemy = (Transform) Instantiate(enemy, transform.position, transform.rotation);
		spawnedEnemy.GetComponentInChildren<EnemyAttack> ().spawnPoint = transform;
		spawnedEnemy.GetComponentInChildren<Enemy> ().spawnPoint = transform;
		needsSpawn = false;
	
	}
	
	// Called Automatically anytime level starts - set default variables here
	private void LevelStart ()
	{
		needsSpawn = true;
		Spawn ();
		//respawn enemies
		//reset enemy posiitions
	}

	// Called automatically anytime level finishes - set win/lose conditions here
	private void LevelComplete ()
	{
		needsSpawn = false;
	}
	
	public void RequestRespawn() {
		if(respawnOnDeath) {
			needsSpawn = true;
			nextSpawn = GameManager.currentTime + spawnDelay;
			print ("next spawn at " + nextSpawn);
		}
	}
}
