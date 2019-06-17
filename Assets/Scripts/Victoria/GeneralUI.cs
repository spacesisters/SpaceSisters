using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GeneralUI : MonoBehaviour
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
            if (metaInf.endOfLevel)
            {
                endLevelScore.text = metaInf.score.ToString();
                endOfLevelScreen.SetActive(true);
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

                if (Input.anyKeyDown) ChangeMenuScene("ice");
            }
        }
            
    }

    public void ChangeMenuScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void StartPause()
    {
        player1.pause();
        player2.pause();
        pauseScreen.SetActive(true);
    }

    public void EndPause()
    {
        player1.endPause();
        player2.endPause();
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
