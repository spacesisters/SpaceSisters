using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArturProceduralGeneratorManagerScript : MonoBehaviour
{
    public int numberOfRooms;
    public int testSpecificRoom;
    public string currentLevel;
    public NavMeshSurface surface;
    public List<Vector3> allEnemyPositions;

    void Awake()
    {
        int numberOfStartRooms = 1;
        int numberOfRandomRooms = 1;
        int numberOfPuzzleRooms = 1;
        // Can of course be different
        if (currentLevel == "tutorial")
        {
            numberOfStartRooms = 1;
            numberOfRandomRooms = 1;
            numberOfPuzzleRooms = 1;
        }
        else if (currentLevel == "ice")
        {
            numberOfStartRooms = 1;
            numberOfRandomRooms = 17;
            numberOfPuzzleRooms = 1;
        }
        else if (currentLevel == "fire")
        {
            numberOfStartRooms = 1;
            numberOfRandomRooms = 4;
            numberOfPuzzleRooms = 1;
        }
        else if (currentLevel == "michael_test")
        {
            numberOfStartRooms = 1;
            numberOfRandomRooms = 1;
            numberOfPuzzleRooms = 1;
        }

        allEnemyPositions = new List<Vector3>();
        string roomPath = "Rooms/" + currentLevel + "/";
        System.Random rand = new System.Random();
        GameObject firstRoom = Resources.Load<GameObject>(roomPath + "start" + (rand.Next(1, numberOfStartRooms)));
        firstRoom.GetComponent<LevelMetaInf>().instantiatedAt = Vector3.zero;

        Instantiate(firstRoom);
        Vector3 roomPosition = new Vector3(firstRoom.GetComponent<LevelMetaInf>().lastBlock.x, 
                                            firstRoom.GetComponent<LevelMetaInf>().lastBlock.y, 0);
        List<GameObject> gameRooms = new List<GameObject>();
        gameRooms.Add(firstRoom);

        for(int i = 0; i < firstRoom.GetComponent<LevelMetaInf>().enemyPositions.Count; i++)
        {
            allEnemyPositions.Add(firstRoom.GetComponent<LevelMetaInf>().enemyPositions[i] + firstRoom.GetComponent<LevelMetaInf>().instantiatedAt);
        }


        if (testSpecificRoom != 0)
        {
            GameObject nextRoom = Resources.Load<GameObject>(roomPath + testSpecificRoom);
            nextRoom.GetComponent<LevelMetaInf>().respawnLocation = roomPosition;
            nextRoom.GetComponent<LevelMetaInf>().instantiatedAt = roomPosition;
            Instantiate(nextRoom, roomPosition, Quaternion.identity);

            gameRooms.Add(nextRoom);

            roomPosition.x += nextRoom.GetComponent<LevelMetaInf>().lastBlock.x + 1;
            roomPosition.y += nextRoom.GetComponent<LevelMetaInf>().lastBlock.y;
        }

        for (int i = 1; i < numberOfRooms; i++)
        {

            /*
             * Controlling the random generation a bit.
             * GameObject nextRoom;
             * if (i % 3 == 0) 
             * {
             *      // Generate bigger room with a puzzle.
             *      GameObject nextRoom = Resources.Load<GameObject>(roomPath + "puzzles/" + rand.Next(1, numberOfPuzzleRooms));
             * }
             * else
             * {
             *      // Random room.
             *      GameObject nextRoom = Resources.Load<GameObject>(roomPath + rand.Next(1, numberOfRandomRooms));
             * }
             */


            GameObject nextRoom = Resources.Load<GameObject>(roomPath + rand.Next(1, numberOfRandomRooms)); // TODO
            nextRoom.GetComponent<LevelMetaInf>().respawnLocation = roomPosition;
            nextRoom.GetComponent<LevelMetaInf>().instantiatedAt = roomPosition;
            Instantiate(nextRoom, roomPosition, Quaternion.identity);

            gameRooms.Add(nextRoom);

            roomPosition.x += nextRoom.GetComponent<LevelMetaInf>().lastBlock.x + 1;
            roomPosition.y += nextRoom.GetComponent<LevelMetaInf>().lastBlock.y;


            for (int j = 0; j < nextRoom.GetComponent<LevelMetaInf>().enemyPositions.Count; j++)
            {
                allEnemyPositions.Add(nextRoom.GetComponent<LevelMetaInf>().enemyPositions[j] + nextRoom.GetComponent<LevelMetaInf>().instantiatedAt);
            }

        }

        GameObject endRoom = Resources.Load<GameObject>(roomPath + "end");
        endRoom.GetComponent<LevelMetaInf>().instantiatedAt = roomPosition;

        Instantiate(endRoom, roomPosition, Quaternion.identity);

        string playerOnePath = "Prefabs/Main/PlayerCharacters/ArturMainCharacter1";
        string playerTwoPath = "Prefabs/Main/PlayerCharacters/ArturMainCharacter2";

        GameObject playerOne = Resources.Load<GameObject>(playerOnePath);
        GameObject playerTwo = Resources.Load<GameObject>(playerTwoPath);

        Instantiate(playerOne, firstRoom.GetComponent<LevelMetaInf>().respawnLocation, Quaternion.identity);
        Instantiate(playerTwo, firstRoom.GetComponent<LevelMetaInf>().respawnLocation + new Vector3(2, 0, 0), Quaternion.identity);

        gameRooms.Add(endRoom);
        surface.BuildNavMesh();
        string enemyPath = "Prefabs/Main/Enemies/Enemy";

        foreach(Vector3 enemyPos in allEnemyPositions)
        {
            GameObject enemy = Resources.Load<GameObject>(enemyPath);
            Instantiate(enemy, enemyPos, Quaternion.identity);
        }


    }

}

