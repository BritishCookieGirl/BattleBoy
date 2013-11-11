﻿using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

	public Transform enemy;
	public float spawnRate = 5.0f;
	public int maxSpawn = 1;
	public static int currentEnemies = 0;
	private bool canSpawn = false;
	private float nextSpawn = 10.0f;


	// Called before Start
	void Awake() {
		GameManager.LevelStart += LevelStart;
		GameManager.LevelComplete += LevelComplete;		
	}
	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		if (currentEnemies >= maxSpawn) {
			canSpawn = false;
		}
		
		if (GameManager.currentTime > nextSpawn && canSpawn) {
			Spawn ();
		}

		if (currentEnemies < maxSpawn && canSpawn == false) {
			canSpawn = true;
			nextSpawn = (GameManager.currentTime + spawnRate);
		}
	}

	//Spawn new enemy from prefab
	public void Spawn ()
	{
        Instantiate(enemy, new Vector3(this.transform.position.x, 2f, 7f), transform.rotation);
		currentEnemies++;
	}

	// Called Automatically anytime level starts - set default variables here
	private void LevelStart ()
	{
		canSpawn = true;
		//respawn enemies
		//reset enemy posiitions
	}

	// Called automatically anytime level finishes - set win/lose conditions here
	private void LevelComplete ()
	{
		canSpawn = false;
	}

}
