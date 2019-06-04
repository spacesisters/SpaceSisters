using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturJumpPadScript : MonoBehaviour
{
    public float jumpForce;
    public LayerMask playerLayer;

    private BoxCollider boxCollider;
    private Vector3 size;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        Vector3 size = boxCollider.size;

        size.y *= 0.5f;
    }


    void FixedUpdate()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y + (boxCollider.size.y / 2), transform.position.z);
        Collider[] colliders = Physics.OverlapBox(position, size / 2, Quaternion.identity, playerLayer);
        foreach(Collider c in colliders)
        {
            //c.GetComponent<Rigidbody>().AddForce(c.transform.up * jumpForce, ForceMode.VelocityChange);
            print(c.transform.up);
            c.GetComponent<Rigidbody>().velocity = new Vector3(c.GetComponent<Rigidbody>().velocity.x, jumpForce * c.transform.up.y, c.GetComponent<Rigidbody>().velocity.z);

        }
    }
}
