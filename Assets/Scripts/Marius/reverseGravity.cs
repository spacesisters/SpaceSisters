﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reverseGravity : MonoBehaviour
{
    private Rigidbody rb;
    private float gForce = -9.81f;
    public bool isReversed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (isReversed)
        {
            gForce *= -1.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector3(0.0f, gForce, 0.0f), ForceMode.Acceleration);
    }

    public void reverseG()
    {
        gForce *= -1.0f;
    }
}
