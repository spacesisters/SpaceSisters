using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturSceneManager : MonoBehaviour
{
    public static float gravity;
    public static float maxAirVelocity;
    public static float maxGroundVelocity;
    public static float gravityReverserDamping;


    [SerializeField]
    private float _gravity;
    [SerializeField]
    private float _maxAirVelocity;
    [SerializeField]
    private float _maxGroundVelocity;
    [SerializeField]
    private float _gravityReverserDamping;



    private void Awake()
    {
        ArturSceneManager.gravity = _gravity;
        ArturSceneManager.maxAirVelocity = _maxAirVelocity;
        ArturSceneManager.gravityReverserDamping = _gravityReverserDamping;
        ArturSceneManager.maxGroundVelocity = _maxGroundVelocity;
    }
}
