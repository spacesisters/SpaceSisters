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

    public static void SetValueOfAnimator(Animator anim, string variable)
    {
        string[] states = new string[] { "isIdle", "isMoving", "isShooting"};

        foreach(string state in states)
        {
            if (state == variable)
            {
                anim.SetBool(state, true);
            }
            else
                anim.SetBool(state, false);
        }
    }

    public static T[] Shuffle<T>(this System.Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
        return array;
    }
}

