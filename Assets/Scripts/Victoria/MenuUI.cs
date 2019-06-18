using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuUI : MonoBehaviour
{
    public void Update()
    {
            StandaloneInputModule inputModule = EventSystem.current.gameObject.GetComponent<StandaloneInputModule>();
            if (Input.GetAxis("ds4_p1_left_vertical") != 0 || Input.GetButtonDown("ds4_p1_button_x"))
            {
                inputModule.cancelButton = "ds4_p1_button_circle";
                inputModule.submitButton = "ds4_p1_button_x";
                inputModule.verticalAxis = "ds4_p1_left_vertical";
                inputModule.horizontalAxis = "ds4_p1_left_horizontal";
            }
            else if (Input.GetAxis("ds4_p2_left_vertical") != 0 || Input.GetButtonDown("ds4_p2_button_x"))
            {
                inputModule.cancelButton = "ds4_p2_button_circle";
                inputModule.submitButton = "ds4_p2_button_x";
                inputModule.verticalAxis = "ds4_p2_left_vertical";
                inputModule.horizontalAxis = "ds4_p2_left_horizontal";
            }
            else if (Input.GetAxis("xbox_p1_left_vertical") != 0 || Input.GetButtonDown("xbox_p1_button_a"))
            {
                inputModule.cancelButton = "xbox_p1_button_circle";
                inputModule.submitButton = "xbox_p1_button_x";
                inputModule.verticalAxis = "xbox_p1_left_vertical";
                inputModule.horizontalAxis = "xbox_p1_left_horizontal";
            }
            else if (Input.GetAxis("xbox_p2_left_vertical") != 0 || Input.GetButtonDown("xbox_p1_button_a"))
            {
                inputModule.cancelButton = "xbox_p2_button_x";
                inputModule.submitButton = "xbox_p2_button_a";
                inputModule.verticalAxis = "xbox_p2_left_vertical";
                inputModule.horizontalAxis = "xbox_p2_left_horizontal";
            }
    }
    public void ChangeMenuScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Player quit game!");
    }
}
