using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameUI : MonoBehaviour
{
    private GameObject pauseScreen;
    private GameObject saveScoreScreen;
    private GameObject afterSavingNamesScreen;
    private GameObject gameoverScreen;
    private GameObject endOfLevelScreen;
    private Text endLevelScore;

    private ArturPlayerOneController player1;
    private ArturPlayerTwoController player2;
    private ArturMetaInf metaInf;

    public void Start()
    {
        pauseScreen = GameObject.Find("PauseScreen");
        saveScoreScreen = GameObject.Find("SaveScoreScreen");
        afterSavingNamesScreen = GameObject.Find("AfterSavingNamesScreen");
        gameoverScreen = GameObject.Find("GameOverScreen");
        endOfLevelScreen = GameObject.Find("EndOfLevelScreen");
        endLevelScore = GameObject.Find("EndOfLevel - Score").GetComponent<Text>();

        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<ArturPlayerOneController>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<ArturPlayerTwoController>();
        metaInf = GetComponent<ArturMetaInf>();

        pauseScreen.SetActive(false);
        gameoverScreen.SetActive(false);
        endOfLevelScreen.SetActive(false);

        StandaloneInputModule inputModule = EventSystem.current.gameObject.GetComponent<StandaloneInputModule>();

        if(player1.controllerType == "DS4" && player1.playerNumber == 1)
        {
            inputModule.cancelButton = "ds4_p1_button_circle";
            inputModule.submitButton = "ds4_p1_button_x";
            inputModule.verticalAxis = "ds4_p1_left_vertical";
            inputModule.horizontalAxis = "ds4_p1_left_horizontal";
        }
        else if (player1.controllerType == "DS4" && player1.playerNumber == 2)
        {
            inputModule.cancelButton = "ds4_p2_button_circle";
            inputModule.submitButton = "ds4_p2_button_x";
            inputModule.verticalAxis = "ds4_p2_left_vertical";
            inputModule.horizontalAxis = "ds4_p2_left_horizontal";
        }
        else if (player1.controllerType == "Xbox" && player1.playerNumber == 1)
        {
            inputModule.cancelButton = "xbox_p1_button_x";
            inputModule.submitButton = "xbox_p1_button_a";
            inputModule.verticalAxis = "xbox_p1_left_vertical";
            inputModule.horizontalAxis = "xbox_p1_left_horizontal";
        }
        else if (player1.controllerType == "Xbox" && player1.playerNumber == 2)
        {
            inputModule.cancelButton = "xbox_p2_button_x";
            inputModule.submitButton = "xbox_p2_button_a";
            inputModule.verticalAxis = "xbox_p2_left_vertical";
            inputModule.horizontalAxis = "xbox_p2_left_horizontal";
        }


    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))        // TODO: Which Button should be pressed to start pause? 
        {
            StartPause();
        }
        if (metaInf != null)
        {
            if (metaInf.playerLives == 0)
            {
                gameoverScreen.SetActive(true);
                afterSavingNamesScreen.SetActive(false);
            }
            if (metaInf.endOfLevel && !endOfLevelScreen.active)
            {
                endLevelScore.text = metaInf.score.ToString();
                endOfLevelScreen.SetActive(true);
                player1.enabled = false;
                // TODO for all objects with rigidbody
                player1.GetComponent<Rigidbody>().isKinematic = true;
                player1.GetComponent<Rigidbody>().detectCollisions = false;

                player2.enabled = false;
                // TODO for all objects with rigidbody
                player2.GetComponent<Rigidbody>().isKinematic = true;
                player2.GetComponent<Rigidbody>().detectCollisions = false;
                metaInf.enabled = false;
                // TODO do this for all objects with rigid body

                if (metaInf.score < 50)
                {
                    GameObject.Find("Star1").SetActive(false);
                    GameObject.Find("Star2").SetActive(false);
                    GameObject.Find("Star3").SetActive(false);
                }
                else if (metaInf.score > 50 && metaInf.score < 100)
                {
                    GameObject.Find("Star1").SetActive(true);
                    GameObject.Find("Star2").SetActive(false);
                    GameObject.Find("Star3").SetActive(false);
                }
                else if (metaInf.score > 100 && metaInf.score < 200)
                {
                    GameObject.Find("Star1").SetActive(true);
                    GameObject.Find("Star2").SetActive(true);
                    GameObject.Find("Star3").SetActive(false);
                }
                else if (metaInf.score > 200)
                {
                    GameObject.Find("Star1").SetActive(true);
                    GameObject.Find("Star2").SetActive(true);
                    GameObject.Find("Star3").SetActive(true);
                }
            else if(metaInf.endOfLevel && endOfLevelScreen.active)
                {
                    if (Input.anyKeyDown) ChangeMenuScene("ice");
                }
                
            }
        }
            
    }

    public void ChangeMenuScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void StartPause()
    {
        player1.enabled = false;
        // TODO for all objects with rigidbody
        player1.GetComponent<Rigidbody>().isKinematic = true;
        player1.GetComponent<Rigidbody>().detectCollisions = false;

        player2.enabled = false;
        // TODO for all objects with rigidbody
        player2.GetComponent<Rigidbody>().isKinematic = true;
        player2.GetComponent<Rigidbody>().detectCollisions = false;
        metaInf.enabled = false;
        pauseScreen.SetActive(true);

        EventSystem.current.SetSelectedGameObject(pauseScreen.transform.Find("Menu").Find("Pause-Back").gameObject);
    }

    public void EndPause()
    {
        player1.enabled = true;
        // TODO for all objects with rigidbody
        player1.GetComponent<Rigidbody>().isKinematic = false;
        player1.GetComponent<Rigidbody>().detectCollisions = true;

        player2.enabled = true;
        // TODO for all objects with rigidbody
        player2.GetComponent<Rigidbody>().isKinematic = false;
        player2.GetComponent<Rigidbody>().detectCollisions = true;

        metaInf.enabled = true;
        pauseScreen.SetActive(false);
    }

    public void SaveScore(string name)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = metaInf.score, name = GameObject.Find("InputNames").GetComponent<InputField>().text }; // TODO wo speichern wir den Score??
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        highscores.highscoreEntryList.Add(highscoreEntry);
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();

        saveScoreScreen.SetActive(false);
        afterSavingNamesScreen.SetActive(true);
    }

    [System.Serializable]
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    /*
     * Represents a single High Score entry
     * */
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}
