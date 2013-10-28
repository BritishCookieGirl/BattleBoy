using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	
	public Transform enemy;
	public bool shouldSpawn = true;
	public float spawnRate = 5.0f;
	public int maxSpawn = 1;
	public static int currentEnemies = 0;
	
	private float nextSpawn = 0.0f;

	// Use this for initialization
	void Start () {
		GameEventManager.LevelComplete += levelComplete;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpawn && shouldSpawn) {
			
			Spawn();
			if (currentEnemies >= maxSpawn) {
				shouldSpawn = false;
			}
		}
		
		if (currentEnemies < maxSpawn && shouldSpawn == false) {
			shouldSpawn = true;
			nextSpawn = (Time.time + spawnRate);
			print ("current time is " + Time.time + "next spawn at " + nextSpawn);
		}
		

	}
	
	public void Spawn() {
			print ("spawning new enemy");
			Instantiate(enemy,transform.position, transform.rotation);         
			currentEnemies++;
	}
	
	private void levelComplete() {
		shouldSpawn = false;	
	}
	
}
