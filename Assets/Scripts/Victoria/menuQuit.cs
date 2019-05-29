using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuQuit : MonoBehaviour
{
   public void doquit()
    {
        Debug.Log("Has quit game");
        Application.Quit();
    }
}
