using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturGravityReverser : MonoBehaviour
{
    public float reverseTimer;
    public LayerMask playerLayer;


    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ReverseGravity(reverseTimer, c));
        }
    }

    public IEnumerator ReverseGravity(float time, Collider c)
    {
        yield return new WaitForSeconds(time);
        print("Reversing gravity");
        c.GetComponent<ArturPlayerController>().gravityReversed *= -1;
        c.GetComponent<ArturPlayerController>().moveDirection.y *= ArturSceneManager.gravityReverserDamping;
    }
}
