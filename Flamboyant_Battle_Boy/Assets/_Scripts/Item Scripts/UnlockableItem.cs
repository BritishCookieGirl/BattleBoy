using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class UnlockableItem : MonoBehaviour {
	
	public string itemName;
	public string itemEffect;
	public string itemDescription;
	public int itemCost;
	
	public Texture unlockedTexture;
	public Texture lockedTexture;
	
	public AudioClip hoverSound;
	public AudioClip selectedSound;
	
	public bool unlocked;
	static bool canClick;
	
	private StoreManager store;
		
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
		if (canClick) {
			renderer.material.color = hoverColor;

            Camera.main.SendMessage("PlaySoundEffect", "MenuButtonOver");
			//audio.Play();
		}
		
	}
	
	void OnMouseExit() {
		//renderer.material.mainTexture = lockedTexture;
		renderer.material.color = normalColor;
	}
	
	void OnMouseDown() {
		
		if (canClick && unlocked) {
			renderer.material.color = activeColor;
			
			store.UpdateIcon(this.GetComponent<UnlockableItem>());
			SwapClickable();

            Camera.main.SendMessage("PlaySoundEffect", "MenuButtonSelect");
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
	
	public void SwapClickable(){
		canClick = !canClick;
	}
}
