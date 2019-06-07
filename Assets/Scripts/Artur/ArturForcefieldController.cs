using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturForcefieldController : MonoBehaviour
{
    public float radius;
    public float forceMagnitude;

    [SerializeField]
    private LayerMask reactToForcefieldLayer;
    private int fieldType;


    public void Initialize(int fieldType)
    {
        this.fieldType = fieldType;
    }

    public void ActivateForcefield()
    {

        Collider[] objects = Physics.OverlapSphere(transform.position, radius, reactToForcefieldLayer);
        foreach (Collider c in objects)
        {

            Vector3 direction = transform.position - c.transform.position;
            float distance = direction.magnitude;
             
            c.GetComponent<Rigidbody>().AddForce(direction.normalized * forceMagnitude * fieldType * (radius - distance), ForceMode.Acceleration);
        }

    }

    private void OnDrawGizmos()
    {
        
        radius = 5;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
       
    }

}
