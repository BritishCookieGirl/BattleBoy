using UnityEngine;
using System.Collections;

public class StoreManager : MonoBehaviour
{
	
	private float height = 240;
	private float width = 400;
	private int score;
	private bool windowOpen;
	private bool errorOpen;
	private GameObject player;
	public GameObject cosmeticManager;
	
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
	public GUIStyle itemHeaderStyle, itemHeaderShaddowStyle;
	public GUIStyle scoreStyle, scoreShaddowStyle, pointShaddow1, pointShaddow2;
	public GUIStyle maskStyle;
	
	private Rect windowRect, errorRect;

	private int abilityIndex, comboIndex, accesoryIndex;
	private UnlockAbility[] abilityMethods, fabMethods;
	public UnlockableItem[] weapons, clothes, accesories;
	private delegate void UnlockAbility();
	
	void Awake ()
	{
		GameManager.LevelStart += LevelStart;
		GameManager.LevelComplete += LevelComplete;
		ScoreManager.ScoreChanged += UpdateScore;
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
		
		fabMethods = new UnlockAbility[] {
			FabUnlock1,
			FabUnlock2,
			FabUnlock3,
			FabUnlock4,
			FabUnlock5			
		};
		
		player = GameObject.FindGameObjectWithTag ("Player");
		windowRect = new Rect ((Screen.width / 2 - width / 2), 200, width, height);
		errorRect = new Rect ((Screen.width / 2 - width / 2), 200, width, height);	
	}
    
	void OnGUI ()
	{	
		
		GUI.Label(new Rect(20,20,0,0),"Score:",scoreShaddowStyle);
		GUI.Label(new Rect(250,20,20,75),score.ToString("#,#"),pointShaddow1);
		GUI.Label(new Rect(250,20,20,75),score.ToString("#,#"),pointShaddow2);
		
		GUI.Label(new Rect(20,20,0,0),"Score:",scoreStyle);
		GUI.Label(new Rect(250,20,20,75),score.ToString("#,#"),scoreStyle);
		
		GUI.Label(new Rect(43,115,0,0),"Weapon",itemHeaderShaddowStyle);
		GUI.Label(new Rect(15,245,0,0),"Clothing",itemHeaderShaddowStyle);
		GUI.Label(new Rect(83,373,0,0),"Flair",itemHeaderShaddowStyle);
		
		GUI.Label(new Rect(43,115,0,0),"Weapon",itemHeaderStyle);
		GUI.Label(new Rect(15,245,0,0),"Clothing",itemHeaderStyle);
		GUI.Label(new Rect(83,373,0,0),"Flair",itemHeaderStyle);
		
		
		if (GUI.Button (new Rect (20, 510, 225, 75), "", buttonStyle)) {
			BackToStart ();
		}
		
		GUI.Label(new Rect(42,518,221,75),"Main Menu",itemHeaderShaddowStyle);
		GUI.Label(new Rect(42,518,221,75),"Main Menu",itemHeaderStyle);
		
		
		if (GUI.Button (new Rect (560, 510, 225, 75), "", buttonStyle)) {
			BackToGame ();
		}
		
		GUI.Label(new Rect(595,518,221,75),"Continue",itemHeaderShaddowStyle);
		GUI.Label(new Rect(595,518,221,75),"Continue",itemHeaderStyle);
		
		
		if (windowOpen) {
			GUI.Box(new Rect(0,0,Screen.width+10, Screen.height+10),"",maskStyle);
			windowRect = GUI.Window (0, windowRect, ItemWindow, "", windowStyle);
		}
		
		if (errorOpen) {
			errorRect = GUI.Window (1, errorRect, ErrorWindow, "", windowStyle);
		}
	}
	
