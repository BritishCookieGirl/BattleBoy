using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
	
	public Texture2D buttonTexture, windowTexture, bgTexture;
	public Texture2D titleImg, startImg, tutorialImg, optionsImg, creditsImg;
		
	
	private float height = 500;
	private float width = 500;
	private float bWidth = 300;
	private float bHeight = 60;
	
	private Rect startMenuRect,buttonRect;
	
	public GUIStyle startStyle, buttonStyle;
	
	// Use this for initialization
	void Start () {
		startMenuRect = new Rect((Screen.width/2 - width/2), 0, width, height);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		startMenuRect = GUI.Window(0,startMenuRect, StartWindow, windowTexture,startStyle);
	}
	
	private void StartWindow(int windowID) {
		GUI.Label(new Rect(75,90,350,70),titleImg,startStyle);
		if (GUI.Button(new Rect(155, 140, bWidth, bHeight), startImg, buttonStyle)) {
			StartClicked();
		}
		if (GUI.Button(new Rect(155, 205, bWidth, bHeight), tutorialImg, buttonStyle)) {
			TutorialClicked();
		}
		if (GUI.Button(new Rect(155, 275, bWidth, bHeight), optionsImg, buttonStyle)) {
			OptionsClicked();
		}
		if (GUI.Button(new Rect(155, 340, bWidth, bHeight), creditsImg, buttonStyle)) {
			CreditsClicked();
		}
		
	}
	
	private void StartClicked() {
		//Start Level
	}
	
	private void TutorialClicked() {
		//Start Tutorial
	}
	
	private void OptionsClicked() {
		//Start Options Screen
	}
	
	private void CreditsClicked() {
		//Start Credits
	}
	
}
