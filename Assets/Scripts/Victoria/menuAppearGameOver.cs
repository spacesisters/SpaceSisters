using UnityEngine;
using System.Collections;

public class menuAppearGameOver : MonoBehaviour
{

    public GameObject gameover;
    public GameObject backToMenu;
    public VictoriaPlayerController player;

    private void Start()
    {
        gameover.SetActive(false);
        backToMenu.SetActive(false);
    }
    void Update()
    {
        if (player.dead)
        {
            gameover.SetActive(true);
        }
    }
}