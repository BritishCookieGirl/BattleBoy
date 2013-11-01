using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    public GameObject AttackBox;

    private int comboLength = 0;
    public int ComboLength { get { return comboLength; } set { comboLength = value; } }

    private float attackStartTime;
    private float attackLength;

    // Use this for initialization
    void Start()
    {
        //AttackBox.SetActive(false);
        attackStartTime = 0f;
        attackLength = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.time - attackStartTime;

        if (attackStartTime != 0 && elapsedTime > attackLength)
        {
            //Attack has finished
            attackStartTime = 0;
            AttackBox.SetActive(false);
            AttackBox.GetComponent<PlayerAttack>().SetLerpStartedFalse();
        }
        else if (attackStartTime != 0 && elapsedTime < attackLength)
        {
            //Attack is currently happening.
        }
        else
        {
            //No attack currently happening.

            if (Input.GetButton("LiftAttack"))
            {
                AttackBox.SetActive(true);
                attackLength = 0.2f;
                attackStartTime = Time.time;
                AttackBox.GetComponent<PlayerAttack>().SetAttackType("Lift");
            }
            if (Input.GetButton("SmashAttack"))
            {
                AttackBox.SetActive(true);
                attackLength = 0.2f;
                attackStartTime = Time.time;
                AttackBox.GetComponent<PlayerAttack>().SetAttackType("Smash");
            }
            if (Input.GetButton("NeutralAttack"))
            {
                AttackBox.SetActive(true);
                attackLength = 0.2f;
                attackStartTime = Time.time;
                AttackBox.GetComponent<PlayerAttack>().SetAttackType("Neutral");
            }
            if (Input.GetButton("SpecialAttack1"))
            {
                AttackBox.SetActive(true);
                attackLength = 1f;
                attackStartTime = Time.time;
                AttackBox.GetComponent<PlayerAttack>().SetAttackType("Special1");
            }
        }
    }

    public void IncreaseComboLength()
    {
        if (ComboLength < 10)
        {
            ComboLength++;
        }
    }
}