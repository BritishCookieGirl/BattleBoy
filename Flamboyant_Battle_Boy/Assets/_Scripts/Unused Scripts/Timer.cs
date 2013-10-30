using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{

	public float startTime, bonusTime, endTime, remainingTime;

	// Use this for initialization
	void Start ()
	{
		GameEventManager.LevelStart += LevelStart;
		GameEventManager.LevelComplete += LevelComplete;
	}

	// Update is called once per frame
	void Update ()
	{
		remainingTime = bonusTime - Time.time;

	}

	private void LevelStart ()
	{
		startTime = Time.time;
	}

	private void LevelComplete ()
	{
		endTime = Time.time;
	}

}