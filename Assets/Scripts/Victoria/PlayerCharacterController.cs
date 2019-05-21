using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : BasicMovement
{

    public CharacterController controller;
    public Vector3 moveDirection;
    public float jumpPadVelocity;
    private bool gravityIsReversed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        size  = controller.bounds.size;
        bottomCenter = controller.bounds.center;
    }

    void Update()
    {
        SetGrounded();
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal") * movementVelocity, moveDirection.y, 0f);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            moveDirection.y = jumpVelocity;
        }
        if (!isGrounded)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }

        controller.Move(moveDirection * Time.deltaTime);
    }

    void OnTriggerEnter(Collider WhatHitMe)
    {
        if (WhatHitMe.gameObject.tag == "GravityReverser")
        {
            gravityIsReversed = !gravityIsReversed;
            moveDirection.y = gravity / 3 * 2;
            gravity = -gravity;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }
}
