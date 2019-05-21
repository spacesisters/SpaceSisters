using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float movementVelocity;
    public float gravity;
    public float jumpVelocity;
    public LayerMask groundLayers;
    

    protected Vector3 bottomCenter, size;

    protected bool isGrounded;
    

    public void SetGrounded()
    {
        if (Physics.OverlapBox(bottomCenter, size / 2, Quaternion.identity, groundLayers).Length > 0)
        {
            isGrounded = true;
        }
        else
            isGrounded = false;
    }


    public void OnDrawGizmos()
    {
        bottomCenter = new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2, transform.position.z);
        size = new Vector3(transform.localScale.x + 1.5f, 0.75f, transform.localScale.z);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bottomCenter, size);
    }

}
