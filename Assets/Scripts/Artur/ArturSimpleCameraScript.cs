using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturSimpleCameraScript : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
