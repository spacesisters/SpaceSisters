using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class ArturHelper
{
    public static void AccelerateTo(this Rigidbody body, Vector3 targetVelocity, float maxAccel)
    {
        Vector3 deltaV = targetVelocity - body.velocity;
        Vector3 accel = deltaV / Time.deltaTime;

        if (accel.sqrMagnitude > maxAccel * maxAccel)
            accel = accel.normalized * maxAccel;

        body.AddForce(accel, ForceMode.Acceleration);
    }
}

