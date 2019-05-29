using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour
{
    public Text score;
    public VictoriaPlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        score.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.dead)
        {
            score.text = player.score.ToString();
        }
        
    }
}
