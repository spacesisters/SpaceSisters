using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuChangeScene : MonoBehaviour
{
    public void changeMenuScene(string scenename)
    {
        Application.LoadLevel(scenename);
    }
}
