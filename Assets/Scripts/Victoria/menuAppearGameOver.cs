using UnityEngine;
using System.Collections;

public class menuAppearGameOver : MonoBehaviour
{

    public GameObject gameover;
    public GameObject backToMenu;
    private ArturPlayerOneController player1;
    private ArturPlayerTwoController player2;
    private ArturMetaInf metaInf;

    private void Start()
    {
        metaInf = GetComponent<ArturMetaInf>();
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<ArturPlayerOneController>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<ArturPlayerTwoController>();
        gameover.SetActive(false);
        backToMenu.SetActive(false);
    }
    void Update()
    {
        if (metaInf.playerLives == 0)
        {
            gameover.SetActive(true);
        }
    }
}