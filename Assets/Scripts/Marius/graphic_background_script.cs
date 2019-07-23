using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graphic_background_script : MonoBehaviour
{
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 new_position = transform.position;
        Vector3 camera_position = camera.transform.position;
        new_position.x = camera_position.x;
        new_position.y = camera_position.y;
        new_position.z = camera_position.z + 2000;
        transform.position = new_position;
    }
}
