using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class UnlockableItem : MonoBehaviour {
	
	public enum Feature {Combo, Ability, Cosmetic};
	
	static bool canClick;
	
	public string itemName;
	public string itemEffect;
	public string itemDescription;
	public int itemCost;
	public bool unlocked, purchased;
	public Feature unlockFeature;
	
	public Texture unlockedTexture;
	public Texture lockedTexture;
	
	public AudioClip hoverSound;
	public AudioClip selectedSound;
	
	private StoreManager store;
	
	private Color fadedColor = new Color(0.4f,0.4f,0.4f);
	private Color normalColor = new Color(0.6f,0.6f,0.6f);
	private Color hoverColor = new Color(0.75f,0.75f,0.75f);
	private Color activeColor = new Color(1.0f,1.0f,1.0f);
	

	// Use this for initialization
	void Start () {
		renderer.material.color = normalColor;
		store = transform.parent.parent.GetComponent<StoreManager>();
		if (unlocked) {
			renderer.material.mainTexture = unlockedTexture;
		} else {
			renderer.material.mainTexture = lockedTexture;
		}
		
		canClick = true;
		audio.clip = hoverSound;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseEnter() {
		//renderer.material.mainTexture = unlockedTexture;
		if (canClick && !purchased) {
			renderer.material.color = hoverColor;

            Camera.main.SendMessage("PlaySoundEffect", "MenuButtonOver");
            //Camera.main.SendMessage("PlaySoundEffect", "MenuButtonOver");
            GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>().PlaySoundEffect("MenuButtonOver");
			//audio.Play();
		}
		
	}
	
	void OnMouseExit() {
		//renderer.material.mainTexture = lockedTexture;
		if (!purchased) {
			renderer.material.color = normalColor;
		}
	}
	
	void OnMouseDown() {
		
		if (canClick && unlocked && !purchased) {
			renderer.material.color = activeColor;
			
			store.UpdateIcon(this.GetComponent<UnlockableItem>());
			SwapClickable();
			
			GameObject.FindGameObjectWithTag("Audio").SendMessage("PlaySoundEffect","MenuButtonSelect");
            //Camera.main.SendMessage("PlaySoundEffect", "MenuButtonSelect");

            Camera.main.SendMessage("PlaySoundEffect", "MenuButtonSelect");
            GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>().PlaySoundEffect("MenuButtonSelect");
			//audio.clip = selectedSound;
			//audio.Play();
		}
		
	}
	
	void OnMouseUp() {
		renderer.material.color = normalColor;
		audio.clip = hoverSound;
	}
	
	public string PointString() {
		return itemCost.ToString() + " pts";
	}
	
	public void SwapClickable() {
		canClick = !canClick;
	}
	
	public Feature UnlockFeature() {
		purchased = true;
		renderer.material.color = fadedColor;
		return unlockFeature;
	}
	
	public void UnlockIcon() {
		unlocked = true;
		renderer.material.mainTexture = unlockedTexture;
	}
	
}
