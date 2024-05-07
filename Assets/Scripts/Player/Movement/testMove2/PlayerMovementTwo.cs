using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerMovementTwo : MonoBehaviour, IDataPersistence
{
    [Header("Movement")]
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float dashSpeed;
    public float dashSpeedChangeFactor;
    public float maxYSpeed;
    public float groundDrag;
    public int groundContact = 0;
    public float wallrunningSpeed;
    public float swingSpeed;
    public float movementUpgrades;
    public float speedBonus;
    
    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float jumpBonus;
    public float jumpUpgrades;
    bool readyToJump;

    [Header("Crouching")] 
    public float crouchSpeed;
    public float crouchYScale;
    public float startYScale;
    
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;
    
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Slope Handling")] 
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;
    public bool isOnSlope;

    [Header("Upgrades")]
    public int grappleUpgrades;
    public int dashUpgrades;
    public int bombUpgrades;

    [Header("Misc")]
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    public ParticleSystem animeLines;
    
    Vector3 moveDirection;

    Rigidbody rb;

    private GrappleGun _grappleGun;
    private Dashing _dashing;
    private Shoot _shoot;
    
    public MovementState state;
    public enum MovementState
    {
        walking,
        swinging,
        sprinting,
        wallrunning,
        //crouching,
        dashing,
        air
    }

    public bool sprinting;
    public bool dashing;
    public bool wallrunning;
    public bool swinging;

    public List<string> coinsList = new List<string>();
    public int coins = 0;
    public bool blueUnlock = false;
    public bool pinkUnlock = false;
    public bool goldUnlock = false;
    
    private void Start()
    {
        _grappleGun = GetComponentInChildren<GrappleGun>();
        _dashing = GetComponent<Dashing>();
        _shoot = GetComponentInChildren<Shoot>();
        
        Time.timeScale = 1.0f;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        startYScale = transform.localScale.y;
    }

    private static PlayerMovementTwo playerInstance;
    void Awake()
    {
        DontDestroyOnLoad (this);
        if (playerInstance == null) {
            playerInstance = this;
        } else {
            if (this.gameObject.name == "CutscenePlayer")
            {
                DestroyObject(playerInstance);
                playerInstance = this;
            }
            else
            {
                DestroyObject(gameObject);
            }
        }
    }
    
    public void LoadData(GameData data)
    {

        this.coinsList = data.coinsList;
        this.coins = data.coins;

        _grappleGun = GetComponentInChildren<GrappleGun>();
        _dashing = GetComponent<Dashing>();
        _shoot = GetComponentInChildren<Shoot>();

        this.grappleUpgrades = data.grappleUpgrades;
        _grappleGun.Recalculate(this.grappleUpgrades);

        this.dashUpgrades = data.dashUpgrades;
        _dashing.Recalculate(this.dashUpgrades);

        this.bombUpgrades = data.bombUpgrades;
        _shoot.Recalculate(this.bombUpgrades);
    }

    public void SaveData(ref GameData data)
    {
        data.coinsList = this.coinsList;
        data.coins = this.coins;

        data.grappleUpgrades = this.grappleUpgrades;
        data.dashUpgrades = this.dashUpgrades;
        data.bombUpgrades = this.bombUpgrades;
    }

    private void Update()
    {
        
        // ground check
        if (groundContact > 0)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        MyInput();
        SpeedControl();
        StateHandler();

        // handle drag
        if (state == MovementState.walking || state == MovementState.sprinting)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        if (moveSpeed > 42) animeLines.Play();
        else animeLines.Stop();

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetButtonDown("Jump") && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        
        // //crouching
        // if (Input.GetKeyDown(crouchKey))
        // {
        //     transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
        //     rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        // }
        //
        // //uncrouching
        // if (Input.GetKeyUp(crouchKey))
        // {
        //     transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        // }
    }

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    private MovementState lastState;
    private bool keepMomentum;
    
    private void StateHandler()
    {
        if (swinging)
        {
            state = MovementState.swinging;
            desiredMoveSpeed = swingSpeed;
        }
        
        //sets player to wallrunning
        else if (wallrunning)
        {
            state = MovementState.wallrunning;
            desiredMoveSpeed = wallrunningSpeed;
        }
        
        //sets player to dashing
        else if (dashing)
        {
            state = MovementState.dashing;
            desiredMoveSpeed = dashSpeed;
            speedChangeFactor = dashSpeedChangeFactor;
        }
        //sets player to crouching
        //else if (Input.GetKey(crouchKey))
        //{
        //    state = MovementState.crouching;
        //    desiredMoveSpeed = crouchSpeed;
        //}
        
        //sets player to sprinting
        else if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
            sprinting = true;
        }
        
        //sets player to walking
        else if (grounded)
        {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
            sprinting = false;
        }
        
        //air control
        else
        {
            state = MovementState.air;
            
            if (desiredMoveSpeed < sprintSpeed)
                desiredMoveSpeed = walkSpeed;
            else 
                desiredMoveSpeed = sprintSpeed;
        }
        
        desiredMoveSpeed = desiredMoveSpeed * 1 + (speedBonus * movementUpgrades);
        
        bool desiredMoveSpeedHasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;
        if (lastState == MovementState.dashing) keepMomentum = true;

        if (desiredMoveSpeedHasChanged)
        {
            if (keepMomentum)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                moveSpeed = desiredMoveSpeed;
            }
        }
        
        lastDesiredMoveSpeed = desiredMoveSpeed;
        lastState = state;
        
    }

    private float speedChangeFactor;

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        // smoothly changes movement speed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        float boostFactor = speedChangeFactor;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);
            time += Time.deltaTime * boostFactor;
            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
        speedChangeFactor = 1f;
        keepMomentum = false;
    }
    private void MovePlayer()
    {
        if (state == MovementState.dashing) return;
        if (swinging) return;
        
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);
            
            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
        
        // on ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        
        //turn off gravity if on a slope
        if(!wallrunning) rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        
        // limiting speed if on slope
        if (OnSlope() && !exitingSlope && grounded)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }
        
        // limiting speed if on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if(flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
        
        //limiting y velocity
        if (maxYSpeed != 0 && rb.velocity.y > maxYSpeed)
            rb.velocity = new Vector3(rb.velocity.x, maxYSpeed, rb.velocity.z);
    }

    private void Jump()
    {
        exitingSlope = true;
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * (jumpForce * 1 + (jumpBonus + jumpUpgrades)), ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundContact++;
            Debug.Log("Increasing ground contact!)");
        }

        if (other.gameObject.tag == "Boss")
        {
            Debug.Log("Being hit!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundContact--;
        }
    }
    
    public void GrappleUpgrade()
    {
        grappleUpgrades++;
        _grappleGun.Recalculate(grappleUpgrades);
        Debug.Log("Upgraded Grapple!");
    }
    
    public void DashUpgrade()
    {
        dashUpgrades++;
        _dashing.Recalculate(dashUpgrades);
        Debug.Log("Upgraded Dash!");
    }
    
    public void BombUpgrade()
    {
        bombUpgrades++;
        _shoot.Recalculate(bombUpgrades);
        Debug.Log("Upgraded Bomb!");
    }
    
}