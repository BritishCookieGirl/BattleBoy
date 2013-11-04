using UnityEngine;
using System.Collections;

public class StoreManager : MonoBehaviour {
	
	private float height =250;
	private float width = 400;
	private bool windowOpen;
	private bool errorOpen;
	
	public UnlockableItem displayItem;
	
	public GUIStyle windowStyle;
	public GUIStyle itemNameStyle;
	public GUIStyle itemPointStyle;
	public GUIStyle itemEffectStyle;
	public GUIStyle itemDescriptionStyle;
	public GUIStyle dividerStyle;
	public GUIStyle buttonStyle;
	
	public GUIStyle errorStyle;
	public GUIStyle errorStyleText;
	
	public Rect windowRect, errorRect;
	
	
	void Start() {
		
		windowRect = new Rect((Screen.width/2 - width/2), 20, width, height);
		errorRect = new Rect((Screen.width/2 - width/2), 20, width, height);
	
	}
    
	void OnGUI() {
        if (windowOpen) {
			windowRect = GUI.Window(0, windowRect, ItemWindow, "", windowStyle);
		}
		
		if (errorOpen) {
			errorRect = GUI.Window(1,errorRect, ErrorWindow, "", windowStyle);
		}
    }
	
    void ItemWindow(int windowID) {
		GUI.Label(new Rect(15,15,130,130),displayItem.unlockedTexture);
        GUI.Label(new Rect(165,10,200,100),displayItem.itemName,itemNameStyle);
		GUI.Label(new Rect(150,35,200,5), "************************", dividerStyle);
        GUI.Label(new Rect(155,50,200,100),displayItem.PointString(),itemPointStyle);
		GUI.Label(new Rect(155,75,200,100),displayItem.itemEffect, itemEffectStyle);
		
		if (GUI.Button(new Rect(150,110,100,35),"Buy",buttonStyle)){
			BuyClicked();
		}
		if (GUI.Button(new Rect(260,110,130,35),"Cancel",buttonStyle)){
			CancelClicked();
		}
		
		GUI.Label(new Rect(10,155,200,5), "**************************************", dividerStyle);
		GUI.Label(new Rect(20,175,370,100),displayItem.itemDescription, itemDescriptionStyle);
    }
	
	void ErrorWindow(int windowID) {
		string errorText = "You are much too bland and boring to posess this item. Maintain vigilance in your holy quest and perhaps one day you shall be worthy of weilding the grandeur item that is currently the object of your affection.";
		GUI.Label(new Rect(10,15, width-30, 30),"INSUFFICIENT FABULOUSNESS", errorStyle);
		GUI.Label(new Rect(5,50,width-30,5), "***************************************", errorStyle);
		GUI.Label(new Rect(15,75,width-30,100), errorText, errorStyleText);
		if (GUI.Button(new Rect(130,200,140,35),"Continue",buttonStyle)){
			ContinueClicked();
		}
	}
	
	void BuyClicked() {
		
		if (ScoreManager.PurchaseItem(displayItem.itemCost)) {
			print ("item purchased");
			CloseWindow();
		} else {
			errorOpen = true;
		}
		//adjust abilities
	}
	
	void CancelClicked() {
		print ("item not purchased");
		CloseWindow();
	}
	
	void ContinueClicked() {
		errorOpen = false;
		CloseWindow();
		
	}
	
	public void UpdateIcon(UnlockableItem display) {
		windowOpen = true;
		displayItem = display.GetComponent<UnlockableItem>();
	}
	
	private void CloseWindow() {
		windowOpen = false;
		displayItem.SwapClickable();
	}
}
