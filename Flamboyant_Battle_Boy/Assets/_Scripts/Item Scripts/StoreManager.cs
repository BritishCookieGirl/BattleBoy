using UnityEngine;
using System.Collections;

public class StoreManager : MonoBehaviour {
	
	private float height =250;
	private float width = 400;
	
	public UnlockableItem displayItem;
	
	public GUIStyle itemNameStyle;
	public GUIStyle itemPointStyle;
	public GUIStyle itemEffectStyle;
	public GUIStyle itemDescriptionStyle;
	public GUIStyle dividerStyle;
	public GUIStyle buttonStyle;

	public Rect windowRect;
	
	void Start() {
		
		windowRect = new Rect((Screen.width/2 - width/2), 20, width, height);
		print ("Box Height = " + height);
		print ("Box Width = " + width);
	}
    
	void OnGUI() {
        windowRect = GUI.Window(0, windowRect, ItemWindow, "");
    }
	
    void ItemWindow(int windowID) {
		GUI.Label(new Rect(15,15,130,130),displayItem.unlockedTexture);
        GUI.Label(new Rect(165,10,200,100),displayItem.itemName,itemNameStyle);
		GUI.Label(new Rect(150,35,200,5), "************************", dividerStyle);
        GUI.Label(new Rect(155,50,200,100),displayItem.PointString(),itemPointStyle);
		GUI.Label(new Rect(155,75,200,100),displayItem.itemEffect, itemEffectStyle);
		
		if (GUI.Button(new Rect(150,110,100,30),"Buy",buttonStyle)){
			BuyClicked();
		}
		if (GUI.Button(new Rect(260,110,130,30),"Cancel",buttonStyle)){
			CancelClicked();
		}
		
		GUI.Label(new Rect(10,155,200,5), "**************************************", dividerStyle);
		GUI.Label(new Rect(20,175,370,100),displayItem.itemDescription, itemDescriptionStyle);
    }
	
	void BuyClicked() {
		print ("item purchased");
		//destroy window
		//adjust points
		//adjust abilities
	}
	
	void CancelClicked() {
		print ("item not purchased");
	}
	
	public void UpdateIcon(UnlockableItem display) {
		displayItem = display.GetComponent<UnlockableItem>();
	}
}
