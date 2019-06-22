using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArturTutorialScript : MonoBehaviour
{

    public Image img;

    private int counter = 0;

    private void Start()
    {
        img.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (counter == 0 && (other.CompareTag("Player1") || other.CompareTag("Player2")))
        {
            Time.timeScale = 0;
            img.enabled = true;
            counter++;
        }
    }

    private void Update()
    {
        if (img.enabled)
        {
            ArturBasePlayerController player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<ArturBasePlayerController>();
            ArturBasePlayerController player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<ArturBasePlayerController>();

            // TODO: Maybe use a button that doesn't do anything.
            if (player1.GetPlayerInput().dashButton || player2.GetPlayerInput().dashButton)
            {
                
                Time.timeScale = 1;
                img.enabled = false;
            }

        }
    }


}
