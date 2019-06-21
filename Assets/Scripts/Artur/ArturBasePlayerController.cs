using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ArturBasePlayerController : MonoBehaviour
{
    public int playerNumber;
    public string controllerType;
    public float jumpForce;
    public float dashForce;
    public float maxMovementSpeedGrounded;
    public float maxAccelerationGrounded;
    public float maxMovementSpeedAir;
    public float maxAccelerationAir;
    public float dashCooldown;
    [SerializeField]
    public float energy;
    public float energyDrainPerSecond;
    public float shootCooldown;
    public float maxJumpAcceleration;


    // public float improvedAmmo; // between 0 and 1 // **
    public bool speedBonus;


    protected ConstantForce constForce;
    protected Rigidbody rBody;
    protected CapsuleCollider capsuleCollider;
    protected PlayerInput playerInput;
    protected bool isGrounded;
    protected bool doForcefield;
    protected InputManager controller;


    [SerializeField]
    private LayerMask groundLayers;
    private Vector3 size;
    private ArturGunScript gunScript;
    private float currentMaxSpeed;
    private float currentMaxAcceleration;
    private float dashTime;
    private float shootTimer;
    private bool doDash;
    private bool doJump;
    private bool doShoot;
    private float groundCheckRadius = 0.1f;


    protected void Initialize()
    {
        energy = 1f;
        speedBonus = false;


        gunScript = GetComponent<ArturGunScript>();
        controller = new InputManager(playerNumber, controllerType);

        playerInput = new PlayerInput();
        playerInput.SetInputManager(controller);
        rBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        constForce = GetComponent<ConstantForce>();

        //size = new Vector3(boxCollider.size.x * 0.975f, boxCollider.size.y * 0.025f, boxCollider.size.z);
        constForce.force = new Vector3(0, ArturSceneManager.gravity, 0);

    }

    protected void Update()
    {
        UpdateGrounded();
        playerInput.Update();
        // Setting max speed.
        if (isGrounded)
        {
            currentMaxSpeed = maxMovementSpeedGrounded;
            currentMaxAcceleration = maxAccelerationGrounded;
        }
        else
        {
            currentMaxSpeed = maxMovementSpeedAir;
            currentMaxAcceleration = maxAccelerationAir;
        }


        //Setting rotation
        if (playerInput.horizontalLeft > 0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        }
        else if (playerInput.horizontalLeft < 0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 180, transform.eulerAngles.z);
        }

        // Managing dash cooldown
        if (dashTime > 0 )
        {
            dashTime -= Time.deltaTime;
        }
        else if (dashTime < 0 )
        {
            dashTime = 0;
        }

        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
        else if (shootTimer < 0)
        {
            shootTimer = 0;
        }
    }


    protected void FixedUpdate()
    {
        ArturHelper.AccelerateTo(rBody, new Vector3(playerInput.horizontalLeft * currentMaxSpeed, 
                                                    rBody.velocity.y, rBody.velocity.z),
                                 currentMaxAcceleration);

        if (doJump)
        {
            
            //rBody.AddForce(new Vector3(0, jumpForce, 0) * transform.up.y, ForceMode.VelocityChange);
            ArturHelper.AccelerateTo(rBody, new Vector3(rBody.velocity.x, jumpForce, rBody.velocity.z),
                                 maxJumpAcceleration);
            doJump = false;
        }

        if (doDash) 
        {      
            rBody.AddForce(transform.right * dashForce, ForceMode.VelocityChange);
            dashTime = dashCooldown;
            doDash = false;
        }
        if (doShoot)
        {
            gunScript.Shoot();
            shootTimer = shootCooldown;
        }
    }


    public struct PlayerInput
    {
        public InputManager input;
        public float horizontalLeft;
        public float verticalLeft;
        public bool jumpButton;
        public bool dashButton;
        public bool forcefieldButton;
        public bool shootButton;

        public void SetInputManager(InputManager input)
        {
            this.input = input;
        }

        public void Update()
        {
            horizontalLeft = Input.GetAxis(input.left_horizontal);
            verticalLeft = Input.GetAxis(input.left_vertical);
            jumpButton = Input.GetButton(input.button1);
            dashButton = Input.GetButton(input.button0);
            forcefieldButton = Input.GetButton(input.button3);
            shootButton = Input.GetButton(input.button2);
        }
    }



    private void UpdateGrounded()
    {
        Vector3 bottomCenter = new Vector3(transform.position.x, transform.position.y - (capsuleCollider.height * 0.5f), rBody.position.z);
        
        if (Physics.OverlapSphere(bottomCenter, groundCheckRadius, groundLayers).Length > 0)
        {
            isGrounded = true;
        }
        else
            isGrounded = false;
          
    }


    protected void TranslateInput()
    {
        if (playerInput.dashButton && dashTime == 0)
        {
            doDash = true;
        }
        else
            doDash = false;

        if (playerInput.jumpButton && isGrounded)
        {
            doJump = true;
        }
        else
            doJump = false;

        if (playerInput.forcefieldButton && energy > 0)
        {
            doForcefield = true;
        }
        else
            doForcefield = false;

        if (playerInput.shootButton && shootTimer == 0)
        {
            doShoot = true;
        }
        else
            doShoot = false;
    }

    public PlayerInput GetPlayerInput()
    {
        return playerInput;
    }



    private void OnDrawGizmos()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        Gizmos.color = Color.cyan;
        Vector3 center = transform.position;
        center.y -= capsuleCollider.height * 0.5f;
        Gizmos.DrawSphere(center, 0.1f);
    }

    [System.Serializable]
    protected class Player
    {
        public int playerNum;
        public string controllertype;
    }
}

