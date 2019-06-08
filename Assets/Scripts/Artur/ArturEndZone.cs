using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArturEndZone : MonoBehaviour
{
    public string nextScene;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            GetComponent<ArturMetaInf>().endOfLevel = true;
            //TODO: Show final score of this lvl. 
            //TODO: Show short cutscene?

            //SceneManager.LoadScene(nextScene);
        }

    }

}
