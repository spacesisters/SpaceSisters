using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ArturBasePlayerController : MonoBehaviour
{

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


    protected ConstantForce constForce;
    protected Rigidbody rBody;
    protected BoxCollider boxCollider;
    protected PlayerInput playerInput;
    protected bool isGrounded;
    protected bool doForcefield;

    [SerializeField]
    private LayerMask groundLayers;
    private Vector3 size;
    private float currentMaxSpeed;
    private float currentMaxAcceleration;
    private float dashTime;
    private bool doDash;
    private bool doJump;

    protected void Initialize()
    {
        playerInput = new PlayerInput();
        rBody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        constForce = GetComponent<ConstantForce>();

        size = new Vector3(boxCollider.size.x * 0.975f, boxCollider.size.y * 0.025f, boxCollider.size.z);
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
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (playerInput.horizontalLeft < 0)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
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


    }

    protected void FixedUpdate()
    {
        ArturHelper.AccelerateTo(rBody, new Vector3(playerInput.horizontalLeft * currentMaxSpeed, 
                                                    rBody.velocity.y, rBody.velocity.z),
                                 currentMaxAcceleration);

        if (doJump)
        {
            rBody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange);
            doJump = false;
        }

        if (doDash) 
        {      
            rBody.AddForce(transform.right * dashForce, ForceMode.VelocityChange);
            dashTime = dashCooldown;
            doDash = false;
        }
    }


    public struct PlayerInput
    {

        public float horizontalLeft;
        public float verticalLeft;
        public bool jumpButton;
        public bool dashButton;
        public bool forcefieldButton;
        public bool specialButton;

        public void Update()
        {
            horizontalLeft = Input.GetAxis("LeftJoystickHorizontal");
            verticalLeft = Input.GetAxis("LeftJoystickVertical");
            jumpButton = Input.GetButton("XButton");
            dashButton = Input.GetButton("AButton");
            forcefieldButton = Input.GetButton("BButton");
            specialButton = Input.GetButton("YButton");
        }
    }



    private void UpdateGrounded()
    {
        Vector3 bottomCenter = new Vector3(rBody.position.x, rBody.position.y - boxCollider.size.y / 2, rBody.position.z);

        if (Physics.OverlapBox(bottomCenter, size / 2, Quaternion.identity, groundLayers).Length > 0)
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
    }

}

