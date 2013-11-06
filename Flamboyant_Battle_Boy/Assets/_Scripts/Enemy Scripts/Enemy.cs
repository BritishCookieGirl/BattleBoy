using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private float lockZDepth = 7f; //= GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>().lockZDepth;
	public float speed = 5f;
	public bool canMove = true;
	public int health = 100;
	public int defense = 0;
	public int pointValue = 100;

    private CharacterController character;

//	public delegate void DamageEventHandler(int points);
//	public static event DamageEventHandler TookDamage;
	
	// Called before Start
	void Awake() {
		GameEventManager.LevelStart += LevelStart;
		GameEventManager.LevelComplete += LevelComplete;		
	}
	
	// Use this for initialization
	void Start ()
    {
        character = this.GetComponent<CharacterController>();
        impact = Vector3.zero;
		//transform.parent = GameObject.Find("Enemy Spawn").transform;

        //Prevent enemies from colliding with each other and player
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        foreach (GameObject enemy in enemyList)
        {
            if (enemy != this.gameObject)
            {
                //Physics.IgnoreCollision(enemy.GetComponent<BoxCollider>(), this.GetComponent<BoxCollider>());
                Physics.IgnoreCollision(enemy.GetComponent<CharacterController>(), this.GetComponent<CharacterController>());
            }
            Physics.IgnoreCollision(enemy.GetComponent<CharacterController>(), player.GetComponent<CharacterController>());
        }
	}

    private Vector3 impact;

    public void AddImpact(Vector3 force, float strength)
    {
        //force.Normalize();
        //impact += force.normalized * strength;
        impact += force * strength;
    }
	
	// Update is called once per frame
    void Update()
    {
        //apply impact force
        if (impact.magnitude > 0.2) character.Move(impact * Time.deltaTime);
        //consumes the impact energy each cycle
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }

    void FixedUpdate()
    {
        // Make sure we are absolutely always in the 2D plane.
        transform.position = new Vector3(transform.position.x, transform.position.y, lockZDepth);
    }
	
	// Called Automatically anytime level starts - set default variables here
	private void LevelStart() {
		canMove = true;	
	}
	
	// Called automatically anytime level finishes - set win/lose conditions here
	private void LevelComplete() {
		canMove = false;	
	}
	
	// Use by external objects to apply damage to enemy instance
	public void ReceiveDamage(string attackType, int attackStrength) {
		int damageTaken = attackStrength - defense;
		health -= damageTaken;
		if (health <= 0) {
			Destroy(gameObject);	
		}
		
		ScoreManager.UpdateScore(damageTaken);

	}
	
	// Called before object death - use to tidy lose ends
	private void OnDestroy() {
		EnemySpawner.currentEnemies--;
		ScoreManager.UpdateScore(pointValue);
	}
	
	
}
