using UnityEngine;
using System.Collections;

public class EnemyCombat : MonoBehaviour
{
    private Vector3 liftEnd = new Vector3(0f, .5f, .5f);
    private Vector3 smashEnd = new Vector3(0f, -.5f, .5f);
    private Vector3 neutralEnd = new Vector3(0f, 0f, .5f);
    private Vector3 dest;

    private float journeyLength;
    private float distCovered;
    private float fracJourney;
    private float startTime;
    private float moveSpeed = 2f;

    private bool lerpStarted;

    public bool isDead = false;

    public void SetLerpStartedFalse()
    {
        lerpStarted = false;
    }

    public void PerformAttack()
    {
        this.transform.localPosition = Vector3.zero;


        //journeyLength = Vector3.Distance(this.transform.localPosition, dest);
        startTime = Time.time;
        //lerpStarted = true;
    }

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
        fracJourney = 0f;
        dest = Vector3.zero;
        lerpStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        //distCovered = (Time.time - startTime) * moveSpeed;
        //fracJourney = distCovered / journeyLength;

        //if (lerpStarted)
        //{
            //this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, dest, fracJourney);
        //}
    }

    void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.tag == "Player" && !isDead)
        {
            GameObject.FindGameObjectWithTag("Audio").SendMessage("PlaySoundEffect", "Kick");

            Vector3 force;
            int attackStrength;

            Vector3 direction = collide.gameObject.GetComponent<CharacterController2D>().GetDirection();
            direction.x *= -1;

            force = new Vector3(5f, 5f, 0f); 
            attackStrength = 2;

            force.x *= direction.x;
            collide.gameObject.GetComponent<PlayerCombat>().AddImpact(force, attackStrength);
            collide.gameObject.GetComponent<PlayerCombat>().InterruptCombo();
        }
    }
}