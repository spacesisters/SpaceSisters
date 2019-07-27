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
    private GameObject endOfLevelScreenFire;
    private Text endLevelScore;
    private Text endLevelScoreF;

    private ArturPlayerOneController player1;
    private ArturPlayerTwoController player2;


    private GameObject p1GameObject, p2GameObject;

    private ArturMetaInf metaInf;

    public void Start()
    {
        pauseScreen = GameObject.Find("PauseScreen");
        saveScoreScreen = GameObject.Find("SaveScoreScreen");
        afterSavingNamesScreen = GameObject.Find("AfterSavingNamesScreen");
        gameoverScreen = GameObject.Find("GameOverScreen");
        endOfLevelScreen = GameObject.Find("EndOfLevelScreen");
        endOfLevelScreenFire = GameObject.Find("EndOfLevelScreenFire");
        endLevelScore = GameObject.Find("EndOfLevel - Score").GetComponent<Text>();
        endLevelScoreF = GameObject.Find("EndOfLevel - ScoreF").GetComponent<Text>();

        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<ArturPlayerOneController>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<ArturPlayerTwoController>();

        p1GameObject = GameObject.FindGameObjectWithTag("Player1");
        p2GameObject = GameObject.FindGameObjectWithTag("Player2");

        metaInf = GetComponent<ArturMetaInf>();
   
        pauseScreen.SetActive(false);
        gameoverScreen.SetActive(false);
        endOfLevelScreen.SetActive(false);
        endOfLevelScreenFire.SetActive(false);

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
                metaInf.playerLives = -1;
                p1GameObject.SetActive(false);
                p2GameObject.SetActive(false);

                gameoverScreen.SetActive(true);
                afterSavingNamesScreen.SetActive(false);
            }
            if (metaInf.endOfLevel && !endOfLevelScreen.active && ArturSceneManager.currentLevel == "ice")
            {
                Time.timeScale = 0;
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
            /*else if(metaInf.endOfLevel && endOfLevelScreen.active)
                {
                    if (Input.anyKeyDown) ChangeMenuScene("ice");
                }
             */  
            } else if (metaInf.endOfLevel && !endOfLevelScreenFire.active && ArturSceneManager.currentLevel == "fire")
            {
                Time.timeScale = 0;
                endLevelScoreF.text = metaInf.score.ToString();
                endOfLevelScreenFire.SetActive(true);
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
                    GameObject.Find("Star1F").SetActive(false);
                    GameObject.Find("Star2F").SetActive(false);
                    GameObject.Find("Star3F").SetActive(false);
                }
                else if (metaInf.score > 50 && metaInf.score < 100)
                {
                    GameObject.Find("Star1F").SetActive(true);
                    GameObject.Find("Star2F").SetActive(false);
                    GameObject.Find("Star3F").SetActive(false);
                }
                else if (metaInf.score > 100 && metaInf.score < 200)
                {
                    GameObject.Find("Star1F").SetActive(true);
                    GameObject.Find("Star2F").SetActive(true);
                    GameObject.Find("Star3F").SetActive(false);
                }
                else if (metaInf.score > 200)
                {
                    GameObject.Find("Star1F").SetActive(true);
                    GameObject.Find("Star2F").SetActive(true);
                    GameObject.Find("Star3F").SetActive(true);
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
        Time.timeScale = 0;
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
        pauseScreen.transform.Find("Menu").Find("Pause-Back").GetComponent<Button>().OnSelect(null);
    }

    public void EndPause()
    {
        Time.timeScale = 1;
        //AudioSource.PlayClipAtPoint(GameObject.Find("PauseScreen").transform.Find("Menu").Find("Pause-Back").gameObject.GetComponent<ClickSound>().sound, transform.position);

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

    public void SaveScore()
    {
        string name = "";
        if (gameoverScreen.active)
        {
            name = GameObject.Find("Actual Names").GetComponent<Text>().text;
        } else if(ArturSceneManager.currentLevel == "fire")
        {
            name = GameObject.Find("Actual Names Fire").GetComponent<Text>().text;
        }

        HighscoreEntry highscoreEntry = new HighscoreEntry { score = metaInf.score, name = name }; 
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        if(highscores == null)
        {
            highscores = new Highscores();
        }
        highscores.highscoreEntryList.Add(highscoreEntry);
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();

        if(ArturSceneManager.currentLevel == "ice")
        {
            afterSavingNamesScreen.SetActive(true);
            saveScoreScreen.SetActive(false);
        } else if(ArturSceneManager.currentLevel == "fire")
        {
            ChangeMenuScene("EndScene");
        }

        Debug.Log(name + " " + metaInf.score);

    }

    [System.Serializable]
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
        public Highscores()
        {
            if (highscoreEntryList == null)
            {
                highscoreEntryList = new List<HighscoreEntry>();
            }
        }
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
