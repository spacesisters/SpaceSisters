using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour
{
    public Text score;
    private ArturPlayerOneController player1;
    private ArturPlayerTwoController player2;
  

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<ArturPlayerOneController>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<ArturPlayerTwoController>();

        score.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (!player1.dead || !player2.dead)
        {
            score.text = player1.score.ToString();
        }
        
    }
}
