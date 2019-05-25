using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturBulletScript : MonoBehaviour
{
    public LayerMask destroyLayer;
    public float bulletSpeed;
    public float lifetime;
    public float knockback;
    private CharacterController controller;
    private Vector3 moveDirection;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<CharacterController>();
        float horizontal = Input.GetAxisRaw("RightJoystickHorizontal");
        float vertical = Input.GetAxisRaw("RightJoystickVertical");
        if (horizontal == 0 && vertical == 0)
        {
            if(player.transform.rotation.y == 0)
            {
                horizontal = 1;
            }
            else if(player.transform.rotation.y == 1)
            {
                horizontal = -1;
            }
        }
        
        moveDirection = new Vector3(horizontal, vertical, 0f) * bulletSpeed * Time.deltaTime;

        StartCoroutine(DestroyBullet(lifetime));
    }

    void Update()
    {
        controller.Move(moveDirection);
        Collider[] colliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius, destroyLayer);
        foreach (Collider c in colliders)
        {
            if (c.CompareTag("Enemy"))
            {
                Destroy(gameObject);
                c.GetComponent<ArturEnemyController>().SetHit(true, transform.position, knockback);
            }
        }
        if (colliders.Length > 0)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator DestroyBullet(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }


    

}
