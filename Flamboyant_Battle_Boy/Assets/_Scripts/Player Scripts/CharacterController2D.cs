﻿using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour
{
    // Does this script currently respond to Input?
    public bool canControl = true;

    public float lockZDepth = 0f;

    // The character will spawn at spawnPoint's position when needed.  This could be changed via a script at runtime to implement, e.g. waypoints/savepoints.
    public Transform spawnPoint;

    public PlatformerControllerMovement movement;

    public PlatformerControllerJumping jump;

    private CharacterController controller;

    // Moving platform support.
    private Transform activePlatform;
    private Vector3 activeLocalPlatformPoint;
    private Vector3 activeGlobalPlatformPoint;
    private Vector3 lastPlatformVelocity;

    // The movement speed modifier (set externally)
    private float speedModifier = 1.0f;
    public float SpeedModifier { get { return speedModifier; } set { speedModifier = value; } }

    // Is Double-Jump Unlocked?
    private bool doubleJumpEnabled;
    public bool DoubleJumpEnabled { get { return doubleJumpEnabled; } set { doubleJumpEnabled = value; } }

    // Is Triple-Jump Unlocked?
    private bool tripleJumpEnabled;
    public bool TripleJumpEnabled { get { return tripleJumpEnabled; } set { tripleJumpEnabled = value; } }

    // This is used to keep track of special effects in UpdateEffects ();
    private bool areEmittersOn = false;

    void Awake()
    {
        GameEventManager.LevelStart += LevelStart;
        GameEventManager.LevelComplete += LevelComplete;

        movement.direction = transform.TransformDirection(Vector3.forward);
        controller = GetComponent<CharacterController>();// GetComponent(CharacterController);

        if(this.gameObject.tag == "Player")
            Spawn();

        // FOR TESTING ONLY
        DoubleJumpEnabled = true;
        TripleJumpEnabled = true;
        // END TESTING
    }

    // Called Automatically anytime level starts - set default variables here
    private void LevelStart()
    {
        canControl = true;
    }

    // Called automatically anytime level finishes - set win/lose conditions here
    private void LevelComplete()
    {
        canControl = false;
    }

    void Spawn()
    {
        // reset the character's speed
        movement.verticalSpeed = 0.0f;
        movement.speed = 0.0f;
        if (this.gameObject.tag == "Enemy")
            movement.gravity /= 3;

        // reset the character's position to the spawnPoint
        transform.position = spawnPoint.position;
    }

    void OnDeath()
    {
        Spawn();
    }

    void UpdateSmoothedMovementDirection()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (this.gameObject.tag != "Player")
            canControl = false;

        if (!canControl)
            h = 0.0f;

        movement.isMoving = Mathf.Abs(h) > 0.1f;

        if (movement.isMoving)
            movement.direction = new Vector3(h, 0, 0);

        // Grounded controls
        if (controller.isGrounded)
        {
            // Smooth the speed based on the current target direction
            float curSmooth = movement.speedSmoothing * Time.deltaTime;

            // Choose target speed
            float targetSpeed = Mathf.Min(Mathf.Abs(h), 1.0f);

            // Pick speed modifier
            if (Input.GetButton("Fire2") && canControl)
                targetSpeed *= movement.runSpeed;
            else
                targetSpeed *= movement.walkSpeed;

            movement.speed = Mathf.Lerp(movement.speed, targetSpeed * SpeedModifier, curSmooth);

            movement.hangTime = 0.0f;
        }
        else
        {
            // In air controls
            movement.hangTime += Time.deltaTime;
            if (movement.isMoving)
            {
                if (this.gameObject.tag == "Player")
                {
                    Vector3 moveDirection = Vector3.zero;
                    moveDirection.z = (float)Input.GetAxis("Horizontal") * movement.walkSpeed;
                    if (moveDirection.z < 0) moveDirection.z *= -1;
                    moveDirection = this.transform.TransformDirection(moveDirection);
                    movement.inAirVelocity = moveDirection;
                }
                else
                    movement.inAirVelocity = new Vector3(Mathf.Sign(h), 0, 0) * Time.deltaTime * movement.inAirControlAcceleration;
            }

            // In air controls
            //movement.hangTime += Time.deltaTime;
            //if (movement.isMoving)
            //    movement.inAirVelocity = new Vector3(Mathf.Sign(h), 0, 0) * Time.deltaTime * movement.inAirControlAcceleration * movement.walkSpeed * 2;
        }
    }

    void FixedUpdate()
    {
        // Make sure we are absolutely always in the 2D plane.
        transform.position = new Vector3(transform.position.x, transform.position.y, lockZDepth);
    }

    void ApplyJumping()
    {
        // Prevent jumping too fast after each other
        if (jump.lastTime + jump.repeatTime > Time.time)
            return;

        if (controller.isGrounded)
        {
            // Jump
            // - Only when pressing the button down
            // - With a timeout so you can press the button slightly before landing		
            if (jump.enabled && Time.time < jump.lastButtonTime + jump.timeout)
            {
                movement.verticalSpeed = CalculateJumpVerticalSpeed(jump.height);
                movement.inAirVelocity = lastPlatformVelocity;
                SendMessage("DidJump", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    void ApplyGravity()
    {
        // Apply gravity
        bool jumpButton = Input.GetButton("Jump");

        if (gameObject.tag != "Player")
            canControl = false;

        if (!canControl)
            jumpButton = false;

        // When we reach the apex of the jump we send out a message
        if (jump.jumping && !jump.reachedApex && movement.verticalSpeed <= 0.0f)
        {
            jump.reachedApex = true;
            SendMessage("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
        }

        // if we are jumping and we press jump button, we do a double jump or
        // if we are falling, we can do a double jump to
        if (DoubleJumpEnabled &&
            (IsJumping() && Input.GetButtonUp("Jump") && !IsDoubleJumping()) ||
            (!controller.isGrounded && !IsJumping() && !IsDoubleJumping() && !IsTripleJumping() && movement.verticalSpeed < -12.0f))
        {
            jump.canDoubleJump = true;
        }

        // if we are jumping and we press jump button, we do a double jump or
        // if we are falling, we can do a double jump to
        if ((DoubleJumpEnabled && TripleJumpEnabled) &&
            (IsJumping() && Input.GetButtonUp("Jump") && IsDoubleJumping() && !IsTripleJumping()) ||
            (!controller.isGrounded && IsJumping() && IsDoubleJumping() && !IsTripleJumping() && movement.verticalSpeed < -12.0f))
        {
            jump.canTripleJump = true;
        }

        // if we can do a double jump, and we press the jump button, we do a double jump
        if (canDoubleJump() && Input.GetButtonDown("Jump") && !IsTouchingCeiling())
        {
            jump.doubleJumping = true;
            movement.verticalSpeed = CalculateJumpVerticalSpeed(jump.doubleJumpHeight);
            jump.canDoubleJump = false;

        }

        // if we can do a triple jump, and we press the jump button, we do a triple jump
        if (canTripleJump() && IsDoubleJumping() && Input.GetButtonDown("Jump") && !IsTouchingCeiling())
        {
            jump.tripleJumping = true;
            movement.verticalSpeed = CalculateJumpVerticalSpeed(jump.tripleJumpHeight);
            jump.canTripleJump = false;
        }

        // * When jumping up we don't apply gravity for some time when the user is holding the jump button
        //   This gives more control over jump height by pressing the button longer
        bool extraPowerJump = IsJumping() && !IsDoubleJumping() && !IsTripleJumping() && movement.verticalSpeed > 0.0f &&
            jumpButton && transform.position.y < jump.lastStartHeight + jump.extraHeight && !IsTouchingCeiling();

        if (extraPowerJump)
            return;
        else if (controller.isGrounded)
        {
            movement.verticalSpeed = -movement.gravity * Time.deltaTime;
            jump.canDoubleJump = false;
            jump.canTripleJump = false;
        }
        else
            movement.verticalSpeed -= movement.gravity * Time.deltaTime;

        // Make sure we don't fall any faster than maxFallSpeed.  This gives our character a terminal velocity.
        movement.verticalSpeed = Mathf.Max(movement.verticalSpeed, -movement.maxFallSpeed);
    }

    float CalculateJumpVerticalSpeed(float targetJumpHeight)
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * targetJumpHeight * movement.gravity);
    }

    void DidJump()
    {
        jump.jumping = true;
        jump.reachedApex = false;
        jump.lastTime = Time.time;
        jump.lastStartHeight = transform.position.y;
        jump.lastButtonTime = -10;
    }

    void UpdateEffects()
    {
        bool wereEmittersOn = areEmittersOn;
        areEmittersOn = jump.jumping && movement.verticalSpeed > 0.0f;

        // By comparing the previous value of areEmittersOn to the new one, we will only update the particle emitters when needed
        if (wereEmittersOn != areEmittersOn)
        {
            foreach (ParticleEmitter emitter in GetComponentsInChildren<ParticleEmitter>())
            {
                emitter.emit = areEmittersOn;
            }
        }
    }

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetButtonDown("Jump") && canControl && gameObject.tag == "Player")
        {
            jump.lastButtonTime = Time.time;
        }

        UpdateSmoothedMovementDirection();

        // Apply gravity
        // - extra power jump modifies gravity
        ApplyGravity();

        // Apply jumping logic
        ApplyJumping();

        // Moving platform support
        if (activePlatform != null)
        {
            Vector3 newGlobalPlatformPoint = activePlatform.TransformPoint(activeLocalPlatformPoint);
            Vector3 moveDistance = (newGlobalPlatformPoint - activeGlobalPlatformPoint);
            transform.position = transform.position + moveDistance;
            lastPlatformVelocity = (newGlobalPlatformPoint - activeGlobalPlatformPoint) / Time.deltaTime;
        }
        else
        {
            lastPlatformVelocity = Vector3.zero;
        }

        activePlatform = null;

        // Save lastPosition for velocity calculation.
        Vector3 lastPosition = transform.position;

        // Calculate actual motion
        Vector3 currentMovementOffset = movement.direction * speedModifier * movement.speed + new Vector3(0, movement.verticalSpeed, 0) + movement.inAirVelocity;

        // We always want the movement to be framerate independent.  Multiplying by Time.deltaTime does this.
        currentMovementOffset *= Time.deltaTime;

        // Move our character!
        movement.collisionFlags = controller.Move(currentMovementOffset);

        // Calculate the velocity based on the current and previous position.  
        // This means our velocity will only be the amount the character actually moved as a result of collisions.
        movement.velocity = (transform.position - lastPosition) / Time.deltaTime;

        // Moving platforms support
        if (activePlatform != null)
        {
            activeGlobalPlatformPoint = transform.position;
            activeLocalPlatformPoint = activePlatform.InverseTransformPoint(transform.position);
        }

        // Set rotation to the move direction	
        if (movement.direction.sqrMagnitude > 0.01f)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.direction), Time.deltaTime * movement.rotationSmoothing);

        // We are in jump mode but just became grounded
        if (controller.isGrounded)
        {
            movement.inAirVelocity = Vector3.zero;
            if (jump.jumping)
            {
                jump.jumping = false;
                jump.doubleJumping = false;
                jump.tripleJumping = false;
                jump.canDoubleJump = false;
                jump.canTripleJump = false;
                SendMessage("DidLand", SendMessageOptions.DontRequireReceiver);

                Vector3 jumpMoveDirection = movement.direction * movement.speed + movement.inAirVelocity;
                if (jumpMoveDirection.sqrMagnitude > 0.01f)
                    movement.direction = jumpMoveDirection.normalized;
            }
        }

        // Update special effects like rocket pack particle effects
        UpdateEffects();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.moveDirection.y > 0.01f)
            return;

        // Make sure we are really standing on a straight platform
        // Not on the underside of one and not falling down from it either!
        if (hit.moveDirection.y < -0.9f && hit.normal.y > 0.9f)
        {
            activePlatform = hit.collider.transform;
        }
    }

    #region Helper Methods

    // Various helper functions below:
    float GetSpeed()
    {
        return movement.speed;
    }

    Vector3 GetVelocity()
    {
        return movement.velocity;
    }

    bool IsMoving()
    {
        return movement.isMoving;
    }

    bool IsJumping()
    {
        return jump.jumping;
    }

    bool IsDoubleJumping()
    {
        return jump.doubleJumping;
    }

    bool IsTripleJumping()
    {
        return jump.tripleJumping;
    }

    bool canDoubleJump()
    {
        return jump.canDoubleJump;
    }

    bool canTripleJump()
    {
        return jump.canTripleJump;
    }

    bool IsTouchingCeiling()
    {
        return (movement.collisionFlags & CollisionFlags.CollidedAbove) != 0;
    }

    public Vector3 GetDirection()
    {
        return movement.direction;
    }

    float GetHangTime()
    {
        return movement.hangTime;
    }

    void Reset()
    {
        gameObject.tag = "Player";
    }

    void SetControllable(bool controllable)
    {
        canControl = controllable;
    }

    #endregion
}

