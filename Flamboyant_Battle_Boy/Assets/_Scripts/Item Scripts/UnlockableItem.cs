using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class UnlockableItem : MonoBehaviour {
	
	public Texture unlockedTexture;
	public Texture lockedTexture;
	public AudioClip hoverSound;
	public AudioClip selectedSound;
	public bool unlocked;
	private Color normalColor = new Color(0.6f,0.6f,0.6f);
	private Color hoverColor = new Color(0.75f,0.75f,0.75f);
	private Color activeColor = new Color(1.0f,1.0f,1.0f);
	
	// Use this for initialization
	void Start () {
		renderer.material.color = normalColor;
		if (unlocked) {
			renderer.material.mainTexture = unlockedTexture;
		} else {
			renderer.material.mainTexture = lockedTexture;
		}
		
		audio.clip = hoverSound;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseEnter() {
		//renderer.material.mainTexture = unlockedTexture;
		renderer.material.color = hoverColor;
		audio.Play();
	}
	
	void OnMouseExit() {
		//renderer.material.mainTexture = lockedTexture;
		renderer.material.color = normalColor;
	}
	
	void OnMouseDown() {
		renderer.material.color = activeColor;
		audio.clip = selectedSound;
		audio.Play();
	}
	
	void OnMouseUp() {
		renderer.material.color = normalColor;
		audio.clip = hoverSound;
	}
}
