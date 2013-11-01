using UnityEngine;
using System.Collections;

public class AlertRange : MonoBehaviour {

	private EnemyAttack e;
	
	// Use this for initialization
	void Start () {
		e = transform.parent.gameObject.GetComponent<EnemyAttack>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		e.currentState = EnemyAttack.EnemyState.alert;
	}
	
	void OnTriggerExit(Collider other) {
		e.currentState = EnemyAttack.EnemyState.idle;
	}
}
