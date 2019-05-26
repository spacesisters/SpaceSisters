using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MariusGravityReverseTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerCapsule")
        {
            PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            player.ReverseGravity();
        }
        else if (other.gameObject.tag == "Reversable")
        {
            other.GetComponent<Reversable>().ReserveGravitiy();
        }
    }
}
