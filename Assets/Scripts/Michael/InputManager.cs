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
    public String options {get;}
    
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
                options          = "ds4_p1_options";
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
                options          = "ds4_p2_options";
            }
        }
        else if(String.Compare(type, "Xbox") == 0)
        {
            if (number == 1)
            {
                left_horizontal  = "xbox_p1_left_horizontal";
                left_vertical    = "xbox_p1_left_vertical";
                right_horizontal = "xbox_p1_right_horizontal";
                right_vertical   = "xbox_p1_right_vertical";
                button0          = "xbox_p1_button_a";
                button1          = "xbox_p1_button_x";
                button2          = "xbox_p1_button_y";
                button3          = "xbox_p1_button_b";
                l1               = "xbox_p1_l1";
                l2               = "xbox_p1_l2";
                r1               = "xbox_p1_r1";
                r2               = "xbox_p1_r2";
                options          = "xbox_p1_options";
            }
            else if (number == 2)
            {
                left_horizontal  = "xbox_p2_left_horizontal";
                left_vertical    = "xbox_p2_left_vertical";
                right_horizontal = "xbox_p2_right_horizontal";
                right_vertical   = "xbox_p2_right_vertical";
                button0          = "xbox_p2_button_a";
                button1          = "xbox_p2_button_x";
                button2          = "xbox_p2_button_y";
                button3          = "xbox_p2_button_b";
                l1               = "xbox_p2_l1";
                l2               = "xbox_p2_l2";
                r1               = "xbox_p2_r1";
                r2               = "xbox_p2_r2";
                options          = "xbox_p2_options";
            }
        }
        else if(String.Compare(type, "Keyboard") == 0)
        {
                left_horizontal  = "keyboard_left_horizontal";
                left_vertical    = "keyboard_left_vertical";
                right_horizontal = "keyboard_right_horizontal";
                right_vertical   = "keyboard_right_vertical";
                button0          = "keyboard_button_0";
                button1          = "keyboard_button_1";
                button2          = "keyboard_button_2";
                button3          = "keyboard_button_3";
                l1               = "keyboard_l1";
                l2               = "keyboard_l2";
                r1               = "keyboard_r1";
                r2               = "keyboard_r2";
                options          = "keyboard_options";
        }
    }

    public String controller_info()
    {
        return "Controller type: " + controller_type + " / Controller number: " + controller_number;
    }
}
