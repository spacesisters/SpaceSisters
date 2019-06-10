using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturSimpleCameraScript : MonoBehaviour
{

    public Vector3 offset;
    public string targetTag;
    private Transform target;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }

    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
