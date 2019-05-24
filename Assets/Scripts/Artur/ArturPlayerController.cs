using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturPlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;

    public float gravity;
    public float maxMovementSpeed;
    public bool rawInput;

    public LayerMask groundLayers;
    public PlayerInfo playerInfo;

    private Rigidbody rigidBody;
    private PlayerInput playerInput;
    private CharacterController controller;
    private Vector3 moveDirection;
    private int gravityReversed;

    private void Start()
    {
        gravityReversed = 1;
        controller = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
        playerInput = new PlayerInput();
        playerInput.SetRaw(rawInput);
        playerInfo = new PlayerInfo();
        playerInfo.Initialize(controller.bounds.size);
    }

    private void Update()
    {
        
        playerInfo.Update(controller.bounds.center, groundLayers, gravityReversed);
        playerInput.SetInput();
        Move();

    }


    private void Move ()
    {
        moveDirection = new Vector3(playerInput.horizontalInput * moveSpeed, moveDirection.y, moveDirection.z);

        if (playerInput.jumpInput && playerInfo.isGrounded)
        {
            moveDirection.y = jumpForce * gravityReversed;
        }
        if (playerInfo.isColldingWith("top", groundLayers) && gravityReversed == 1)
        {
            moveDirection.y = 0;
        }
        else if(playerInfo.isColldingWith("bottom", groundLayers) && gravityReversed == -1)
        {
            moveDirection.y = 0;
        }

        if (!playerInfo.isGrounded)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }
        else if ((playerInfo.isGrounded && !playerInput.jumpInput))
        {
            moveDirection.y = 0;
        }

        moveDirection.x = Mathf.Clamp(moveDirection.x, -1 * maxMovementSpeed, maxMovementSpeed);
        moveDirection.y = Mathf.Clamp(moveDirection.y, -1 * maxMovementSpeed, maxMovementSpeed);

        controller.Move(moveDirection * Time.deltaTime);
    }


    public struct PlayerInput
    {
        public float horizontalInput;
        public float verticalInput;
        public bool jumpInput;
        public bool raw;

        public void SetRaw(bool raw)
        {
            this.raw = raw;
        }

        public void SetInput()
        {
            if (!raw)
            {
                horizontalInput = Input.GetAxis("Horizontal");
                verticalInput = Input.GetAxis("Vertical");
            }
            else
            {
                horizontalInput = Input.GetAxisRaw("Horizontal");
                verticalInput = Input.GetAxisRaw("Vertical");
            }
            
            jumpInput = Input.GetButtonDown("Jump");
        }   
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reversable"))
        {
            StartCoroutine(ReverseGravity(0.25f));
        }
    }


    /*
    
    private void OnDrawGizmos()
    {
        controller = GetComponent<CharacterController>();
        playerInfo = new PlayerInfo(controller.bounds.center, controller.bounds.size);
        Gizmos.color = Color.white;
        Gizmos.DrawCube(playerInfo.right, playerInfo.size);
    }
    
    */

    IEnumerator ReverseGravity(float time)
    {
        yield return new WaitForSeconds(time);
        print("Reversing gravity");
        gravityReversed *= -1;
        gravity *= -1;
    }

}
