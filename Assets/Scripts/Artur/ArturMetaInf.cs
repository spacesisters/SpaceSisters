using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturMetaInf : MonoBehaviour
{
    
    public int playerLives = 10;
    public int score = 0;
    public bool endOfLevel;
    public int playerHealth;
    public float speedBonusTime;

    private void Update()
    {
        score += 1; // TODO 
    }

}