[System.Serializable]
public class PlatformerControllerMovement
{
    // The speed when walking 
    public float walkSpeed = 3.0f;
    // when pressing "Fire1" button (control) we start running
    public float runSpeed = 10.0f;

    public float inAirControlAcceleration = 2.0f;

    // The gravity for the character
    public float gravity = 40.0f;
    public float maxFallSpeed = 10.0f;

    // How fast does the character change speeds?  Higher is faster.
    public float speedSmoothing = 10.0f;

    // This controls how fast the graphics of the character "turn around" when the player turns around using the controls.
    public float rotationSmoothing = 10.0f;

    // The current move direction in x-y.  This will always be (1,0,0) or (-1,0,0)
    // The next line, @System.NonSerialized , tells Unity to not serialize the variable or show it in the inspector view.  Very handy for organization!
    //@System.NonSerialized
    [System.NonSerialized]
    public Vector3 direction = Vector3.zero;

    // The current vertical speed
    [System.NonSerialized]
    public float verticalSpeed = 0.0f;

    // The current movement speed.  This gets smoothed by speedSmoothing.
    [System.NonSerialized]
    public float speed = 0.0f;

    // Is the user pressing the left or right movement keys?
    [System.NonSerialized]
    public bool isMoving = false;

    // The last collision flags returned from controller.Move
    [System.NonSerialized]
    public CollisionFlags collisionFlags;

