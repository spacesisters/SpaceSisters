using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityReverser : MonoBehaviour
{
    private bool isColliding;

    private void Update()
    {
        isColliding = false;
    }
    // Just drag the script to object which become to gravity reverser then and give the character the tag "agent"
    private bool gravityIsReversed;

    void OnTriggerEnter(Collider WhatHitMe)
    {
        if (WhatHitMe.CompareTag("Agent"))
        {
            isColliding = true;
            PlayerCharacterController agent = WhatHitMe.gameObject.GetComponent<PlayerCharacterController>();
            gravityIsReversed = !gravityIsReversed;
            agent.moveDirection = new Vector3(Input.GetAxisRaw("Horizontal") * agent.movementVelocity, agent.moveDirection.y, 0f);
            agent.moveDirection.y = agent.gravity / 3 * 2;
            agent.gravity = -agent.gravity;
            agent.controller.Move(agent.moveDirection * Time.deltaTime);
            agent.controller.transform.eulerAngles = new Vector3(agent.controller.transform.eulerAngles.x + 180, agent.controller.transform.eulerAngles.y, agent.controller.transform.eulerAngles.z);
        }

    }
}
