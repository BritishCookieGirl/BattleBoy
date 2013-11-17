using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	
	private GameObject player;
	private Transform mood;
	public float speed = 2.0f;
	public enum EnemyState {alert, attack, idle};
	public EnemyState currentState;
	public Transform pacePoint1, pacePoint2;
	private bool point1;
	public Transform spawnPoint;
	//private bool attacking;
	private float nextAttack, attackTime; 
		
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		mood = transform.FindChild("Mood");
		pacePoint1 = spawnPoint.transform.Find("PacePoint1");
		pacePoint2 = spawnPoint.transform.Find("PacePoint2");
		attackTime = 1.0f;
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
		
		if (GameManager.currentTime > nextAttack) {
			
			//
			//attack code here
			//
			
			nextAttack = GameManager.currentTime + attackTime + Random.Range(0,3);
		}
		
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
		
		float toPoint1 = Mathf.Abs((transform.position.x - pacePoint1.position.x));
		float toPoint2 = Mathf.Abs((transform.position.x - pacePoint2.position.x));
		
		if (toPoint1 < 0.4f) {
			point1 = false;
		}
		if (toPoint2 < 0.4f) {
			point1 = true;
		}

		Vector3 dest;
		
		if (point1) {
			dest = pacePoint1.position;
		} else {
			dest = pacePoint2.position;
		}
		
		transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
	}
	
	private void OnDestroy() {
		//spawnPoint.GetComponent<EnemySpawner> ().RequestRespawn();
	}
}

