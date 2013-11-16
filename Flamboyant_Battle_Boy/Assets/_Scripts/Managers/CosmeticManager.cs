using UnityEngine;
using System.Collections;

public class CosmeticManager : MonoBehaviour {
	
	public ParticleSystem AmbientParticles;
	public ParticleSystem AmbientBurstParticles;
	public ParticleSystem PlayerTrail;
	
	void Awake () {
		ScoreManager.ThresholdReached += makePretty;
		GameManager.LevelStart += ResetLocks;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void makePretty(int level) {
		
		AmbientBurstParticles.Emit(1000);
		
		switch (level){
			case 1:
				Unlock1();
				break;
			case 2:
				Unlock2();
				break;
			case 3:
				Unlock3();
				break;
			case 4:
				Unlock4();
				break;
			case 5:
				Unlock5();
				break;
			default:
				break;	
		}
		
	}
	
	private void Unlock1() {
		AmbientParticles.Play();

	}
	
	private void Unlock2() {
		PlayerTrail.Play();
	}
	
	private void Unlock3() {
		print ("unlocking third level of awesome");
	}
	
	private void Unlock4() {
		print ("unlocking fourth level of awesome");
	}
	
	private void Unlock5() {
		print ("unlocking fifth level of awesome");
	}
	
	private void ResetLocks() {
		AmbientParticles.Stop();
		AmbientParticles.Clear();
		
		PlayerTrail.Stop();
		PlayerTrail.Clear();
	}
	
}
