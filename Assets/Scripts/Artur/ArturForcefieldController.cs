using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturForcefieldController : MonoBehaviour
{
    public float radius = 5.0f;
    public float radius_updated;
    public float forceMagnitude = 10.0f;

    [SerializeField]
    private LayerMask reactToForcefieldLayer;
    private int fieldType;

    void Awake()
    {
        radius_updated = radius;
    }

    public void Initialize(int fieldType)
    {
        this.fieldType = fieldType;
    }

    public void ActivateForcefield()
    {

        Collider[] objects = Physics.OverlapSphere(transform.position, radius_updated, reactToForcefieldLayer);
        foreach (Collider c in objects)
        {


            Vector3 direction = transform.position - c.transform.position;
            float distance = direction.magnitude;
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddForce(direction.normalized * forceMagnitude * fieldType * (radius_updated - distance), ForceMode.Acceleration); //.Impulse
            }
        }

    }

    private void OnDrawGizmos()
    {   
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius_updated);    
        
    }

}
