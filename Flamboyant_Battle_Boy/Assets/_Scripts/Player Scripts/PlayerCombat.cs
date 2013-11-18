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

    private OTAnimatingSprite runAnimation;
    private OTAnimatingSprite attackAnimation;
    private OTAnimatingSprite idleAnimation;
    private bool isRunPlaying = false;
    private bool isAttackPlaying = false;

    private bool isMoving = false;
    public bool IsMoving { get { return isMoving; } set { isMoving = value; } }
	
    // Use this for initialization
    void Start()
    {
        attackAnimation = OT.ObjectByName("PlayerAttackAnimSprite") as OTAnimatingSprite;
        attackAnimation.onAnimationFinish = OnAnimationFinish;
        attackAnimation.visible = false;
        runAnimation = OT.ObjectByName("PlayerRunAnimSprite") as OTAnimatingSprite;
        runAnimation.visible = false;
        idleAnimation = OT.ObjectByName("PlayerIdleAnimSprite") as OTAnimatingSprite;
        idleAnimation.visible = true;

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
        HandleAnimations();

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
        else if(currentCombo < ComboLength && attackStartTime == 0)
        {
            //No attack currently happening.

            if (Input.GetButton("LiftAttack"))
            {
                AttackBox.SetActive(true);
                attackLength = 0.3f;
                attackStartTime = Time.time;
                AttackBox.GetComponent<PlayerAttack>().SetAttackType("Lift");
                currentCombo++;
                PlayAnimation();
            }
            if (Input.GetButton("SmashAttack"))
            {
                AttackBox.SetActive(true);
                attackLength = 0.3f;
                attackStartTime = Time.time;
                AttackBox.GetComponent<PlayerAttack>().SetAttackType("Smash");
                currentCombo++;
                PlayAnimation();
            }
            if (Input.GetButton("NeutralAttack"))
            {
                AttackBox.SetActive(true);
                attackLength = 0.3f;
                attackStartTime = Time.time;
                AttackBox.GetComponent<PlayerAttack>().SetAttackType("Neutral");
                currentCombo++;
                PlayAnimation();
            }
            if (Input.GetButton("SpecialAttack1"))
            {
                AttackBox.SetActive(true);
                attackLength = 0.3f; // 1f;
                attackStartTime = Time.time;
                AttackBox.GetComponent<PlayerAttack>().SetAttackType("Special1");
                currentCombo++;
                PlayAnimation();
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

    private void HandleAnimations()
    {
        if (IsMoving && !isAttackPlaying && !isRunPlaying) //is moving and not attacking but run animation is not playing
        {
            isRunPlaying = true;
            runAnimation.visible = true;
            runAnimation.Play();
            idleAnimation.Stop();
            idleAnimation.visible = false;
        }
        if (!IsMoving && !isAttackPlaying) //is not moving and not attacking
        {
            isRunPlaying = false;
            runAnimation.Stop();
            runAnimation.visible = false;
            idleAnimation.visible = true;
            idleAnimation.Play();
        }
    }

    private void PlayAnimation()
    {
        //if (!isAnimPlaying)
        //{
            isAttackPlaying = true;
            attackAnimation.visible = true;
            attackAnimation.PlayOnce("Attack");
            isRunPlaying = false;
            runAnimation.Stop();
            runAnimation.visible = false;
            idleAnimation.Stop();
            idleAnimation.visible = false;
        //}
    }

    public void OnAnimationFinish(OTObject owner)
    {
        if (owner == attackAnimation && isAttackPlaying)
        {
            isAttackPlaying = false;
            isRunPlaying = false;
            attackAnimation.visible = false;
            runAnimation.visible = false;
            idleAnimation.visible = true;
            idleAnimation.Play();
        }
    }
}