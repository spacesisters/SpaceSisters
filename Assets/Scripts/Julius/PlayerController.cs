using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Julius
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 10.0f;
        public float jumpSpeed = 8.0f;
        public GameObject bullet;
        public Transform shotSpawn;

        private Rigidbody rb;
        private Collider playerCollider;
        private float distanceToGround;
        private float fireRate = 1.0f;
        private float nextFire;
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            playerCollider = GetComponent<Collider>();
            distanceToGround = playerCollider.bounds.extents.y;
        }

        void Update()
        {
            
            if (Time.time >= nextFire && Input.GetButtonDown("Fire1"))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition-shotSpawn.position);
                Debug.Log(ray);
                Debug.Log(mousePosition);
                Debug.Log(transform.position);
                Vector3 newDir = new Vector3(ray.direction.x, ray.direction.y, .0f);
                Debug.Log(newDir);
                GameObject tmp = Instantiate(bullet, shotSpawn.position, shotSpawn.rotation);
                tmp.GetComponent<ProjectileController>().setDirection(newDir);
                nextFire = Time.time + fireRate;
               
            }
        }

        void FixedUpdate()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            //float moveVertical = Input.GetAxis("Vertical");
        
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
            if (IsGrounded() && Input.GetButton("Jump"))
            {
                movement.y = jumpSpeed;
            }
            rb.AddForce(movement * speed, ForceMode.Acceleration);
        }

        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
        }
    }
}
