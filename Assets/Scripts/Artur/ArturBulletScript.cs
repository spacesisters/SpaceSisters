using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturBulletScript : MonoBehaviour
{
    public float bulletVelocity;
    public float targetRadius;
    public float targetVelocity;
    public LayerMask damagable;
    public LayerMask autoAim;
    
    private Rigidbody rBody;
    private SphereCollider sphereCollider;

    void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        rBody.velocity = transform.right * bulletVelocity;
    }

    private void Update()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, targetRadius, autoAim);
        Vector3 direction = Vector3.zero;
        float distance = float.MaxValue;
        Collider target = null;
        foreach (Collider c in targets)
        {
            direction = transform.position - c.transform.position;
            if (direction.magnitude < distance)
            {
                distance = direction.magnitude;
                target = c;
                direction = transform.position - c.transform.position;
            }
        }

        if (target != null)
        {
            //rBody.AddForce(direction.normalized * targetVelocity, ForceMode.VelocityChange);
            rBody.velocity = - direction.normalized * targetVelocity;
        }


        Collider[] colliders = Physics.OverlapSphere(transform.position, sphereCollider.radius * 0.525f, damagable);
        // TODO: Apply damage.
    }

    private void OnDrawGizmos()
    {
        sphereCollider = GetComponent<SphereCollider>();
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, sphereCollider.radius * 0.525f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3);

    }
}
