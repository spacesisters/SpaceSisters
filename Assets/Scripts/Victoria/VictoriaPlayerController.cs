using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoriaPlayerController : MonoBehaviour
{
    public double score;
    public float energy;  // zwischen 0 und 1
    public bool dead;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        energy = 1f;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        score += 0.1;
        energy -= 0.001f;
        if (energy < 0.8) {
            dead = true;
        }
    }
}
