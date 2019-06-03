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
        GameObject start = Resources.Load<GameObject>("Rooms/00" + (rand.Next(1, 1)));
        Instantiate(start);
        Vector3 roomPosition = new Vector3(start.GetComponent<LevelMetaInf>().lastBlock.x, start.GetComponent<LevelMetaInf>().lastBlock.y, 0);
        GameObject previousGameObject = start;
        for (int i = 1; i < numberOfRooms; i++)
        {
            GameObject nextRoom = Resources.Load<GameObject>("Rooms/" + rand.Next(1, 5));
            Instantiate(nextRoom, roomPosition, Quaternion.identity);
            roomPosition.x += nextRoom.GetComponent<LevelMetaInf>().lastBlock.x + 1;
            roomPosition.y += nextRoom.GetComponent<LevelMetaInf>().lastBlock.y;

            print(roomPosition);
        }
    }

}

