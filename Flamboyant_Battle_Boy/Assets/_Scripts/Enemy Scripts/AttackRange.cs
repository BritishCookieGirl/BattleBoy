using UnityEngine;
using System.Collections;

public class AttackRange : MonoBehaviour {
	
	private EnemyAttack e;
	
	// Use this for initialization
	void Start () {
		e = transform.parent.gameObject.GetComponent<EnemyAttack>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.name == "Player") {
			e.currentState = EnemyAttack.EnemyState.attack;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.name == "Player") {
			e.currentState = EnemyAttack.EnemyState.alert;
		}
	}
}
