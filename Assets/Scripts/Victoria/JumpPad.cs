using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    // Just drag the script to object which become to gravity reverser then and give the character the tag "agent"
    public float jumpPadVelocity;

    void OnTriggerEnter(Collider WhatHitMe)
    {
        if(WhatHitMe.CompareTag("Agent"))
        {
            PlayerCharacterController agent = WhatHitMe.gameObject.GetComponent<PlayerCharacterController>();
            agent.moveDirection = new Vector3(Input.GetAxisRaw("Horizontal") * agent.movementVelocity, agent.moveDirection.y, 0f);
            agent.moveDirection.y = jumpPadVelocity;
            agent.controller.Move(agent.moveDirection * Time.deltaTime);
        }
        
    }
}
