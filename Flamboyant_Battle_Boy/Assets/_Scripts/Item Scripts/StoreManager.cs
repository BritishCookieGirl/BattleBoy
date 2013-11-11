using UnityEngine;
using System.Collections;

public class StoreManager : MonoBehaviour
{
	
	private float height = 250;
	private float width = 400;
	private bool windowOpen;
	private bool errorOpen;
	private GameObject player;
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
	
	private delegate void UnlockAbility();
	private UnlockAbility[] abilityMethods;
	private int abilityIndex, comboIndex, accesoryIndex;
	
	public UnlockableItem[] weapons, clothes, accesories;
	
	
	void Awake ()
	{
		GameManager.LevelStart += LevelStart;
		GameManager.LevelComplete += LevelComplete;
	}
		
	void Start ()
	{
		abilityMethods = new UnlockAbility[] {
			AbilityUnlock1,
			AbilityUnlock2,
			AbilityUnlock3,
			AbilityUnlock4,
			AbilityUnlock5			
		};
		abilityIndex = 0;
		
		player = GameObject.FindGameObjectWithTag ("Player");
		windowRect = new Rect ((Screen.width / 2 - width / 2), 20, width, height);
		errorRect = new Rect ((Screen.width / 2 - width / 2), 20, width, height);	
	}
    
	void OnGUI ()
	{
		
		if (GUI.Button (new Rect (0, 0, 200, 50), "Back", buttonStyle)) {
			BackToGame ();
		}
		
		if (windowOpen) {
			windowRect = GUI.Window (0, windowRect, ItemWindow, "", windowStyle);
		}
		
		if (errorOpen) {
			errorRect = GUI.Window (1, errorRect, ErrorWindow, "", windowStyle);
		}
	}
	
	private void ItemWindow (int windowID)
	{
		GUI.Label (new Rect (15, 15, 130, 130), displayItem.unlockedTexture);
		GUI.Label (new Rect (165, 10, 200, 100), displayItem.itemName, itemNameStyle);
		GUI.Label (new Rect (150, 35, 200, 5), "************************", dividerStyle);
		GUI.Label (new Rect (155, 50, 200, 100), displayItem.PointString (), itemPointStyle);
		GUI.Label (new Rect (155, 75, 200, 100), displayItem.itemEffect, itemEffectStyle);
		
		if (GUI.Button (new Rect (150, 110, 100, 35), "Buy", buttonStyle)) {
			BuyClicked ();
		}
		if (GUI.Button (new Rect (260, 110, 130, 35), "Cancel", buttonStyle)) {
			CancelClicked ();
		}
		
		GUI.Label (new Rect (10, 155, 200, 5), "**************************************", dividerStyle);
		GUI.Label (new Rect (20, 175, 370, 100), displayItem.itemDescription, itemDescriptionStyle);
	}
	
	private void ErrorWindow (int windowID)
	{
		string errorText = "You are much too bland and boring to posess this item. Maintain vigilance in your holy quest and perhaps one day you shall be worthy of weilding the grandeur item that is currently the object of your affection.";
		GUI.Label (new Rect (10, 15, width - 30, 30), "INSUFFICIENT FABULOUSNESS", errorStyle);
		GUI.Label (new Rect (5, 50, width - 30, 5), "***************************************", errorStyle);
		GUI.Label (new Rect (15, 75, width - 30, 100), errorText, errorStyleText);
		if (GUI.Button (new Rect (130, 200, 140, 35), "Continue", buttonStyle)) {
			ContinueClicked ();
		}
	}
	
	private void BuyClicked ()
	{
		print ("points: " + ScoreManager.totalScore);
		print ("item cost" + displayItem.itemCost);
		if (ScoreManager.PurchaseItem (displayItem.itemCost)) {
			
			switch (displayItem.UnlockFeature()) {
			case UnlockableItem.Feature.Combo:
				player.GetComponent<PlayerCombat> ().IncreaseComboLength ();
				comboIndex++;
				weapons[comboIndex].UnlockIcon();
				break;
			case UnlockableItem.Feature.Ability:
				abilityMethods[abilityIndex]();
				abilityIndex++;
				clothes[abilityIndex].UnlockIcon();
				break;
			case UnlockableItem.Feature.Cosmetic:
				print ("things are prettier now. deal.");
				accesoryIndex++;
				accesories[accesoryIndex].UnlockIcon();
				break;
			default:
				break;
			}

			CloseWindow ();
			print("close window called");
			
		} else {
			errorOpen = true;
		}
		
	}
	
	private void CancelClicked ()
	{
		CloseWindow ();
	}
	
	private void ContinueClicked ()
	{
		errorOpen = false;
		CloseWindow ();
	}
	
	public void UpdateIcon (UnlockableItem display)
	{
		
		displayItem = display.GetComponent<UnlockableItem> ();
		if (displayItem.unlocked) {
			windowOpen = true;
		}
	}
	
	private void CloseWindow ()
	{
		windowOpen = false;
		displayItem.SwapClickable ();
	}
	
	private void BackToGame ()
	{
		GameManager.TriggerStoreClosed ();
	}
	
	private void LevelStart ()
	{
//		foreach (Transform child in transform) {
//			print(child.name);
//			child.gameObject.SetActive(true);
//		}
	}
	
	private void LevelComplete ()
	{
//		foreach (Transform child in transform) {
//			print(child.name);
//			child.gameObject.SetActive(false);
//		}
	}
	
	private void AbilityUnlock1() {
		player.GetComponent<CharacterController2D> ().DoubleJumpEnabled = true;
	}
	private void AbilityUnlock2() {
		player.GetComponent<CharacterController2D> ().TripleJumpEnabled = true;
	}
	private void AbilityUnlock3() {
		//player.GetComponent<CharacterController2D> ().QuadJumpEnabled = true;
	}
	private void AbilityUnlock4() {
		//player.GetComponent<CharacterController2D> ().SeptJumpEnabled = true;
	}
	private void AbilityUnlock5() {
		//player.GetComponent<CharacterController2D> ().SextJumpEnabled = true;
	}
	
}
