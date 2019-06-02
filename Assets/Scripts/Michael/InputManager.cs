using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    private String controller_type;
    private int controller_number;

    public String left_horizontal {get;}
    public String left_vertical {get;}
    public String right_horizontal {get;}
    public String right_vertical {get;}
    public String button0 {get;}
    public String button1 {get;}
    public String button2 {get;}
    public String button3 {get;}
    public String l1 {get;}
    public String l2 {get;}
    public String r1 {get;}
    public String r2 {get;}
    
    public InputManager(int number, String type)
    {
        controller_number = number;
        controller_type = type;

        if(String.Compare(type, "DS4") == 0)
        {
            if(number == 1)
            {
                left_horizontal  = "ds4_p1_left_horizontal";
                left_vertical    = "ds4_p1_left_vertical";
                right_horizontal = "ds4_p1_right_horizontal";
                right_vertical   = "ds4_p1_right_vertical";
                button0          = "ds4_p1_button_x";
                button1          = "ds4_p1_button_square";
                button2          = "ds4_p1_button_triangle";
                button3          = "ds4_p1_button_circle";
                l1               = "ds4_p1_l1";
                l2               = "ds4_p1_l2";
                r1               = "ds4_p1_r1";
                r2               = "ds4_p1_r2";
            }
            else if (number == 2)
            {
                left_horizontal  = "ds4_p2_left_horizontal";
                left_vertical    = "ds4_p2_left_vertical";
                right_horizontal = "ds4_p2_right_horizontal";
                right_vertical   = "ds4_p2_right_vertical";
                button0          = "ds4_p2_button_x";
                button1          = "ds4_p2_button_square";
                button2          = "ds4_p2_button_triangle";
                button3          = "ds4_p2_button_circle";
                l1               = "ds4_p2_l1";
                l2               = "ds4_p2_l2";
                r1               = "ds4_p2_r1";
                r2               = "ds4_p2_r2";
            }
        }
        else if(String.Compare(type, "XboxOne") == 0)
        {
            //TODO
        }
    }

    public String controller_info()
    {
        return "Controller type: " + controller_type + " / Controller number: " + controller_number;
    }
}
