using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public float speed = 5f;
	
	// Use this for initialization
	void Start () {
		
		transform.parent = GameObject.Find("Enemy Spawn").transform;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Translate(Vector3.left * speed * Time.deltaTime);

	}
	
}
