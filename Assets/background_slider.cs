﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_slider : MonoBehaviour
{
    public Vector2 offset;
    public Material mat;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        offset.x += 0.0005f;
        offset.y = Mathf.Sin(Time.time) * 0.05f;
        mat.SetVector("_offset", offset);
        //mat.mainTextureOffset = offset;
        //print(mat.mainTextureOffset);
        //print(mat.GetTextureOffset("_MainTex"));
    }
}
