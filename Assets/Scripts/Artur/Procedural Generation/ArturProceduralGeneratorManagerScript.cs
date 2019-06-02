using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturProceduralGeneratorManagerScript : MonoBehaviour
{
    public int numberOfRooms;
    public float levelWidth;

    void Awake()
    {
        System.Random rand = new System.Random();
        GameObject start = Resources.Load<GameObject>("Rooms/0" + (rand.Next(1, 1)) + "/" + (rand.Next(1, 1)));
        Instantiate(start);
        Vector3 roomPosition = new Vector3(levelWidth, 0, 0);
        GameObject previousGameObject = start;
        for (int i = 1; i < numberOfRooms; i++)
        {
            int prevousEnding = previousGameObject.GetComponent<LevelMetaInf>().ending;
            int nextEnding = rand.Next(1, 1);
            int nextVersion = rand.Next(1, 3);
            print("Room: " + i + " " + "Version: " + nextVersion);
            GameObject nextRoom = Resources.Load<GameObject>("Rooms/" + prevousEnding + "" + nextEnding + "/" + nextVersion);
            Instantiate(nextRoom, roomPosition, Quaternion.identity);
            previousGameObject = nextRoom;
            roomPosition.x += levelWidth;
        }
    }

}

