using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
	
	public Camera startMenuCam, mainLevelCam, unlockStoreCam, creditsCam;
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
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void StartGame() {
		startGUI.SetActive(true);
		levelGUI.SetActive(false);
		storeGUI.SetActive(false);
		pointTally.SetActive(false);
		creditsGUI.SetActive(false);
		
		startMenuCam.enabled = true;
		mainLevelCam.enabled = false;
		unlockStoreCam.enabled = false;
		creditsCam.enabled = false;
		
		sceneLight.enabled = false;
		
		AlignAudio(startMenuCam);
	}
	
	private void EndGame() {
		startGUI.SetActive(true);
		levelGUI.SetActive(false);
		storeGUI.SetActive(false);
		pointTally.SetActive(false);
		creditsCam.enabled = false;
		creditsGUI.SetActive(false);
		
		startMenuCam.enabled = true;
		mainLevelCam.enabled = false;
		unlockStoreCam.enabled = false;
		
		sceneLight.enabled = false;
		
		AlignAudio(startMenuCam);
	}
	
	private void StartLevel() {
		startGUI.SetActive(false);
		levelGUI.SetActive(true);
		storeGUI.SetActive(false);
		pointTally.SetActive(false);
		creditsCam.enabled = false;
		creditsGUI.SetActive(false);
		
		startMenuCam.enabled = false;
		mainLevelCam.enabled = true;
		unlockStoreCam.enabled = false;
		
		sceneLight.enabled = false;
		
		AlignAudio(mainLevelCam);
	}
	
	private void EndLevel() {
		startGUI.SetActive(false);
		levelGUI.SetActive(false);
		storeGUI.SetActive(false);
		pointTally.SetActive(true);
		creditsCam.enabled = false;
		creditsGUI.SetActive(false);
		
		startMenuCam.enabled = false;
		mainLevelCam.enabled = true;
		unlockStoreCam.enabled = false;
		
		sceneLight.enabled = false;
		
		AlignAudio(mainLevelCam);
	}
	
	private void OpenStore() {
		startGUI.SetActive(false);
		levelGUI.SetActive(false);
		storeGUI.SetActive(true);
		pointTally.SetActive(false);
		creditsCam.enabled = false;
		creditsGUI.SetActive(false);
		
		startMenuCam.enabled = false;
		mainLevelCam.enabled = false;
		unlockStoreCam.enabled = true;
		
		sceneLight.enabled = true;
		
		AlignAudio(unlockStoreCam);
	}
	
	private void CloseStore() {
		startGUI.SetActive(true);
		levelGUI.SetActive(false);
		storeGUI.SetActive(false);
		pointTally.SetActive(false);
		creditsGUI.SetActive(false);
		
		startMenuCam.enabled = true;
		mainLevelCam.enabled = false;
		unlockStoreCam.enabled = false;
		creditsCam.enabled = false;
		
		sceneLight.enabled = false;
		
		AlignAudio(startMenuCam);
	}
	
	private void OpenCredits() {
		startGUI.SetActive(false);
		levelGUI.SetActive(false);
		storeGUI.SetActive(false);
		pointTally.SetActive(false);
		creditsGUI.SetActive(true);
		
		startMenuCam.enabled = false;
		mainLevelCam.enabled = false;
		unlockStoreCam.enabled = false;
		creditsCam.enabled = true;
		
		sceneLight.enabled = true;
		
		AlignAudio(creditsCam);
	}
	
	
	private void AlignAudio (Camera activeCamera) {
		audioListener.parent = activeCamera.transform;
		audioListener.localPosition = Vector3.zero;
		audioListener.localRotation = Quaternion.identity;
	}
	
}
