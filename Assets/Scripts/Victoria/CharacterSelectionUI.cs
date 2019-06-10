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

    string player1Controller;

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

                if (Input.GetButtonDown("ds4_p1_button_x"))
                {
                    player1 = new Player { playerNum = 1, controllertype = "DS4"};
                    controller1_number = 1;
                    ArturControllerSettings.player1ControllerNumber = 1;
                    ArturControllerSettings.player1ControllerType = "DS4";
                }
                else if (Input.GetButtonDown("ds4_p2_button_x"))
                {
                    player1 = new Player { playerNum = 2, controllertype = "DS4"};
                    controller1_number = 2;

                    ArturControllerSettings.player1ControllerNumber = 2;
                    ArturControllerSettings.player1ControllerType = "DS4";
                }
                else if (Input.GetButtonDown("xbox_p1_button_a"))
                {
                    player1 = new Player { playerNum = 1, controllertype = "Xbox"};
                    controller1_number = 1;

                    ArturControllerSettings.player1ControllerNumber = 1;
                    ArturControllerSettings.player1ControllerType = "Xbox";
                }
                else if (Input.GetButtonDown("xbox_p2_button_a"))
                {
                    player1 = new Player { playerNum = 2, controllertype = "Xbox"};
                    controller1_number = 2;

                    ArturControllerSettings.player1ControllerNumber = 2;
                    ArturControllerSettings.player1ControllerType = "Xbox";
                }

            }

            // SetUp for Player2
            else if (player1 != null && player2 == null)
            {
                selectplayer1.SetActive(false);
                selectplayer2.SetActive(true);
                startgame.SetActive(false);

                if (Input.GetButtonDown("ds4_p1_button_x") && controller1_number != 1)
                {
                    player2 = new Player { playerNum = 1, controllertype = "DS4"};
                    controllers_assigned = true;

                    Player[] players = { player1, player2 };
                    string json = JsonUtility.ToJson(players);
                    print(json);

                    PlayerPrefs.SetString("Controllers", json);
                    PlayerPrefs.Save();

                    ArturControllerSettings.player2ControllerNumber = 1;
                    ArturControllerSettings.player2ControllerType = "DS4";

                }
                else if (Input.GetButtonDown("ds4_p2_button_x") && controller1_number != 2)
                {
                    player2 = new Player { playerNum = 2, controllertype = "DS4"};
                    controllers_assigned = true;

                    Player[] players = { player1, player2 };
                    string json = JsonUtility.ToJson(players);
                    print(json);

                    PlayerPrefs.SetString("Controllers", json);
                    PlayerPrefs.Save();


                    ArturControllerSettings.player2ControllerNumber = 2;
                    ArturControllerSettings.player2ControllerType = "DS4";
                }
                else if (Input.GetButtonDown("xbox_p1_button_a") && controller1_number != 1)
                {
                    print("player2 1");
                    player2 = new Player { playerNum = 1, controllertype = "Xbox" };
                    controllers_assigned = true;

                    Player[] players = { player1, player2 };
                    string json = JsonUtility.ToJson(players);
                    print(json);
                    PlayerPrefs.SetString("Controllers", json);
                    PlayerPrefs.Save();

                    ArturControllerSettings.player2ControllerNumber = 1;
                    ArturControllerSettings.player2ControllerType = "Xbox";
                }
                else if (Input.GetButtonDown("xbox_p2_button_a") && controller1_number != 2)
                {
                    print("player2 2");

                    player2 = new Player { playerNum = 2, controllertype = "Xbox"};
                    controllers_assigned = true;

                    Player[] players = { player1, player2 };
                    string json = JsonUtility.ToJson(players);
                    print(json);
                    PlayerPrefs.SetString("Controllers", json);
                    PlayerPrefs.Save();

                    ArturControllerSettings.player2ControllerNumber = 2;
                    ArturControllerSettings.player2ControllerType = "Xbox";
                } 
                

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
        SceneManager.LoadScene("Scenes/Main/Ice");
    }

    [System.Serializable]
    private class Player
    {
        public int playerNum;
        public string controllertype;
    }

}
