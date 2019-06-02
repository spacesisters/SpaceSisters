using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturSceneManager : MonoBehaviour
{
    public static float gravity;


    [SerializeField]
    private float _gravity;


    private void Awake()
    {
        gravity = _gravity;
    }
}
