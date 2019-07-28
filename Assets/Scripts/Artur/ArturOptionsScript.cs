using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class ArturOptionsScript : MonoBehaviour
{

    public void SetNumberOfRooms()
    {
        int numberOfRooms;
        string nORooms = GameObject.FindGameObjectWithTag("NumberOfRoomsOption").GetComponent<Text>().text;
        bool isNumeric = int.TryParse(nORooms, out numberOfRooms);
        if (isNumeric)
        {
            numberOfRooms = Convert.ToInt32(nORooms);
        }

        ArturGameManager.numberOfRooms = numberOfRooms;
    }
}
