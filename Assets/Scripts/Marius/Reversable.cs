using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reversable : MonoBehaviour
{
    private Rigidbody rb;
    public bool isReversed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(isReversed)
        {
            rb.AddForce(new Vector3(0, 19.6f, 0));
        }
    }

    public void ReserveGravitiy()
    {
        isReversed = !isReversed;
    }
}
