using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artur2GravityReverser : MonoBehaviour
{

    public float maxGravityReverseTime;
    public float reverseDamper;


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float waitTime = maxGravityReverseTime * Mathf.Abs((other.GetComponent<Artur2PlayerController>().GetRigidbody().velocity.y /
                other.GetComponent<Artur2PlayerController>().maximumSpeed)) * 0.5f;

            StartCoroutine(ReverseGravityAfterTime(waitTime, other));
            other.GetComponent<Artur2PlayerController>().GetRigidbody().AddForce(new Vector3(0, reverseDamper * other.GetComponent<Artur2PlayerController>().gravityReverse, 0),
                ForceMode.Force);
        }
    }

    public IEnumerator ReverseGravityAfterTime(float waitTime, Collider other)
    {
        yield return new WaitForSeconds(waitTime);
        print(waitTime);
        other.GetComponent<Artur2PlayerController>().ReverseGravity();       
    }

    
}
