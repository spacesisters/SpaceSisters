using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artur2SceneManager : MonoBehaviour
{
    public static float gravity;

    [SerializeField]
    private float _gravity;

    void Awake()
    {
        gravity = _gravity;
    }


}
