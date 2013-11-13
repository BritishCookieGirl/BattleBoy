using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
	
	public Texture2D buttonTexture, windowTexture, bgTexture;
	public Texture2D startImg, tutorialImg, optionsImg, creditsImg;
		
	private float bWidth = 250;
	private float bHeight = 100;
	
private bool showStartMenu = true;
	
	private Rect startMenuRect,buttonRect;
	
	public GUIStyle startStyle, titleStyle, shaddowTitleStyle, buttonStyle, buttonTextStyle, buttonTextShaddowStyle;
	
	// Use this for initialization
	void Start () {
		startMenuRect = new Rect(0,0,Screen.width,Screen.height);
		
		//GameManager.LevelStart += LevelStart;
		//GameManager.GameStart += StartGame;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		if (showStartMenu) {
			GUI.Box(startMenuRect,bgTexture);
			startMenuRect = GUI.Window(0,startMenuRect, StartWindow, windowTexture,startStyle);
		}
	}
	
	private void StartWindow(int windowID) {
		GUI.Label(new Rect(155,10,350,70),"Flamboyant",shaddowTitleStyle);
		GUI.Label(new Rect(165,95,350,70),"Battle Boy",shaddowTitleStyle);
		
		GUI.Label(new Rect(150,5,350,70),"Flamboyant",titleStyle);
		GUI.Label(new Rect(160,90,350,70),"Battle Boy",titleStyle);
		
		if (GUI.Button(new Rect(275, 230, bWidth, bHeight), "", buttonStyle)) {
			StartClicked();
		}
		GUI.Label(new Rect(325,235,bWidth,bHeight),"Start",buttonTextShaddowStyle);
		GUI.Label(new Rect(321,231,bWidth,bHeight),"Start",buttonTextStyle);
		
		if (GUI.Button(new Rect(275, 340, bWidth, bHeight), "", buttonStyle)) {
			QuitClicked();
		}
		GUI.Label(new Rect(345,345,bWidth,bHeight),"Quit",buttonTextShaddowStyle);
		GUI.Label(new Rect(341,341,bWidth,bHeight),"Quit",buttonTextStyle);
		
		if (GUI.Button(new Rect(275, 450, bWidth, bHeight), "", buttonStyle)) {
			CreditsClicked();
		}
		GUI.Label(new Rect(295,455,bWidth,bHeight),"Credits",buttonTextShaddowStyle);
		GUI.Label(new Rect(291,451,bWidth,bHeight),"Credits",buttonTextStyle);
		
	}
	
	private void StartClicked() {
		//Start Level
		GameManager.TriggerLevelStart();
	}
	
	private void QuitClicked() {
		//Quit Game
		
	}

	private void CreditsClicked() {
		//Start Credits
		
	}
	

}
