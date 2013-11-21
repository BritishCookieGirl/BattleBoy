using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
	
	public GUIStyle buttonStyle;
	public GUIStyle bTextStyle;
	public GUIStyle bTextShaddowStyle;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		
		if (GUI.Button (new Rect (20, 510, 220, 75), "", buttonStyle)) {
			GameManager.TriggerMainOpen();
		}
		
		GUI.Label(new Rect(58,520,225,75), "Back",bTextShaddowStyle);
		GUI.Label(new Rect(58,520,225,75), "Back",bTextStyle);
	}
}
