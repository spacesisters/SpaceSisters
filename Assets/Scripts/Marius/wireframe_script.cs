using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wireframe_script : MonoBehaviour
{
    public Camera camera;
    public Material wireframeMat;

    private float time_offset;
    public float yChange = 0.01f;
    public float yOffset = -3.0f;
    private float epsilon = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        time_offset = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 new_position = transform.position;
        Vector3 cameraPos = camera.transform.position;
        wireframeMat.SetVector("_offset", new Vector2(cameraPos.x * 0.5f + time_offset, 0.0f));

        new_position.x = cameraPos.x;
        if ((cameraPos.y + yOffset) > (new_position.y + epsilon))
        {
            new_position.y += yChange;
        }
        else if ((cameraPos.y + yOffset) < (new_position.y - epsilon))
        {
            new_position.y -= yChange;
        }
        transform.position = new_position;
    }

    private void FixedUpdate()
    {
        time_offset += 0.01f;
    }
}
