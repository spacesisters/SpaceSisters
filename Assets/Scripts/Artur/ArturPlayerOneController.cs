using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturPlayerOneController : ArturBasePlayerController
{
    public float shootCooldown;

    private float shootTimer;
    private ArturForcefieldController forcefieldController;
    private ArturGunScript gunScript;
    private bool doShoot;

    private void Start()
    {
        forcefieldController = transform.GetChild(0).GetComponent<ArturForcefieldController>();
        gunScript = GetComponent<ArturGunScript>();
        forcefieldController.Initialize(1);
        base.Initialize();
    }

    private new void Update()
    {
        base.Update();
        TranslateInput();
        if (doForcefield)
        {
            forcefieldController.ActivateForcefield();
            energy -= energyDrainPerSecond * Time.deltaTime;
        }

        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
        else if (shootTimer < 0)
        {
            shootTimer = 0;
        }
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        if (doShoot)
        {
            gunScript.Shoot();
            shootTimer = shootCooldown;
        }
    }

    private new void TranslateInput()
    {
        base.TranslateInput();
        if (playerInput.specialButton && shootTimer == 0)
        {
            doShoot = true;
        }
        else
            doShoot = false;
    }

}
