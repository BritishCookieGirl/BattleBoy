using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	
	public Transform enemy;
	public float spawnRate = 2.0f;
	
	private float nextSpawn = 0.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpawn) {
			nextSpawn = Time.time + spawnRate;
			Spawn();
		}
		
	}
	
	void Spawn() {
		
			Instantiate(enemy,transform.position, transform.rotation);         
		
	}
	
}
