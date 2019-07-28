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
        if (isNumeric && !nORooms.Equals(""))
        {
            numberOfRooms = Convert.ToInt32(nORooms);
            GameObject.FindGameObjectWithTag("NumberOfRoomsPlaceholder").GetComponent<Text>().text = nORooms;

            RoomNumber roomnumber = new RoomNumber { numberOfRooms = numberOfRooms };
            string json = JsonUtility.ToJson(roomnumber);
            PlayerPrefs.SetString("numberOfRooms", json);
            PlayerPrefs.Save();
        }
    }

    [System.Serializable]
    private class RoomNumber
    {
        public int numberOfRooms;
    }
}