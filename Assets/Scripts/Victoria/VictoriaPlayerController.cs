using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoriaPlayerController : MonoBehaviour
{
    public int score; // **
    public float energy;  // // ** between 0 and 1
    public bool dead;  // **
    public float improvedAmmo; // between 0 and 1 // **
    public float improvedSpeed; // between 0 and 1 // **
    private int internalnum;
    private bool isPaused; // **
    private CharacterController cc;

    // TODO Pause: Freeze player
    // Start is called before the first frame update
    void Start()
    {
        score = 0; // **
        internalnum = 0; 
        improvedAmmo = 0; // **
        improvedSpeed = 0; // **
        energy = 1f; // **
        dead = false; // **
        isPaused = false; // **

    }

    // Update is called once per frame
    void Update()
    {
        if (!dead && !isPaused)
        {
            internalnum += 1;
            score += 1;
            if (energy > 0) energy -= 0.1f * Time.deltaTime;
            if (energy < 0.5) dead = true;
            if (internalnum == 60) improvedAmmo = 1;
            else if (internalnum > 10 && improvedAmmo > 0) improvedAmmo -= 0.1f * Time.deltaTime;
        }
    }

    public void pause() // **
    { 
        isPaused = true; // **
    }

    public void endPause() // **
    {
        isPaused = false; // **
    }
}
