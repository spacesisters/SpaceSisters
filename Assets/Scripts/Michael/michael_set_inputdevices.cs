using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class michael_set_inputdevices : MonoBehaviour
{

    void Awake()
    {
        ArturControllerSettings.player1ControllerType = "DS4";
        ArturControllerSettings.player1ControllerNumber = 1;
        ArturControllerSettings.player2ControllerType = "Keyboard";
        ArturControllerSettings.player2ControllerNumber = 0;
        

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
