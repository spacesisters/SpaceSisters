using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_ : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    void Update()
    {
        transform.position = target.position - offset;
        transform.LookAt(target);
    }
}
