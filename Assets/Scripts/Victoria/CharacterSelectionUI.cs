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

    void Start()
    {
        controller_names = Input.GetJoystickNames();
        controllers_assigned = false;

    }

    void Update()
    {
        if (controllers_assigned == false)
        {
            // SetUp of Player1
            if (player1 == null && controller_names.Length > 0)
            {
                GameObject.Find("SelectPlayer1").SetActive(true);
                GameObject.Find("SelectPlayer2").SetActive(false);
                GameObject.Find("StartGame").SetActive(false);

                if (Input.GetButton("ds4_p1_button_x"))
                {
                    player1 = new Player { numForJulius = 1, controllertype = "DS4"};
                    controller1_number = 1;
                }
                else if (Input.GetButton("ds4_p2_button_x"))
                {
                    player1 = new Player { numForJulius = 2, controllertype = "DS4"};
                    controller1_number = 2;
                }
                else if (Input.GetButton("xbox_p1_button_a"))
                {
                    player1 = new Player { numForJulius = 1, controllertype = "Xbox"};
                    controller1_number = 1;
                }
                else if (Input.GetButton("xbox_p2_button_a"))
                {
                    player1 = new Player { numForJulius = 2, controllertype = "Xbox"};
                    controller1_number = 2;
                }
            }

            // SetUp for Play
            else if (player2 == null && controller_names.Length > 1)
            {
                GameObject.Find("SelectPlayer1").SetActive(false);
                GameObject.Find("SelectPlayer2").SetActive(true);
                GameObject.Find("StartGame").SetActive(false);

                if (Input.GetButton("ds4_p1_button_x") && controller1_number != 1)
                {
                    player2 = new Player { numForJulius = 1, controllertype = "DS4"};
                }
                else if (Input.GetButton("ds4_p2_button_x") && controller1_number != 2)
                {
                    player2 = new Player { numForJulius = 2, controllertype = "DS4"};
                }
                else if (Input.GetButton("xbox_p1_button_a") && controller1_number != 1)
                {
                    player2 = new Player { numForJulius = 1, controllertype = "Xbox" };
                }
                else // (Input.GetButton("xbox_p2_button_a") && controller1_number != 2)
                {
                    player2 = new Player { numForJulius = 2, controllertype = "Xbox"};
                    
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
            GameObject.Find("SelectPlayer1").SetActive(false);
            GameObject.Find("SelectPlayer2").SetActive(false);
            GameObject.Find("StartGame").SetActive(true);
        }
    }

        public void StartGame()
    {
        SceneManager.LoadScene("Assets/Scenes/Ice/");
    }

    [System.Serializable]
    private class Player
    {
        public int numForJulius;
        public string controllertype;
    }

}
