using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public AudioClip pickupSound;

    virtual public void Effects(ArturBasePlayerController player){}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1" | other.gameObject.tag == "Player2"){
            ArturBasePlayerController player = other.GetComponent<ArturBasePlayerController>();
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            Effects(player);
        }
    }
}
