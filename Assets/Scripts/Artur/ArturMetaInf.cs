using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturMetaInf : MonoBehaviour
{
    
    public int playerLives = 10;
    public int score = 0;
    public bool endOfLevel;
    public int playerHealth;
    public float speedBonusTime;

    [SerializeField]
    private int initialPlayerHealth;

    private void Awake()
    {
        playerHealth = initialPlayerHealth;
    }

    private void Update()
    {
        score += 1; // TODO 

        if (playerHealth <= 0)
        {
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
            playerHealth = initialPlayerHealth;
        }
    }

}
