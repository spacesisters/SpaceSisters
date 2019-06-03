using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuPause : MonoBehaviour
{
    public GameObject pause;
    public VictoriaPlayerController player;

    public void Start()
    {
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
        player.pause();
        pause.SetActive(true);
    }

    public void endPause()
    {
        player.endPause();
        pause.SetActive(false);
    }
}
