using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuNewGame : MonoBehaviour
{
    public void changeMenuScene(string scenename)
    {
        Application.LoadLevel(scenename);
    }
}
