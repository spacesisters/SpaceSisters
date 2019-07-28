using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoriaGetPlayerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ArturGameManager.numberOfRooms = JsonUtility.FromJson<RoomNumber>(PlayerPrefs.GetString("numberOfRooms")).numberOfRooms;
    }

    [System.Serializable]
    private class RoomNumber
    {
        public int numberOfRooms;
    }
}

    
