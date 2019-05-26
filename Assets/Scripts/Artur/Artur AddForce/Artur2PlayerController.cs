using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artur2PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float runDrag;
    public float stopDrag;
    public float airDrag;
    public float airDamp;
    public float maximumSpeed;
    public LayerMask groundLayers;
    public int gravityReverse;

    private float airDampMultiplier;
    private Rigidbody rBody;
    private PlayerInput playerInput;
    private PlayerInfo playerInfo;
    private BoxCollider boxCollider;
    private Vector3 gravityVector;

    void Start()
    {
        gravityReverse = 1;
        gravityVector = new Vector3(0, Artur2SceneManager.gravity, 0);
        boxCollider = GetComponent<BoxCollider>();

        playerInfo = new PlayerInfo();
        playerInfo.Initialize(boxCollider.bounds.size);
        playerInput = new PlayerInput();
        rBody = GetComponent<Rigidbody>();     
    }

    private void Update()
    {
        playerInfo.Update(transform.position, groundLayers, gravityReverse);
        playerInput.Update();

        if (playerInput.horizontalLeft != 0 && playerInfo.isGrounded)
            rBody.drag = runDrag;
        else if(playerInput.horizontalLeft == 0 && playerInfo.isGrounded)
            rBody.drag = stopDrag;
        else if(!playerInfo.isGrounded)
        {
            rBody.drag = airDrag;
            airDampMultiplier = airDamp;
        }

        if (playerInfo.isGrounded)
        {
            airDampMultiplier = 1;
        }      
    }

    void FixedUpdate()
    {       
        rBody.AddForce(new Vector3(playerInput.horizontalLeft, 0f, 0f) * movementSpeed * airDampMultiplier);
        rBody.AddForce(gravityVector);


        float speed = Vector3.Magnitude(rBody.velocity);

        if (speed > maximumSpeed)
        {

            float brakeSpeed = speed - maximumSpeed;  

            Vector3 normalisedVelocity = rBody.velocity.normalized * maximumSpeed;
            rBody.velocity = normalisedVelocity;

            
        }


    }

    public void ReverseGravity()
    {
        gravityReverse *= -1;
        gravityVector *= -1;
    }


    public struct PlayerInfo
    {
        public bool isGrounded;

        public Vector3 size;
        public Vector3 bottom, top, left, right;

        public void Initialize(Vector3 size)
        {
            this.size = size;
        }

        public void Update(Vector3 center, LayerMask groundLayers, int gravityReversed)
        {
            bottom = center - new Vector3(0, size.y * 0.5f, 0f);
            top = center + new Vector3(0, size.y * 0.5f, 0f);
            right = center + new Vector3(size.x * 0.5f, 0f, 0f);
            left = center - new Vector3(size.x * 0.5f, 0f, 0f);

            Vector3 groundedChecker = Vector3.zero;
            if (gravityReversed == 1)
            {
                groundedChecker = bottom;
            }
            else if (gravityReversed == -1)
            {
                groundedChecker = top;
            }


            if (Physics.OverlapBox(groundedChecker, size / 2, Quaternion.identity, groundLayers).Length > 0)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }

        public bool isColldingWith(string side, LayerMask layer)
        {
            Vector3 collisionSide = Vector3.zero;
            if (side == "top")
                collisionSide = top;
            else if (side == "bottom")
                collisionSide = bottom;
            else if (side == "left")
                collisionSide = left;
            else if (side == "right")
                collisionSide = right;
            if (Physics.OverlapBox(collisionSide, size / 2, Quaternion.identity, layer).Length > 0)
                return true;
            return false;
        }
    }

    public struct PlayerInput
    {
        public float horizontalLeft;
        public float verticalLeft;
        public float horizontalRight;
        public float verticalRight;
        public bool jump;

        public void Update()
        {
            horizontalLeft = Input.GetAxis("LeftJoystickHorizontal");
            verticalLeft = Input.GetAxis("LeftJoystickVertical");

            horizontalRight = Input.GetAxis("RightJoystickHorizontal");
            verticalRight = Input.GetAxis("RightJoystickVertical");

            jump = Input.GetButtonDown("XButton");
        }

        public void PrintInput()
        {
            print("H.L.: " + horizontalLeft);
            print("V.L.: " + verticalLeft);

            print("H.R.: " + horizontalRight);
            print("V.R.: " + verticalRight);
        }
    }

    public void AddForce(Vector3 force)
    {
        rBody.AddForce(force);
    }

    public Rigidbody GetRigidbody()
    {
        return rBody;
    }

    /*
    private void OnDrawGizmos()
    {
        boxCollider = GetComponent<BoxCollider>();
        playerInfo = new PlayerInfo();
        playerInfo.Initialize(boxCollider.size);
        playerInfo.Update(transform.position, groundLayers, gravityReverse);
        Gizmos.color = Color.white;
        Gizmos.DrawCube(playerInfo.bottom, playerInfo.size);
    }
    */
}
