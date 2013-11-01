using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	//SIMPLE TEST CODE TO GET DAMAGE WORKING ON ENEMIES PROBABLY GOING TO SCRAP ALL OF THIS
	
	private int attackStrength = 20;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Attack(Enemy enemy) {
		enemy.ReceiveDamage("test", attackStrength);
		ComboManager.UpdateCombo(1);
	}
	
	private void OnTriggerStay(Collider other) {
		
		if (other.name == "Enemy(Clone)" && Input.GetButtonDown("Fire1")) {

			Enemy e = (Enemy)other.gameObject.GetComponent("Enemy");
			Attack (e);	
		}
		
	}
	
	
	
}
