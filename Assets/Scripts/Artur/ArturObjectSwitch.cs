using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturObjectSwitch : MonoBehaviour
{

    public GameObject target;
    public bool permanent;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("MovableBlock"))
        {
            target.SetActive(!target.activeSelf);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("MovableBlock"))
        {
            if (!permanent)
            {
                target.SetActive(!target.activeSelf);
            }
        }
    }

}
