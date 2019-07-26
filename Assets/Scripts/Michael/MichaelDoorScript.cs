using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MichaelDoorScript : MonoBehaviour
{
    private GameObject door;
    private bool door_state;

    // Start is called before the first frame update
    void Start()
    {
        door = transform.Find("door").gameObject;
        door_state = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void switch_door_state()
    {
        door.active = !door.active;
        door_state = door.active;
    }

    public void set_door_state(bool state)
    {
        door.active = state;
    }

    public bool get_door_state()
    {
        return door.active;
    }
}
