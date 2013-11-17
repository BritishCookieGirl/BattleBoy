using UnityEngine;
using System.Collections;

public class IncreaseFabulous : MonoBehaviour {
	
	void Awake() {
		ScoreManager.ThresholdReached += IncreaseAlpha;
		GameManager.LevelStart += ResetAlpha;
		
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void IncreaseAlpha(int level) {
		Color currentColor = renderer.material.color;
		Color newColor = new Color(1.0f,1.0f,1.0f,currentColor.a+0.25f);
		renderer.material.color = newColor;
		
	}
	
	private void ResetAlpha() {
		renderer.material.color = new Color(1.0f,1.0f,1.0f,0.0f);
	}
}
