using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturPlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;

    public float gravity;

    public bool rawInput;

    public LayerMask groundLayers;
    public PlayerInfo playerInfo;

    private Rigidbody rigidBody;
    private PlayerInput playerInput;
    private CharacterController controller;
    private Vector3 moveDirection;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
        playerInput = new PlayerInput();
        playerInput.SetRaw(rawInput);
        playerInfo = new PlayerInfo();
        playerInfo.Initialize(controller.bounds.size);
    }

    private void Update()
    {
        playerInfo.Update(controller.bounds.center, moveDirection, groundLayers);
        playerInput.SetInput();
        moveDirection = new Vector3(playerInput.horizontalInput * moveSpeed, moveDirection.y, moveDirection.z);
        Move();
        print(playerInfo.isFalling);
    }


    private void Move ()
    {
        if (playerInput.jumpInput && playerInfo.isGrounded)
        {
            moveDirection.y = jumpForce;
        }

        if (!playerInfo.isGrounded)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }

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
        public bool isFalling;

        public Vector3 size;
        public Vector3 bottom, top, left, right;

        public void Initialize(Vector3 size)
        {
            this.size = size;
        }

        public void Update(Vector3 center, Vector3 moveDirection, LayerMask groundLayers)
        {
            print(moveDirection);
            bottom = center - new Vector3(0, size.y * 0.5f, 0f);
            top = center + new Vector3(0, size.y * 0.5f, 0f);
            right = center + new Vector3(size.x * 0.5f, 0f, 0f);
            left = center - new Vector3(size.x * 0.5f, 0f, 0f);

            if (Physics.OverlapBox(bottom, size / 2, Quaternion.identity, groundLayers).Length > 0)
            {
                isGrounded = true;
                isFalling = false;
            }
            else
            {
                isGrounded = false;
            } 

            if (moveDirection.y < 0)
            {
                isFalling = true;
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


    /*
    
    private void OnDrawGizmos()
    {
        controller = GetComponent<CharacterController>();
        playerInfo = new PlayerInfo(controller.bounds.center, controller.bounds.size);
        Gizmos.color = Color.white;
        Gizmos.DrawCube(playerInfo.right, playerInfo.size);
    }
    
    */
}
