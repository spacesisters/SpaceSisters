using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturJumpPad : MonoBehaviour
{

    public float jumpPadForce;
    public BoxCollider boxCollider;
    public LayerMask playerLayer;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }


    void Update()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + new Vector3(0, 0.1f, 0), (boxCollider.size / 2), Quaternion.identity, playerLayer);
        foreach (Collider c in colliders)
        {
            if (c.gameObject.CompareTag("Player"))
            {
                c.GetComponent<ArturPlayerController>().moveDirection.y = jumpPadForce * c.GetComponent<ArturPlayerController>().gravityReversed;
            }
        }
    }

    private void OnDrawGizmos()
    {
        boxCollider = GetComponent<BoxCollider>();
        Gizmos.color = Color.white;
        Gizmos.DrawCube(transform.position + new Vector3(0, 0.1f, 0), (boxCollider.size / 2));
    }
}
