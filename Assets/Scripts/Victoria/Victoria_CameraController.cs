using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victoria_CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    // LateUpdate is called after Update each frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
}
