using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class controller_test : MonoBehaviour
{
    [SerializeField]Text text_component;
    [SerializeField]int number;
    [SerializeField]String type;

    public InputManager controller {set; get;}

    // Start is called before the first frame update
    void Start()
    {
        controller = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(controller == null)
        {
            text_component.text = "No controller selected\n";
        }
        else
        {
            print_controller_info();
        }

    }

    public void set_controller(int number, String type)
    {
        controller = new InputManager(number, type);
    }

    void print_controller_info()
    {
        float left_horizontal = Input.GetAxis(controller.left_horizontal);
        float left_vertical = Input.GetAxis(controller.left_vertical);
        float right_horizontal = Input.GetAxis(controller.right_horizontal);
        float right_vertical = Input.GetAxis(controller.right_vertical);
        bool button0 = Input.GetButton(controller.button0);
        bool button1 = Input.GetButton(controller.button1);
        bool button2 = Input.GetButton(controller.button2);
        bool button3 = Input.GetButton(controller.button3);
        bool l1 = Input.GetButton(controller.l1);
        bool l2 = Input.GetButton(controller.l2);
        bool r1 = Input.GetButton(controller.r1);
        bool r2 = Input.GetButton(controller.r2);
        bool options = Input.GetButton(controller.options);

        String output_text = "";

        output_text += controller.controller_info() + "\n";
        output_text += "Left Stick Horizontal: " + left_horizontal + "\n";
        output_text += "Left Stick Vertical: " + left_vertical + "\n";
        output_text += "Right Stick Horizontal: " + right_horizontal + "\n";
        output_text += "Right Stick Vertical: " + right_vertical + "\n";
        output_text += "Button 0: " + button0 + "\n";
        output_text += "Button 1: " + button1 + "\n";
        output_text += "Button 2: " + button2 + "\n";
        output_text += "Button 3: " + button3 + "\n";
        output_text += "Button L1: " + l1 + "\n";
        output_text += "Button L2: " + l2 + "\n";
        output_text += "Button R1: " + r1 + "\n";
        output_text += "Button R2: " + r2 + "\n";
        output_text += "Button Options: " + options + "\n";

        text_component.text = output_text;
    }
}
