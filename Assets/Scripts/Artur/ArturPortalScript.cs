using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturPortalScript : MonoBehaviour
{
    public Transform otherPortalSpawnLocation;
    public bool canTraverse;
    public bool reverseGravityOnEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            if (canTraverse)
            {
                other.gameObject.transform.position = otherPortalSpawnLocation.position;
                if (reverseGravityOnEnter)
                {
                    other.GetComponent<ArturBasePlayerController>().ReverseGravity();
                }
            }
        }
    }

}
