using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturJumpPadScript : MonoBehaviour
{
    public float jumpForce;
    public LayerMask playerLayer;

    private BoxCollider boxCollider;
    private float jumpOverlayRadius = 0.25f;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }


    void FixedUpdate()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y + (boxCollider.size.y / 2), transform.position.z);
        
        Collider[] colliders = Physics.OverlapSphere(position, jumpOverlayRadius, playerLayer);

        foreach (Collider c in colliders)
        {

            c.GetComponent<Rigidbody>().AddForce(jumpForce * c.transform.up, ForceMode.Acceleration);


        }
    }

    /*
    private void OnDrawGizmos()
    {
        boxCollider = GetComponent<BoxCollider>();

        Vector3 position = new Vector3(transform.position.x, transform.position.y + (boxCollider.size.y / 2), transform.position.z);
        Vector3 size = boxCollider.size;

        //size.y *= 0.85f;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(position, jumpOverlayRadius);

    }
    */
}
