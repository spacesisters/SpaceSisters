using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graphic_camera_script : MonoBehaviour
{
    float speed = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 new_position = transform.position;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            new_position.x -= speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            new_position.x += speed;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            new_position.y += speed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            new_position.y -= speed;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            new_position.z += speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            new_position.z -= speed;
        }

        transform.position = new_position;
    }
}
