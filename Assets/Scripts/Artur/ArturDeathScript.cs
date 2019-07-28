using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturDeathScript : MonoBehaviour
{

    private float cooldown = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {

            StartCoroutine(Respawn(cooldown));

            GameObject.FindGameObjectWithTag("ScreenFader").GetComponent<Animator>().Play("fade_out_in");



        }

    }


    IEnumerator Respawn(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        GameObject arturSceneManager = GameObject.FindGameObjectWithTag("SceneManager");

        ArturMetaInf metaInf = arturSceneManager.GetComponent<ArturMetaInf>();
        LevelMetaInf levelMetaInf = transform.GetComponentInParent<LevelMetaInf>();

        Vector3 respawnPosition = levelMetaInf.respawnLocation;

        ArturPlayerOneController p1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<ArturPlayerOneController>();
        ArturPlayerTwoController p2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<ArturPlayerTwoController>();

        p1.Respawn(respawnPosition + new Vector3(0, 3, 0));
        p2.Respawn(respawnPosition + new Vector3(2, 3, 0));


        metaInf.playerLives--;


        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");

        foreach (GameObject g in rooms)
        {
            Vector3 instantiateAt = g.GetComponent<LevelMetaInf>().instantiatedAt;
            GameObject room = Resources.Load<GameObject>("Rooms/" + ArturSceneManager.currentLevel + "/" + g.GetComponent<LevelMetaInf>().roomNumber);
            room.GetComponent<LevelMetaInf>().instantiatedAt = instantiateAt;
            room.GetComponent<LevelMetaInf>().lastBlock = g.GetComponent<LevelMetaInf>().lastBlock;
            room.GetComponent<LevelMetaInf>().respawnLocation = g.GetComponent<LevelMetaInf>().respawnLocation;

            Instantiate(room, instantiateAt, Quaternion.identity);
            Destroy(g);

        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        string enemyPath = "Prefabs/Main/Enemies/Enemy";
        List<Vector3> allEnemyPositions = GameObject.FindGameObjectWithTag("ProceduralGenerator").GetComponent<ArturProceduralGeneratorManagerScript>().allEnemyPositions;
        foreach (Vector3 enemyPos in allEnemyPositions)
        {
            GameObject enemy = Resources.Load<GameObject>(enemyPath);
            Instantiate(enemy, enemyPos, Quaternion.identity);
        }

    }
}

