using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MichaelDoorSwitch : MonoBehaviour
{
    [SerializeField] GameObject door_object;
    [SerializeField] bool permanent_switch;

    private MichaelDoorScript door;

    // Start is called before the first frame update
    void Start()
    {
        door = door_object.GetComponent<MichaelDoorScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if(door.get_door_state() == true)
        {
            door.set_door_state(false);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(!permanent_switch)
        {
            door.set_door_state(true);
        }
    }
}