    // We will keep track of an approximation of the character's current velocity, so that we return it from GetVelocity () for our camera to use for prediction.
    [System.NonSerialized]
    public Vector3 velocity;

    // This keeps track of our current velocity while we're not grounded?
    [System.NonSerialized]
    public Vector3 inAirVelocity = Vector3.zero;

    // This will keep track of how long we have we been in the air (not grounded)
    [System.NonSerialized]
    public float hangTime = 0.0f;
}

// We will contain all the jumping related variables in one helper class for clarity.
[System.Serializable]
public class PlatformerControllerJumping
{
    // Can the character jump?
    public bool enabled = true;

    // How high do we jump when pressing jump and letting go immediately
    public float height = 1.0f;
    // We add extraHeight units (meters) on top when holding the button down longer while jumping
    public float extraHeight = 4.1f;

    public float doubleJumpHeight = 2.5f;
    public float tripleJumpHeight = 2.5f;

    // This prevents inordinarily too quick jumping
    // The next line, @System.NonSerialized , tells Unity to not serialize the variable or show it in the inspector view.  Very handy for organization!
    //@System.NonSerialized
    [System.NonSerialized]
    public float repeatTime = 0.05f;

    [System.NonSerialized]
    public float timeout = 0.15f;

    // Are we jumping? (Initiated with jump button and not grounded yet)
    [System.NonSerialized]
    public bool jumping = false;

    // Are we double jumping? ( Initiated when jumping or falling after pressing the jump button )
    [System.NonSerialized]
    public bool doubleJumping = false;

    // Are we triple jumping? ( Initiated when jumping or falling after pressing the jump button after a double jump)
    [System.NonSerialized]
    public bool tripleJumping = false;

    // Can we make a double jump ( we can't make two double jump or more at the same jump )
    [System.NonSerialized]
    public bool canDoubleJump = false;

    // Can we make a triple jump?
    [System.NonSerialized]
    public bool canTripleJump = false;

    [System.NonSerialized]
    public bool reachedApex = false;

    // Last time the jump button was clicked down
    [System.NonSerialized]
    public float lastButtonTime = -10.0f;

    // Last time we performed a jump
    [System.NonSerialized]
    public float lastTime = -1.0f;

    // the height we jumped from (Used to determine for how long to apply extra jump power after jumping.)
    [System.NonSerialized]
    public float lastStartHeight = 0.0f;
}