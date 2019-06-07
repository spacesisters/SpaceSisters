using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuPause : MonoBehaviour
{
    public GameObject pause;
    private ArturPlayerOneController player1;
    private ArturPlayerTwoController player2;


    public void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<ArturPlayerOneController>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<ArturPlayerTwoController>();
        pause.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            startPause();
        }
    }

    public void startPause()
    {
        player1.pause();
        pause.SetActive(true);
    }

    public void endPause()
    {
        player1.endPause();
        pause.SetActive(false);
    }
}
