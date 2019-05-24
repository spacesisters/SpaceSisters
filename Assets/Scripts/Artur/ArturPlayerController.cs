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


    private Rigidbody rigidBody;
    private PlayerInput playerInput;
    private GroundedChecker groundedChecker;
    private CharacterController controller;
    private Vector3 moveDirection;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
        playerInput = new PlayerInput();
        playerInput.SetRaw(rawInput);
        groundedChecker = new GroundedChecker();
        groundedChecker.Initialize(controller.bounds.size);
    }

    private void Update()
    {
        groundedChecker.Update(controller.bounds.center, groundLayers);
        playerInput.SetInput();

        Move();
    }


    private void Move ()
    {
        moveDirection = new Vector3(playerInput.horizontalInput * moveSpeed, moveDirection.y, moveDirection.z);
        if (playerInput.jumpInput && groundedChecker.isGrounded)
        {
            moveDirection.y = jumpForce;
        }

        if (!groundedChecker.isGrounded)
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

    public struct GroundedChecker
    {
        public Vector3 bottomCenter, size;
        public bool isGrounded;

        public void Initialize(Vector3 size)
        {
            this.size = size;
            this.size.x *= 0.95f;
            this.size.y *= 0.5f;
        }

        public void Update(Vector3 center, LayerMask groundLayers)
        {
            bottomCenter = center - new Vector3(0, size.y, 0f);
            if (Physics.OverlapBox(bottomCenter, size / 2, Quaternion.identity, groundLayers).Length > 0)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
    }
    




    /*
    private void OnDrawGizmos()
    {
        controller = GetComponent<CharacterController>();
        groundedChecker.Initialize(controller.bounds.size);
        groundedChecker.Update(controller.bounds.center, groundLayers);
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundedChecker.bottomCenter, groundedChecker.size);
    }
    */

}
