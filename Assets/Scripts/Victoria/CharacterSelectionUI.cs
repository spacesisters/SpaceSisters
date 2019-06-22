using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
                bool set = false;
                int player1ControllerNumber = 0;
                string player1ControllerType = null;

                if (Input.GetButtonDown("ds4_p1_button_x"))
                {
                    set = true;
                    player1ControllerNumber = 1;
                    player1ControllerType = "DS4";
                }
                else if (Input.GetButtonDown("ds4_p2_button_x"))
                {
                    set = true;
                    player1ControllerNumber = 2;
                    player1ControllerType = "DS4";
                }
                else if (Input.GetButtonDown("xbox_p1_button_a"))
                {
                    set = true;
                    player1ControllerNumber = 1;
                    player1ControllerType = "Xbox";
                }
                else if (Input.GetButtonDown("xbox_p2_button_a"))
                {
                    set = true;
                    player1ControllerNumber = 2;
                    player1ControllerType = "Xbox";
                }
                else if (Input.GetButtonDown("keyboard_button_0"))
                {
                    set = true;
                    player1ControllerNumber = 0;
                    player1ControllerType = "Keyboard";
                }

                if (set)
                {
                    ArturControllerSettings.player1ControllerType = player1ControllerType;
                    ArturControllerSettings.player1ControllerNumber = player1ControllerNumber;
                    player1 = new Player(player1ControllerNumber, player1ControllerType);
                }
            }

            // SetUp for Player2
            else if (player1 != null && player2 == null)
            {
                selectplayer1.SetActive(false);
                selectplayer2.SetActive(true);
                startgame.SetActive(false);
                int player2ControllerNumber = 2;
                string player2ControllerType = "Xbox";

                if (Input.GetButtonDown("ds4_p1_button_x") && ArturControllerSettings.player1ControllerNumber != 1)
                {
                    controllers_assigned = true;

                    player2ControllerNumber = 1;
                    player2ControllerType = "DS4";

                }
                else if (Input.GetButtonDown("ds4_p2_button_x") && ArturControllerSettings.player1ControllerNumber != 2)
                {
                    controllers_assigned = true;

                    player2ControllerNumber = 2;
                    player2ControllerType = "DS4";
                }
                else if (Input.GetButtonDown("xbox_p1_button_a") && ArturControllerSettings.player1ControllerNumber != 1)
                {
                    controllers_assigned = true;

                    player2ControllerNumber = 1;
                    player2ControllerType = "Xbox";
                }
                else if (Input.GetButtonDown("xbox_p2_button_a") && ArturControllerSettings.player1ControllerNumber != 2)
                {
                    controllers_assigned = true;

                    player2ControllerNumber = 2;
                    player2ControllerType = "Xbox";
                }
                else if (Input.GetButtonDown("keyboard_button_0") && ArturControllerSettings.player1ControllerNumber != 0)
                {
                    controllers_assigned = true;

                    player2ControllerNumber = 0;
                    player2ControllerType = "Keyboard";
                }


                if (controllers_assigned)
                {
                    ArturControllerSettings.player2ControllerNumber = player2ControllerNumber;
                    ArturControllerSettings.player2ControllerType = player2ControllerType;
                    player2 = new Player(player2ControllerNumber, player2ControllerType);
                }

            }
        }
        else
        {
            selectplayer1.SetActive(false);
            selectplayer2.SetActive(false);
            startgame.SetActive(true);
            EventSystem.current.SetSelectedGameObject(startgame.gameObject);
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
        public string controllerType;

        public Player(int num, string type)
        {
            this.playerNum = num;
            this.controllerType = type;
        }
       
    }

}
