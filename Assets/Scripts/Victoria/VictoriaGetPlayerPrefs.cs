using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoriaGetPlayerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(JsonUtility.FromJson<RoomNumber>(PlayerPrefs.GetString("numberOfRooms")) == null)
        {
            RoomNumber roomnumber = new RoomNumber { numberOfRooms = 10 };
            string json = JsonUtility.ToJson(roomnumber);
            PlayerPrefs.SetString("numberOfRooms", json);
            PlayerPrefs.Save();
            ArturGameManager.numberOfRooms = 10;
        }
        else
        {
            ArturGameManager.numberOfRooms = JsonUtility.FromJson<RoomNumber>(PlayerPrefs.GetString("numberOfRooms")).numberOfRooms;
        }
    }

    [System.Serializable]
    private class RoomNumber
    {
        public int numberOfRooms;
    }
}

    
