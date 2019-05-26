using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturShootController : MonoBehaviour
{

    public Transform firePosition;
    public GameObject bullet;
    public GameObject player;
    public float maxBeamLength;

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    void Update()
    {

        float horizontal = Input.GetAxisRaw("RightJoystickHorizontal");
        float vertical = Input.GetAxisRaw("RightJoystickVertical");
        player = GameObject.FindGameObjectWithTag("Player");

        firePosition.transform.position = player.transform.position + new Vector3(horizontal,
            vertical, 0f);


        if (Input.GetButtonDown("RB"))
        {
            Shoot();
        }
        if (Input.GetButtonDown("LB"))
        {
            Vector3 direction = new Vector3(horizontal, vertical, 0f);
            RaycastHit hit;
            Physics.Raycast(player.transform.position, direction, out hit, 100f);
            lineRenderer.SetPosition(0, player.transform.position);
            Vector3 endpoint = Vector3.zero;
            if (hit.point == Vector3.zero)
            {
                if (player.transform.rotation.y == 0)
                {
                    endpoint.x = maxBeamLength;
                }
                else if (player.transform.rotation.y == 1)
                {
                    endpoint.x = -maxBeamLength;
                }
            }
            else
            {
                endpoint = hit.point;
                
            }

            lineRenderer.SetPosition(1, endpoint);
            lineRenderer.enabled = true;
        }
        else if (Input.GetButtonUp("LB"))
        {
            lineRenderer.enabled = false;
        }

    }

    private void Shoot()
    {
        Instantiate(bullet, firePosition.position, firePosition.rotation);
    }

   
}
