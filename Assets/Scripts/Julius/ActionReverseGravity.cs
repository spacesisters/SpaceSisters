using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReverseGravity : MonoBehaviour
{
    public float waitFor = 5.0f;

    private float timePassed;
    private bool isReversed = false;
    private Vector3 previousForce;
    private bool savedForceState = false;
    void Start()
    {
        gameObject.tag = "Reversable";
    }

    void Update()
    {
        if (isReversed)
        {
            timePassed += Time.deltaTime;
            if(timePassed >= waitFor)
            {
                isReversed = false;
                timePassed = .0f;
                Reverse();
            }
        }
    }

    private ConstantForce GetGravity()
    {
        ConstantForce gravity = gameObject.GetComponent<ConstantForce>();
        if (gravity == null)
        {
            gravity = gameObject.AddComponent<ConstantForce>();
            previousForce = Physics.gravity;
            savedForceState = true;
            return gravity;
        }
        if (!savedForceState)
        {
            previousForce = gravity.force;
            savedForceState = true;
        }
        return gravity;
    }

    public void Reverse(Vector3 force)
    {
        isReversed = true;
        ConstantForce gravity = GetGravity();
        gravity.force = force;

        Rigidbody rb2 = gameObject.GetComponent<Rigidbody>();
        rb2.useGravity = false;
        //rb2.AddForce(new Vector3(.0f, .0f, .0f));
    }

    public void Reverse()
    {
        ConstantForce gravity = GetGravity();
        gravity.force = previousForce;
    }
}
