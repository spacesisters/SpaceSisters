using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturDeathScript : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            ArturMetaInf metaInf = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ArturMetaInf>();
            LevelMetaInf levelMetaInf = transform.GetComponentInParent<LevelMetaInf>();


            Vector3 respawnPosition = levelMetaInf.respawnLocation;

            ArturPlayerOneController p1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<ArturPlayerOneController>();
            ArturPlayerTwoController p2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<ArturPlayerTwoController>();

            p1.transform.position = respawnPosition + new Vector3(0, 3, 0);
            p2.transform.position = respawnPosition + new Vector3(2, 3, 0);

            metaInf.playerLives--;
        }
    }
}
