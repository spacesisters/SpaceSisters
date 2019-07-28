using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArturMetaInf : MonoBehaviour
{

    public int playerLives = 10;
    public float score = 0;
    public float starScore = 250;
    public bool endOfLevel;
    public int playerHealth;
    public float speedBonusTime;

    [SerializeField]
    public int initialPlayerHealth;

    private float cooldown = 0.1f;
    private float timer = 0;

    private void Awake()
    {
        playerHealth = initialPlayerHealth;
        playerLives = ArturGameManager.playerLives;
        score = ArturGameManager.playerScore;
    }

    private void Update()
    {
        if (playerLives > 0)
        {
            starScore -= Time.deltaTime;
            score -= Time.deltaTime;

            starScore = Math.Max(0, starScore);
            score = Math.Max(0, score);

        }

        if (playerHealth <= 0)
        {
            playerLives--;
            GameObject.FindGameObjectWithTag("ScreenFader").GetComponent<Animator>().Play("fade_out_in");

            StartCoroutine(Respawn(cooldown));
            playerHealth = initialPlayerHealth;

        }
    }


    IEnumerator Respawn(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        ArturPlayerOneController p1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<ArturPlayerOneController>();
        ArturPlayerTwoController p2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<ArturPlayerTwoController>();

        ArturBasePlayerController behind = p1;
        if (p2.transform.position.x < p1.transform.position.x)
            behind = p2;

        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        LevelMetaInf levelMetaInf = rooms[0].GetComponent<LevelMetaInf>();
        for (int i = 0; i < rooms.Length - 1; i++)
        {
            if (rooms[i].transform.position.x < behind.transform.position.x &&
                rooms[i + 1].transform.position.x > behind.transform.position.x)
            {
                levelMetaInf = rooms[i].GetComponent<LevelMetaInf>();
            }
        }
        Vector3 respawnPosition = levelMetaInf.respawnLocation;
        p1.transform.position = respawnPosition + new Vector3(0, 3, 0);
        p2.transform.position = respawnPosition + new Vector3(2, 3, 0);

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

        



        string enemyPath = "Prefabs/Main/Enemies/Enemy";
        List<Vector3> allEnemyPositions = GameObject.FindGameObjectWithTag("ProceduralGenerator").GetComponent<ArturProceduralGeneratorManagerScript>().allEnemyPositions;
        foreach (Vector3 enemyPos in allEnemyPositions)
        {
            GameObject enemy = Resources.Load<GameObject>(enemyPath);
            Instantiate(enemy, enemyPos, Quaternion.identity);
        }

        //GameObject.FindGameObjectWithTag("ScreenFader").GetComponent<Animator>().SetBool("fade_out_in", false);
    }
}
