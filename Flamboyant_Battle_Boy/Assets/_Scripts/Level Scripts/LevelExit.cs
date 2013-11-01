using UnityEngine;
using System.Collections;

public class LevelExit : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	// Triggers level end when entered by Player
	void OnTriggerEnter (Collider other)
	{
		if (other.name == "Player") {
<<<<<<< HEAD:Flamboyant_Battle_Boy/Assets/_Scripts/Level Scripts/LevelExit.cs
			GameEventManager.TriggerLevelComplete ();
=======
			GameManager.TriggerLevelComplete();
>>>>>>> Eric-Dev-Branch:Flamboyant_Battle_Boy/Assets/_Scripts/Level Scripts/LevelExit.cs
		}
	}

}
