using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private string attackType;
    public string AttackType { get { return attackType; } set { attackType = value; } }

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

    public void SetLerpStartedFalse()
    {
        lerpStarted = false;
    }

    public void SetAttackType(string type)
    {
        AttackType = type;

        switch (AttackType)
        {
            case "Lift": dest = liftEnd; break;
            case "Smash": dest = smashEnd; break;
            case "Neutral": dest = neutralEnd; break;
            case "Special1": dest = neutralEnd; break;
            default: dest = Vector3.zero; break;
        }

        PerformAttack();
    }

    private void PerformAttack()
    {
        this.transform.localPosition = Vector3.zero;

        journeyLength = Vector3.Distance(this.transform.localPosition, dest);
        startTime = Time.time;
        lerpStarted = true;
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
        distCovered = (Time.time - startTime) * moveSpeed;
        fracJourney = distCovered / journeyLength;

        if (lerpStarted)
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, dest, fracJourney);
        }
    }

    void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.tag == "Enemy")
        {
            Vector3 force;
            switch (AttackType)
            {
                case "Lift": force = new Vector3(0f, 400f, 0f); break;
                case "Smash": force = new Vector3(0f, -400f, 0f); break;
                case "Neutral": force = new Vector3(0f, 0f, 400f); break;
                case "Special1": force = new Vector3(0f, 0f, 10000f); break;
                default: force = Vector3.zero; break;
            }
            collide.gameObject.rigidbody.AddForce(force);

            int attackStrength = 20;
            collide.gameObject.GetComponent<Enemy>().ReceiveDamage(AttackType, attackStrength);
        }
    }
}