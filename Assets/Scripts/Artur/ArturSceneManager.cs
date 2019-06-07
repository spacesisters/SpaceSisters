using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturSceneManager : MonoBehaviour
{
    public static float gravity;
    public static string currentLevel;

    [SerializeField]
    private float _gravity;

    [SerializeField]
    private string _currentLevel;


    private void Awake()
    {
        gravity = _gravity;
        currentLevel = _currentLevel;
    }
}
