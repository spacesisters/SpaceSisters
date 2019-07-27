using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ArturEndZone : MonoBehaviour
{
    public string nextScene;
    public Camera rocketCamera;
    public GameObject rocket;
    public float rocketLaunchSpeed;
    public float waitTime;
    public float endCutsceneTime;
    public bool playCutscene;

    private float fadeTime = 2f;
    private bool endOfLevel = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            ArturBasePlayerController player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<ArturBasePlayerController>();
            ArturBasePlayerController player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<ArturBasePlayerController>();

            GameObject.FindGameObjectWithTag("Player1").SetActive(false);
            GameObject.FindGameObjectWithTag("Player2").SetActive(false);
            

            GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
            rocketCamera.enabled = true;

            rocket.GetComponent<Rigidbody>().velocity = new Vector3(0, rocketLaunchSpeed, 0);


            StartCoroutine(EndTheLevel(waitTime));


            //TODO: Show final score of this lvl. 
            //TODO: Show short cutscene?

        }

    }

    public void Update()
    {
        if (Input.anyKey && endOfLevel && !(ArturSceneManager.currentLevel == "fire"))
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    IEnumerator EndTheLevel(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject.FindGameObjectWithTag("ScreenFader").GetComponent<Animator>().SetBool("fadeOut", true);
        yield return new WaitForSeconds(fadeTime);
        GameObject screenFader = GameObject.FindGameObjectWithTag("ScreenFader");
        screenFader.SetActive(false);
        rocket.SetActive(false);

        if (playCutscene)
        {
            GameObject gameCanvas = GameObject.FindGameObjectWithTag("GameCanvas");
            gameCanvas.SetActive(false);


            GameObject.FindGameObjectWithTag("VideoPlayer").GetComponent<VideoPlayer>().targetCamera = rocketCamera;
            GameObject.FindGameObjectWithTag("VideoPlayer").GetComponent<VideoPlayer>().enabled = true;
            rocketCamera.farClipPlane = 1000;

            yield return new WaitForSeconds(endCutsceneTime);


            gameCanvas.SetActive(true);
        }
        

        GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ArturMetaInf>().endOfLevel = true;

        ArturGameManager.playerLives = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ArturMetaInf>().playerLives;
        ArturGameManager.playerScore += GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ArturMetaInf>().score;
        endOfLevel = true;

    }

}
