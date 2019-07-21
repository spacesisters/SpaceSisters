using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 20.0f;
    public float effectAmount;
    public int damage = 20;


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
    public void setDirection(Vector3 direction)
    {
        if (rb != null)
        {
            rb.AddForce(direction.normalized * speed, ForceMode.Acceleration);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().health -= damage;
        }
        else if(other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ArturMetaInf>().playerHealth -= damage;
        }
        Destroy(gameObject);

    }
}
