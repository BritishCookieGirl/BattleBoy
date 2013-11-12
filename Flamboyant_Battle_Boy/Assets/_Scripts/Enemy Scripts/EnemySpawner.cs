using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

	public Transform enemy;
	public bool needsSpawn;
	public Transform spawnedEnemy;
	


	// Called before Start
	void Awake() {
		GameManager.LevelStart += LevelStart;
		GameManager.LevelComplete += LevelComplete;		
	}
	
	void Update() {
		if (needsSpawn)
		Spawn();
	}

	//Spawn new enemy from prefab
	public void Spawn ()
	{
	    spawnedEnemy = (Transform) Instantiate(enemy, transform.position, transform.rotation);
		spawnedEnemy.GetComponentInChildren<EnemyAttack> ().spawnPoint = transform;
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

}
