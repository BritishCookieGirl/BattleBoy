using UnityEngine;
using System.Collections;
using System.Linq;

public class Enemy : MonoBehaviour
{
    private float lockZDepth = 7f; //= GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>().lockZDepth;
	public float speed = 5f;
	public bool canMove = true;
	public int health = 200;
	public int defense = 0;
	public int pointValue = 200;
	public ParticleSystem deathRainbows;
	public TextMesh healthCounter;
	public Transform spawnPoint;
	
	public CosmeticManager cosmeticManager;
	public bool particlesUnlocked;

    private CharacterController character;

    private OTAnimatingSprite deathAnimation;
    private OTAnimatingSprite runAnimation;

    private Vector3 prevLoc = Vector3.zero;
    private Vector3 prevVel = Vector3.zero;
	
	private bool alive;

//	public delegate void DamageEventHandler(int points);
//	public static event DamageEventHandler TookDamage;
	
	// Called before Start
	void Awake() {
		GameManager.LevelStart += LevelStart;
		GameManager.LevelComplete += LevelComplete;
		
		cosmeticManager = GameObject.Find("Cosmetics").GetComponent<CosmeticManager> ();
		particlesUnlocked = cosmeticManager.enemyDeathUnlocked;

        alive = true;
	}
	
	// Use this for initialization
	void Start ()
    {
        deathAnimation = this.gameObject.GetComponentsInChildren<OTAnimatingSprite>().Single(x => x.name.Contains("EnemyHitAnimSprite"));
        deathAnimation.onAnimationStart = OnAnimationStart;
        deathAnimation.onAnimationFinish = OnAnimationFinish;
        deathAnimation.visible = false;
        runAnimation = this.gameObject.GetComponentsInChildren<OTAnimatingSprite>().Single(y => y.name.Contains("EnemyRunAnimSprite"));
        runAnimation.visible = true;

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

    //private void CreateAnimSprites()
    //{
    //    OTSpriteSheet sheet1 = OT.CreateObject(OTObjectType.SpriteSheet).GetComponent<OTSpriteSheet>();
    //    sheet1.texture = (Texture2D)Resources.Load("monster run", typeof(Texture2D));
    //    sheet1.framesXY = new Vector2(2, 2);
    //    OTAnimation animation1 = OT.CreateObject(OTObjectType.Animation).GetComponent<OTAnimation>();
    //    OTAnimationFrameset frameset1 = new OTAnimationFrameset();
    //    frameset1.container = sheet1;
    //    frameset1.startFrame = 0;
    //    frameset1.endFrame = 3;
    //    frameset1.name = "Run";
    //    animation1.framesets = new OTAnimationFrameset[] { frameset1 };
    //    animation1.duration = 0.1333f;
    //    animation1.name = "EnemyRunAnim";
    //    runAnimation = OT.CreateObject(OTObjectType.AnimatingSprite).GetComponent<OTAnimatingSprite>();
    //    runAnimation.animation = animation1;
    //    runAnimation.startAtRandomFrame = false;
    //    runAnimation.looping = true;
    //    runAnimation.playOnStart = true;
    //    runAnimation.visible = true;

    //    OTSpriteSheet sheet2 = OT.CreateObject(OTObjectType.SpriteSheet).GetComponent<OTSpriteSheet>();
    //    sheet2.texture = (Texture2D)Resources.Load("monster knockback", typeof(Texture2D));
    //    sheet2.framesXY = new Vector2(2, 3);
    //    OTAnimation animation2 = OT.CreateObject(OTObjectType.Animation).GetComponent<OTAnimation>();
    //    OTAnimationFrameset frameset2 = new OTAnimationFrameset();
    //    frameset2.container = sheet2;
    //    frameset2.startFrame = 0;
    //    frameset2.endFrame = 5;
    //    frameset2.name = "Hit";
    //    animation2.framesets = new OTAnimationFrameset[] { frameset2 };
    //    animation2.duration = 0.2f;
    //    animation2.name = "EnemyHitAnim";
    //    deathAnimation = OT.CreateObject(OTObjectType.AnimatingSprite).GetComponent<OTAnimatingSprite>();
    //    deathAnimation.animation = animation2;
    //    deathAnimation.startAtRandomFrame = false;
    //    deathAnimation.looping = false;
    //    deathAnimation.playOnStart = false;
    //    deathAnimation.visible = false;
    //}

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
		if (alive) {
			//apply impact force
			if (impact.magnitude > 0.2) character.Move(impact * Time.deltaTime);
			//consumes the impact energy each cycle
			impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
			
			Vector3 curVel = (character.transform.localPosition - prevLoc);
			if (curVel.x > 0)
			{
				//character.transform.LookAt(new Vector3(100f, 0f, 0f));
				runAnimation.flipHorizontal = true;
			}
			else
			{
				//character.transform.LookAt(new Vector3(-100f, 0f, 0f));
				runAnimation.flipHorizontal = false;
			}
			prevLoc = character.transform.localPosition;
			prevVel = curVel;
		}
    }

