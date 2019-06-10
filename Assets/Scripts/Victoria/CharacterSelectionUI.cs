using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterSelectionUI : MonoBehaviour
{
    private string[] controller_names;
    private bool controllers_assigned;

    private int controller1_number;

    Player player1, player2;
    GameObject selectplayer1, selectplayer2, startgame;

    void Start()
    {
        controllers_assigned = false;

        selectplayer1 = GameObject.Find("SelectPlayer1");
        selectplayer2 = GameObject.Find("SelectPlayer2");
        startgame = GameObject.Find("StartGame");

    }

    void Update()
    {
        if (controllers_assigned == false)
        {
            // SetUp of Player1
            if (player1 == null)
            {
                selectplayer1.SetActive(true);
                selectplayer2.SetActive(false);
                startgame.SetActive(false);

                if (Input.GetButton("ds4_p1_button_x"))
                {
                    player1 = new Player { playerNum = 1, controllertype = "DS4"};
                    controller1_number = 1;
                }
                else if (Input.GetButton("ds4_p2_button_x"))
                {
                    player1 = new Player { playerNum = 2, controllertype = "DS4"};
                    controller1_number = 2;
                }
                else if (Input.GetButton("xbox_p1_button_a"))
                {
                    player1 = new Player { playerNum = 1, controllertype = "Xbox"};
                    controller1_number = 1;
                }
                else if (Input.GetButton("xbox_p2_button_a"))
                {
                    player1 = new Player { playerNum = 2, controllertype = "Xbox"};
                    controller1_number = 2;
                }
            }

            // SetUp for Player2
            else if (player1 != null && player2 == null)
            {
                selectplayer1.SetActive(false);
                selectplayer2.SetActive(true);
                startgame.SetActive(false);

                if (Input.GetButton("ds4_p1_button_x") && controller1_number != 1)
                {
                    player2 = new Player { playerNum = 1, controllertype = "DS4"};
                }
                else if (Input.GetButton("ds4_p2_button_x") && controller1_number != 2)
                {
                    player2 = new Player { playerNum = 2, controllertype = "DS4"};
                }
                else if (Input.GetButton("xbox_p1_button_a") && controller1_number != 1)
                {
                    player2 = new Player { playerNum = 1, controllertype = "Xbox" };
                }
                else // (Input.GetButton("xbox_p2_button_a") && controller1_number != 2)
                {
                    player2 = new Player { playerNum = 2, controllertype = "Xbox"};
                    
                }

                controllers_assigned = true;

                Player[] players = { player1, player2 };
                string json = JsonUtility.ToJson(players);
                PlayerPrefs.SetString("Controllers", json);
                PlayerPrefs.Save();
            }
        }
        else
        {
            selectplayer1.SetActive(false);
            selectplayer2.SetActive(false);
            startgame.SetActive(true);
        }
    }

        public void StartGame()
    {
        SceneManager.LoadScene("Assets/Scenes/Ice/");
    }

    [System.Serializable]
    private class Player
    {
        public int playerNum;
        public string controllertype;
    }

}
