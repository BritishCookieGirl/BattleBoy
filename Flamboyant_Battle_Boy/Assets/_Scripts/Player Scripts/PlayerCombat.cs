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
    private OTAnimatingSprite liftAnimation;
    private OTAnimatingSprite idleAnimation;
    private OTAnimatingSprite jumpAnimation;
    private bool isRunPlaying = false;
    private bool isAttackPlaying = false;

    private bool isMoving = false;
    public bool IsMoving { get { return isMoving; } set { isMoving = value; } }

    private bool isJumping = false;
    public bool IsJumping { get { return isJumping; } set { isJumping = value; } }
	
    // Use this for initialization
    void Start()
    {
        attackAnimation = OT.ObjectByName("PlayerAttackAnimSprite") as OTAnimatingSprite;
        attackAnimation.onAnimationFinish = OnAnimationFinish;
        attackAnimation.onAnimationStart = OnAnimationStart;
        attackAnimation.visible = false;
        liftAnimation = OT.ObjectByName("PlayerAttackLiftAnimSprite") as OTAnimatingSprite;
        liftAnimation.onAnimationFinish = OnAnimationFinish;
        liftAnimation.onAnimationStart = OnAnimationStart;
        liftAnimation.visible = false;
        runAnimation = OT.ObjectByName("PlayerRunAnimSprite") as OTAnimatingSprite;
        runAnimation.visible = false;
        idleAnimation = OT.ObjectByName("PlayerIdleAnimSprite") as OTAnimatingSprite;
        idleAnimation.visible = true;
        jumpAnimation = OT.ObjectByName("PlayerJumpAnimSprite") as OTAnimatingSprite;
        jumpAnimation.visible = false;

        AttackBox.SetActive(false);
        attackStartTime = 0f;
        attackLength = 1f;
        comboEndTime = Time.time;
        comboEndLength = 2f;
        currentCombo = 0;

        controller = this.gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (Input.GetButtonDown("Jump"))
        //{
        //    HandleJump();
        //}

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
                PlayAttackAnimation("Lift");
            }
            if (Input.GetButton("SmashAttack"))
            {
                AttackBox.SetActive(true);
                attackLength = 0.3f;
                attackStartTime = Time.time;
                AttackBox.GetComponent<PlayerAttack>().SetAttackType("Smash");
                currentCombo++;
                PlayAttackAnimation("Smash");
            }
            if (Input.GetButton("NeutralAttack"))
            {
                AttackBox.SetActive(true);
                attackLength = 0.3f;
                attackStartTime = Time.time;
                AttackBox.GetComponent<PlayerAttack>().SetAttackType("Neutral");
                currentCombo++;
                PlayAttackAnimation("Neutral");
            }
            if (Input.GetButton("SpecialAttack1"))
            {
                AttackBox.SetActive(true);
                attackLength = 0.3f; // 1f;
                attackStartTime = Time.time;
                AttackBox.GetComponent<PlayerAttack>().SetAttackType("Special1");
                currentCombo++;
                PlayAttackAnimation("Special");
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

    public void HandleJump()
    {
        Debug.Log("handlejump");
        //make it jump
        if (!IsJumping)
        {
            Debug.Log("not jumping");
            IsJumping = true;
            jumpAnimation.visible = true;
            jumpAnimation.Play();

            IsMoving = false;
            isAttackPlaying = false;
            isRunPlaying = false;
            attackAnimation.Stop();
            attackAnimation.visible = false;
            liftAnimation.Stop();
            liftAnimation.visible = false;
            runAnimation.Stop();
            runAnimation.visible = false;
            idleAnimation.Stop();
            idleAnimation.visible = false;
        }
        else
        {
            Debug.Log("jumping");
            isJumping = true;
            jumpAnimation.visible = true;

            IsMoving = false;
            isAttackPlaying = false;
            isRunPlaying = false;
            attackAnimation.Stop();
            attackAnimation.visible = false;
            liftAnimation.Stop();
            liftAnimation.visible = false;
            runAnimation.Stop();
            runAnimation.visible = false;
            idleAnimation.Stop();
            idleAnimation.visible = false;
        }
    }

    private CharacterController controller;

    private void HandleAnimations()
    {
        if (controller.isGrounded)
        {
            jumpAnimation.Stop();
            jumpAnimation.visible = false;
            isJumping = false;
        }

        //make it run
        if (IsMoving && !IsJumping && !isAttackPlaying && !isRunPlaying) //is moving and not attacking but run animation is not playing
        {
            isRunPlaying = true;
            runAnimation.visible = true;
            runAnimation.Play();
            idleAnimation.Stop();
            idleAnimation.visible = false;
            jumpAnimation.Stop();
            jumpAnimation.visible = false;
        }
        //make it idle
        if (!IsMoving && !isAttackPlaying && !IsJumping) //is not moving and not attacking
        {
            isRunPlaying = false;
            runAnimation.Stop();
            runAnimation.visible = false;
            idleAnimation.visible = true;
            idleAnimation.Play();
            jumpAnimation.Stop();
            jumpAnimation.visible = false;
        }
    }

    private void PlayAttackAnimation(string attackType)
    {
        isAttackPlaying = true;

        if (attackType == "Neutral" || attackType == "Smash")
        {
            attackAnimation.visible = true;
            attackAnimation.PlayOnce("Attack");
        }
        else if (attackType == "Lift" || attackType == "Special")
        {
            liftAnimation.visible = true;
            liftAnimation.PlayOnce("Lift");
        }
        isRunPlaying = false;
        runAnimation.Stop();
        runAnimation.visible = false;
        idleAnimation.Stop();
        idleAnimation.visible = false;
        //jumpAnimation.Stop(); //should be pause
        if (!controller.isGrounded)
            jumpAnimation.Pauze();
        else
            jumpAnimation.Stop();
        jumpAnimation.visible = false;
    }

    public void OnAnimationStart(OTObject owner)
    {
        if (owner == attackAnimation || owner == liftAnimation)
        {
            GameObject.FindGameObjectWithTag("Audio").SendMessage("PlaySoundEffect", "Whoosh");
        }
    }

    public void OnAnimationFinish(OTObject owner)
    {
        if ((owner == attackAnimation || owner == liftAnimation) && isAttackPlaying)
        {
            isAttackPlaying = false;
            isRunPlaying = false;
            attackAnimation.visible = false;
            liftAnimation.visible = false;
            runAnimation.visible = false;

            if (!controller.isGrounded)
            {
                jumpAnimation.Resume();
                jumpAnimation.visible = true;

                idleAnimation.visible = false;
            }
            else
            {
                idleAnimation.visible = true;
                idleAnimation.Play();
            }
        }
    }

    public void InterruptCombo()
    {
        currentCombo = ComboLength;
        comboEndTime = Time.time - (Time.deltaTime * 70);
    }

    private Vector3 impact;

    public void AddImpact(Vector3 force, float strength)
    {
        impact += force * strength;
    }
	
	// Update is called once per frame
    void Update()
    {
        //apply impact force
        if (impact.magnitude > 0.2) this.gameObject.GetComponent<CharacterController>().Move(impact * Time.deltaTime);
        //consumes the impact energy each cycle
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }
}