    void FixedUpdate()
    {
        // Make sure we are absolutely always in the 2D plane.
        transform.position = new Vector3(transform.position.x, transform.position.y, lockZDepth);
    }
	
	// Called Automatically anytime level starts - set default variables here
	private void LevelStart() {
		canMove = true;	
		deathRainbows.Stop();
		particlesUnlocked = cosmeticManager.enemyDeathUnlocked;
	}
	
	// Called automatically anytime level finishes - set win/lose conditions here
	private void LevelComplete() {
		LevelCleanup();
	}
	
	// Use by external objects to apply damage to enemy instance
	public void ReceiveDamage(string attackType, int attackStrength) {
		int damageTaken = attackStrength - defense;
		health -= damageTaken;
		healthCounter.text = health.ToString();
		if (health <= 0) {
			Die ();
			spawnPoint.GetComponent<EnemySpawner> ().RequestRespawn();
		}
		ScoreManager.AddToScore(10);
		ComboManager.IncrementCombo();
	}
	
	// Called before object death - use to tidy lose ends
	private void OnDestroy() {
		GameManager.LevelStart -= LevelStart;
		GameManager.LevelComplete -= LevelComplete;
	}

    public void LevelCleanup()
    {
        ScoreManager.AddToScore(pointValue);

        this.gameObject.GetComponentInChildren<EnemyCombat>().isDead = true;

        Destroy(gameObject, 3);
        gameObject.collider.enabled = false;
        gameObject.renderer.enabled = false; ;
    }
	
	public void Die()
    {
		ScoreManager.AddToScore(pointValue);

        this.gameObject.GetComponentInChildren<EnemyCombat>().isDead = true;

        runAnimation.Stop();
        runAnimation.visible = false;
		
        deathAnimation.visible = true;
        deathAnimation.PlayOnce();
        
        //DeathWait();

		if (particlesUnlocked) {
			deathRainbows.Play();
		}
		
		alive = false;
		
		Destroy(gameObject,3);
		gameObject.collider.enabled = false;
		gameObject.renderer.enabled = false;;
		
		gameObject.GetComponent<EnemyAttack>().enabled = false;
		gameObject.GetComponent<CharacterController2D>().enabled = false;
		
	}

    private IEnumerable DeathWait()
    {
        yield return new WaitForSeconds(0.5f);
    }

    public void OnAnimationStart(OTObject owner)
    {
        if (owner == deathAnimation)
        {
            GameObject.FindGameObjectWithTag("Audio").SendMessage("PlaySoundEffect", "Die");
        }
    }

    public void OnAnimationFinish(OTObject owner)
    {
        if (owner == deathAnimation)
        {
            GameObject.FindGameObjectWithTag("Audio").SendMessage("PlaySoundEffect", "Fireworks");
        }
    }
}
