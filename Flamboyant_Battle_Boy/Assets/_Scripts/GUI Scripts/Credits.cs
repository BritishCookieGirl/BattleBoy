using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {
	
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
		
		if (GUI.Button (new Rect (520, 450, 180, 75), "", buttonStyle)) {
			GameManager.TriggerCreditsClose();
		}
		
		GUI.Label(new Rect(545,445,225,75), "Back",bTextShaddowStyle);
		GUI.Label(new Rect(545,445,225,75), "Back",bTextStyle);
	}
}
