using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public GameManager gm;
	private int attackStrength = 20;
	
	// Use this for initialization
	void Start () {
		gm.resetCombo();
	
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetButtonDown("Fire1")) {
//			gm.updateCombo(1);
//			gm.updateScore(100);
//		}
	}
	
	public void Attack(Enemy enemy) {
		int damage = enemy.TakeDamage(attackStrength);
		if (damage > 0) {
			gm.updateScore(damage * 10);
			gm.updateCombo(-1);
		}
	}
	
	private void OnTriggerStay(Collider other) {
		
		if (other.name == "Enemy(Clone)" && Input.GetButtonDown("Fire1")) {

			Enemy e = (Enemy)other.gameObject.GetComponent("Enemy");
			Attack (e);	
		}
		
	}
	
	
	
}
