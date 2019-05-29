using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float movementVelocity;
    public float gravity;
    public float jumpVelocity;
    public LayerMask groundLayers;
    protected bool gravityIsReversed;

    protected Vector3 bottomCenter, size, topCenter;

    protected bool isGrounded;



    public void SetGrounded()
    {
        if (gravityIsReversed)
        {
            if (Physics.OverlapBox(topCenter, size / 2, Quaternion.identity, groundLayers).Length > 0)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        } else
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
}