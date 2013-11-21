using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
	
	public Camera startMenuCam, mainLevelCam, unlockStoreCam, creditsCam, winCam, tutorialCam;
	public Light sceneLight;
	public Transform audioListener;
	public GameObject startGUI, levelGUI, storeGUI, pointTally, creditsGUI, tutorialGUI;
	
	void Awake() {
		GameManager.GameStart += StartMenu;
		GameManager.GameOver += EndGame;
		GameManager.LevelStart += StartLevel;
		GameManager.LevelComplete += EndLevel;
		GameManager.StoreOpen += OpenStore;
		GameManager.CreditsOpen += OpenCredits;
		GameManager.GameWon += WinGame;
		GameManager.MainOpen += StartMenu;
		GameManager.TutorialOpen += OpenTutorial;
		
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	private void EndGame() {
		
		ChangeCameras(startMenuCam,startGUI,false);
		
	}
	
	private void StartMenu() {
		ChangeCameras(startMenuCam,startGUI,false);
	}
	
	private void StartLevel() {

		ChangeCameras(mainLevelCam,levelGUI,false);

        mainLevelCam.orthographicSize = 4;
        mainLevelCam.farClipPlane = 20;

	}
	
	private void EndLevel() {

		ChangeCameras(mainLevelCam,pointTally,false);
		
	}
	
	private void OpenStore() {

		ChangeCameras(unlockStoreCam,storeGUI,true);
		
	}
	
	
	private void OpenCredits() {

		ChangeCameras(creditsCam,creditsGUI,false);
		
	}
	
	private void OpenTutorial() {
		ChangeCameras(tutorialCam,tutorialGUI,false);
	}
	
	
	private void WinGame()
    {
        GameObject.FindGameObjectWithTag("Audio").SendMessage("PlaySoundEffect", "Cheer");

		ChangeCameras(winCam,null,false);		
	}
	
	
	private void AlignAudio (Camera activeCamera) {
		
		audioListener.parent = activeCamera.transform;
		audioListener.localPosition = Vector3.zero;
		audioListener.localRotation = Quaternion.identity;
		
	}
	
	
	private void ChangeCameras(Camera onCamera, GameObject onGUI, bool lightsNeeded) {
		
		startGUI.SetActive(false);
		levelGUI.SetActive(false);
		storeGUI.SetActive(false);
		pointTally.SetActive(false);
		creditsGUI.SetActive(false);
		tutorialGUI.SetActive(false);
		
		startMenuCam.enabled = false;
		mainLevelCam.enabled = false;
		unlockStoreCam.enabled = false;
		creditsCam.enabled = false;
		winCam.enabled = false;
		tutorialCam.enabled = false;
		
		sceneLight.enabled = false;
		
		onCamera.enabled = true;
		onGUI.SetActive(true);
		
		AlignAudio(onCamera);
		
		if (lightsNeeded) {
			sceneLight.enabled = true;
		}	
	}
	
}
