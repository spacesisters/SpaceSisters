﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturGunScript : MonoBehaviour
{

    public GameObject bullet;
    public Transform firePosition;

    public void Shoot()
    {
        Instantiate(bullet, firePosition.position, firePosition.rotation);
    }
}
