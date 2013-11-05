using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    public GameObject AttackBox;

    private int comboLength = 5; //Max 10
    public int ComboLength { get { return comboLength; } set { comboLength = value; } }

    private float attackStartTime;
    private float attackLength;

    private int currentCombo;
    private float comboEndTime;
    private float comboEndLength;

    // Use this for initialization
    void Start()
    {
        AttackBox.SetActive(false);
        attackStartTime = 0f;
        attackLength = 1f;
        comboEndTime = Time.time;
        comboEndLength = 2f;
        currentCombo = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float elapsedAttackTime = Time.time - attackStartTime;
        float elapsedComboEndTime = Time.time - comboEndTime;

        if (elapsedComboEndTime > comboEndLength)
        {
            currentCombo = 0;
            comboEndTime = Time.time;
        }

        if (attackStartTime != 0 && elapsedAttackTime > attackLength)
        {
            //Attack has finished
            attackStartTime = 0;
            AttackBox.SetActive(false);
            AttackBox.GetComponent<PlayerAttack>().SetLerpStartedFalse();
            comboEndTime = Time.time;
        }
        else if (attackStartTime != 0 && elapsedAttackTime < attackLength)
        {
            //Attack is currently happening.
        }
        else if(currentCombo < ComboLength)
        {
            //No attack currently happening.

            if (Input.GetButton("LiftAttack"))
            {
                AttackBox.SetActive(true);
                attackLength = 0.2f;
                attackStartTime = Time.time;
                AttackBox.GetComponent<PlayerAttack>().SetAttackType("Lift");
                currentCombo++;
            }
            if (Input.GetButton("SmashAttack"))
            {
                AttackBox.SetActive(true);
                attackLength = 0.2f;
                attackStartTime = Time.time;
                AttackBox.GetComponent<PlayerAttack>().SetAttackType("Smash");
                currentCombo++;
            }
            if (Input.GetButton("NeutralAttack"))
            {
                AttackBox.SetActive(true);
                attackLength = 0.2f;
                attackStartTime = Time.time;
                AttackBox.GetComponent<PlayerAttack>().SetAttackType("Neutral");
                currentCombo++;
            }
            if (Input.GetButton("SpecialAttack1"))
            {
                AttackBox.SetActive(true);
                attackLength = 1f;
                attackStartTime = Time.time;
                AttackBox.GetComponent<PlayerAttack>().SetAttackType("Special1");
                currentCombo++;
            }
            //Debug.Log("Current Combo: " + currentCombo);
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