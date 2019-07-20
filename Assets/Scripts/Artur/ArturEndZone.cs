using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArturEndZone : MonoBehaviour
{
    public string nextScene;
    public Camera rocketCamera;
    public GameObject rocket;
    public float rocketLaunchSpeed;
    public float waitTime;

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

            //SceneManager.LoadScene(nextScene);
        }

    }

    IEnumerator EndTheLevel(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ArturMetaInf>().endOfLevel = true;
    }

}
