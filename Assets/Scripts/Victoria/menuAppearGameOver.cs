using UnityEngine;
using System.Collections;

public class menuAppearGameOver : MonoBehaviour
{

    public GameObject gameover;
    public GameObject backToMenu;
    private ArturPlayerOneController player1;
    private ArturPlayerTwoController player2;

    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<ArturPlayerOneController>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<ArturPlayerTwoController>();
        gameover.SetActive(false);
        backToMenu.SetActive(false);
    }
    void Update()
    {
        if (player1.dead || player2.dead)
        {
            gameover.SetActive(true);
        }
    }
}