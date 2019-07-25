using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundColorScript : MonoBehaviour
{
    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mat.SetVector("_hue", new Vector2(Time.time, 0.0f));
        print(mat.GetVector("_hue"));
    }
}
