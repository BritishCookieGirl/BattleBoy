using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	
	private GameObject player;
	private Transform mood;
	public float speed = 2.0f;
	public enum EnemyState {alert, attack, idle};
	public EnemyState currentState;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		mood = transform.FindChild("Mood");
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentState) {
		case EnemyState.idle:
			//print("Chillin");
			Idle();
			break;
		case EnemyState.alert:
			//print("Who dat is?!");
			Alert();
			break;
		case EnemyState.attack:
			//print("YAA BITCH!!");
			Attack();
			break;
		default:
			break;
		}
	
	}
	
	private void Attack() {
		//attack player
		mood.renderer.material.color = Color.red;
		
		Vector3 dest = new Vector3(
			player.transform.position.x,
			transform.position.y
			, transform.position.z);
		
		transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
	}
	
	private void Alert() {
		//pursue player
		Vector3 dest = new Vector3(
			player.transform.position.x,
			transform.position.y
			, transform.position.z);
		
		
		transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
		
		mood.renderer.material.color = Color.yellow;
	}
	
	private void Idle() {
		//ignore player
		mood.renderer.material.color = Color.green;
	}
}

