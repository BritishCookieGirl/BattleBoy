using UnityEngine;
using System.Collections;

public class RainbowTrail : MonoBehaviour {
	
	public CosmeticManager cosmeticManager;
	public bool particlesUnlocked;

	// Use this for initialization
	void Start () {
		cosmeticManager = GameObject.Find("Cosmetics").GetComponent<CosmeticManager> ();
		particlesUnlocked = cosmeticManager.enemyDeathUnlocked;
		GameManager.LevelStart += LevelStart;
		GameManager.LevelComplete += LevelComplete;
	}
	
	// Update is called once per frame
	void Update () {
		if (particlesUnlocked && !particleSystem.isPlaying) {
			particleSystem.Play();
		}
	}
	
	private void LevelStart() {
		particlesUnlocked = cosmeticManager.playerTrailUnlocked;
		if (particlesUnlocked) {
			particleSystem.Play();
		}
	}
	
	private void LevelComplete() {
		particleSystem.Stop();
	}
}
