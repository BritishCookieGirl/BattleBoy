using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
	
	public Camera startMenuCam, mainLevelCam, unlockStoreCam, creditsCam, winCam;
	public Light sceneLight;
	public Transform audioListener;
	public GameObject startGUI, levelGUI, storeGUI, pointTally, creditsGUI;
	
	void Awake() {
		GameManager.GameStart += StartGame;
		GameManager.GameOver += EndGame;
		GameManager.LevelStart += StartLevel;
		GameManager.LevelComplete += EndLevel;
		GameManager.StoreOpen += OpenStore;
		GameManager.StoreClosed += CloseStore;
		GameManager.CreditsOpen += OpenCredits;
		GameManager.CreditsClose += CloseCredits;
		GameManager.GameWon += WinGame;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void StartGame() {
		
		ChangeCameras(startMenuCam,startGUI,false);
		
	}
	
	private void EndGame() {
		
		ChangeCameras(startMenuCam,startGUI,false);
		
	}
	
	private void StartLevel() {

		ChangeCameras(mainLevelCam,levelGUI,false);

		startGUI.SetActive(false);
		levelGUI.SetActive(true);
		storeGUI.SetActive(false);
		pointTally.SetActive(false);
		creditsGUI.SetActive(false);
		
		startMenuCam.enabled = false;
		mainLevelCam.enabled = true;
            mainLevelCam.orthographicSize = 4;
            mainLevelCam.farClipPlane = 20;
		unlockStoreCam.enabled = false;
		creditsCam.enabled = false;
		winCam.enabled = false;
		
		sceneLight.enabled = false;
	}
	
	private void EndLevel() {

		ChangeCameras(mainLevelCam,pointTally,false);
		
	}
	
	private void OpenStore() {

		ChangeCameras(unlockStoreCam,storeGUI,true);
		
	}
	
	private void CloseStore() {

		ChangeCameras(mainLevelCam,levelGUI,false);
		
	}
	
	private void OpenCredits() {

		ChangeCameras(creditsCam,creditsGUI,true);
		
	}
	
	private void CloseCredits() {
		
		ChangeCameras(startMenuCam,startGUI,false);
		
	}
	
	private void WinGame() {

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
		
		startMenuCam.enabled = false;
		mainLevelCam.enabled = false;
		unlockStoreCam.enabled = false;
		creditsCam.enabled = false;
		winCam.enabled = false;
		
		sceneLight.enabled = false;
		
		onCamera.enabled = true;
		AlignAudio(onCamera);
		onGUI.SetActive(true);
		
		if (lightsNeeded) {
			sceneLight.enabled = true;
		}	
	}
	
}
