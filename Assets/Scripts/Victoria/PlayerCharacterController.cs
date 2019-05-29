using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : BasicMovement
{

    public CharacterController controller;
    public Vector3 moveDirection;
    public float jumpPadVelocity;
    private Collider collider;

    void Start()
    {
        collider = GetComponent<Collider>();
        controller = GetComponent<CharacterController>();

    }

    void Update()
    {
        size = collider.bounds.size * 0.75f;
        bottomCenter = collider.bounds.center - new Vector3(0, 0.6f, 0);
        topCenter = collider.bounds.center + new Vector3(0, collider.bounds.size.y *0.75f - 0.6f, 0);
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

    public void changeGravity()
    {
        gravityIsReversed = !gravityIsReversed;
        gravity = -gravity;
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

    private void OnDrawGizmos()
    {
        collider = GetComponent<Collider>();
        size = collider.bounds.size * 0.75f;
        bottomCenter = collider.bounds.center + new Vector3(0, collider.bounds.size.y * 0.75f, 0 );
        //Debug.Log(bottomCenter);
        
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(bottomCenter - new Vector3(0, 0.6f, 0), size);
    }
}
