using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturBulletScript : MonoBehaviour
{
    public float bulletVelocity;
    public LayerMask damagable;
    
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, sphereCollider.radius * 0.525f, damagable);
        // TODO: Apply damage.
    }

    private void OnDrawGizmos()
    {
        sphereCollider = GetComponent<SphereCollider>();
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, sphereCollider.radius * 0.525f);
    }
}
