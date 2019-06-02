using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controller_binding : MonoBehaviour
{
    [SerializeField] controller_test controller1;
    [SerializeField] controller_test controller2;
    [SerializeField] Text text_component;

    private String[] controller_names;
    private bool controllers_assigned;

    private int controller1_number, controller2_number;

    // Start is called before the first frame update
    void Start()
    {
        controller_names = Input.GetJoystickNames();
        controllers_assigned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(controllers_assigned == false)
        {
            if(controller1.controller == null && controller_names.Length > 0)
            {
                text_component.text = "Player 1 press the assign button on your controller\n";
                
                if(Input.GetButton("ds4_p1_button_x"))
                {
                    controller1.set_controller(1, "DS4");
                    controller1_number = 1;
                }
                else if(Input.GetButton("ds4_p2_button_x"))
                {
                    controller1.set_controller(2, "DS4");
                    controller1_number = 2;
                }
                /*
                else if( xbox_p1_...)
                {
                }
                else if( xbox_p2_...)
                {
                }
                */
            }
            else if(controller2.controller == null && controller_names.Length > 1)
            {
                text_component.text = "Player 2 press the assign button on your controller\n";
                
                if(Input.GetButton("ds4_p1_button_x") && controller1_number != 1)
                {
                    controller2.set_controller(1, "DS4");
                    controllers_assigned = true;
                    text_component.text = "";
                }
                else if(Input.GetButton("ds4_p2_button_x") && controller1_number != 2)
                {
                    controller2.set_controller(2, "DS4");
                    controllers_assigned = true;
                    text_component.text = "";
                }
                /*
                else if( xbox_p1_...)
                {
                }
                else if( xbox_p2_...)
                {
                }
                */
            }
        }


        
    }
}
