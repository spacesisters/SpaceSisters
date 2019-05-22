using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 20.0f;
    public float effectAmount;

    private MeshRenderer meshRender;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        meshRender = GetComponent<MeshRenderer>();
    }
    
    // Update is called once per frame
    void Update()
    {
        effectAmount = Mathf.Lerp(effectAmount, 0, Time.deltaTime);
        meshRender.material.SetFloat("_Amount", effectAmount);
    }
    public void setDirection(float direction)
    {
        if (rb != null)
        {
            rb.velocity = new Vector3(direction, .0f, .0f) * speed;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Reversable"))
        {
            other.GetComponent<ActionReverseGravity>().Reverse(new Vector3(.0f, 9.8f, .0f));
        }
    }
}
