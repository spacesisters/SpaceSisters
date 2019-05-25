using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturShootController : MonoBehaviour
{

    public Transform firePosition;
    public Transform player;
    public GameObject bullet;

    void Update()
    {
        firePosition.transform.position = player.transform.position + new Vector3(Input.GetAxis("RightJoystickHorizontal"),
            Input.GetAxis("RightJoystickVertical"), 0f);


        if (Input.GetButtonDown("RB"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, firePosition.position, firePosition.rotation);
    }
}
