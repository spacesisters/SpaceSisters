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
        {
            isGrounded = false;
        }
    }
}