﻿using UnityEngine;
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
		
		if (GUI.Button (new Rect (340, 510, 180, 75), "", buttonStyle)) {
			GameManager.TriggerMainOpen();
		}
		
		GUI.Label(new Rect(355,522,225,75), "Back",bTextShaddowStyle);
		GUI.Label(new Rect(355,522,225,75), "Back",bTextStyle);
	}
}
