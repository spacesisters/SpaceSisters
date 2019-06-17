using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturProceduralGeneratorManagerScript : MonoBehaviour
{
    public int numberOfRooms;
    public string currentLevel;

    void Awake()
    {

        string roomPath = "Rooms/" + currentLevel + "/";
        System.Random rand = new System.Random();
        GameObject firstRoom = Resources.Load<GameObject>(roomPath + "start" + (rand.Next(1, 1)));
        firstRoom.GetComponent<LevelMetaInf>().respawnLocation = Vector3.zero;
        firstRoom.GetComponent<LevelMetaInf>().instantiatedAt = Vector3.zero;

        Instantiate(firstRoom);
        Vector3 roomPosition = new Vector3(firstRoom.GetComponent<LevelMetaInf>().lastBlock.x, 
                                            firstRoom.GetComponent<LevelMetaInf>().lastBlock.y, 0);

        for (int i = 1; i < numberOfRooms; i++)
        {
            GameObject nextRoom = Resources.Load<GameObject>(roomPath + rand.Next(1, 6));
            nextRoom.GetComponent<LevelMetaInf>().respawnLocation = roomPosition;
            nextRoom.GetComponent<LevelMetaInf>().respawnLocation = roomPosition;
            nextRoom.GetComponent<LevelMetaInf>().instantiatedAt = roomPosition;
            Instantiate(nextRoom, roomPosition, Quaternion.identity);



            roomPosition.x += nextRoom.GetComponent<LevelMetaInf>().lastBlock.x + 1;
            roomPosition.y += nextRoom.GetComponent<LevelMetaInf>().lastBlock.y;
        }

        GameObject endRoom = Resources.Load<GameObject>(roomPath + "end");
        endRoom.GetComponent<LevelMetaInf>().instantiatedAt = roomPosition;
        Instantiate(endRoom, roomPosition, Quaternion.identity);

        string playerOnePath = "Prefabs/Main/PlayerCharacters/ArturMainCharacter1";
        string playerTwoPath = "Prefabs/Main/PlayerCharacters/ArturMainCharacter2";

        GameObject playerOne = Resources.Load<GameObject>(playerOnePath);
        GameObject playerTwo = Resources.Load<GameObject>(playerTwoPath);

        Instantiate(playerOne);
        Instantiate(playerTwo);
    }

}