	private void ItemWindow (int windowID)
	{
		GUI.Label (new Rect (15, 15, 130, 130), displayItem.unlockedTexture);
		
		GUI.Label (new Rect (125, 0, 200, 100), displayItem.itemName, itemNameStyle);
		//GUI.Label (new Rect (122, 100, 200, 5), "************************", dividerStyle);
		GUI.Label (new Rect (175, 60, 200, 100), displayItem.PointString (), itemPointStyle);
		GUI.Label (new Rect (10, 115, 400, 5), displayItem.itemEffect, itemEffectStyle);
		
		if (GUI.Button (new Rect (9, 155, 185, 75), "", buttonStyle)) {
			BuyClicked ();
		}
		GUI.Label(new Rect(71, 163, 205, 75),"Buy",itemHeaderShaddowStyle);
		GUI.Label(new Rect(71, 163, 205, 75),"Buy",itemHeaderStyle);
		
		if (GUI.Button (new Rect (201, 155, 185, 75), "", buttonStyle)) {
			CancelClicked ();
		}
		GUI.Label(new Rect(236, 163, 205, 75),"Cancel",itemHeaderShaddowStyle);
		GUI.Label(new Rect(236, 163, 205, 75),"Cancel",itemHeaderStyle);
		
	}
	
	private void ErrorWindow (int windowID)
	{
		string errorText = "You are much too bland and boring to posess this item. Maintain vigilance in your holy quest and perhaps one day you shall be worthy of weilding the grandeur item that is currently the object of your affection.";
		GUI.Label (new Rect (10, 15, width - 30, 30), "INSUFFICIENT FABULOUSNESS", errorStyle);
		GUI.Label (new Rect (10, 25, width - 30, 5), "______________________", errorStyle);
		GUI.Label (new Rect (10, 70, width - 30, 100), errorText, errorStyleText);
		if (GUI.Button (new Rect (190, 170, 200, 60), "Continue", buttonStyle)) {
			ContinueClicked ();
		}
		GUI.Label(new Rect(215, 170, 200, 60),"Continue",itemHeaderShaddowStyle);
		GUI.Label(new Rect(215, 170, 200, 60),"Continue",itemHeaderStyle);
	}
	
	private void BuyClicked ()
	{
		print ("points: " + ScoreManager.totalScore);
		print ("item cost" + displayItem.itemCost);
		if (ScoreManager.PurchaseItem (displayItem.itemCost)) {
			
			switch (displayItem.UnlockFeature()) {
			case UnlockableItem.Feature.Combo:
				player.GetComponent<PlayerCombat> ().IncreaseComboLength ();
				ComboManager.IncrementMaxCombo();
				comboIndex++;
				UnlockNextItem(comboIndex,weapons);
				break;
			case UnlockableItem.Feature.Ability:
				abilityMethods[abilityIndex]();
				abilityIndex++;
				UnlockNextItem(abilityIndex,clothes);
				break;
			case UnlockableItem.Feature.Cosmetic:
				fabMethods[accesoryIndex]();
				accesoryIndex++;
				UnlockNextItem(accesoryIndex,accesories);
				break;
			default:
				break;
			}

			CloseWindow ();
			
		} else {
			errorOpen = true;
		}
		
	}
	
	private void UnlockNextItem(int index, UnlockableItem[] itemList) {
		if (index <= itemList.Length) {
			itemList[index].UnlockIcon();
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
		GameManager.TriggerLevelStart ();		
	}
	
	private void BackToStart ()
	{
		GameManager.TriggerMainOpen();
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
	
	private void FabUnlock1() {
		cosmeticManager.GetComponent<CosmeticManager>().enemyDeathUnlocked = true;
	}
	
	private void FabUnlock2() {
		cosmeticManager.GetComponent<CosmeticManager>().playerTrailUnlocked = true;
	}
	
	private void FabUnlock3() {
		
	}
	
	private void FabUnlock4() {
		
	}
	private void FabUnlock5() {
		
	}
	
	private void UpdateScore(int newScore) {
		score = newScore;
	}
	
}
