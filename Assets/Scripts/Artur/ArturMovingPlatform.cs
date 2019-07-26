using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturMovingPlatform : MonoBehaviour
{
    public Transform thisPlatform;
    public float smoothTime;
    public float resetTime;

    public List<Vector3> positions;

    private int currentTarget;
    private Vector3 nextPosition;

    private Vector3 currentVelocity;

    private void Start()
    {
        currentTarget = 0;
        ChangeTarget();
    }

    private void Update()
    {
        //thisPlatform.position = Vector3.Lerp(thisPlatform.position, nextPosition, smoothTime * Time.deltaTime);
        thisPlatform.position = Vector3.SmoothDamp(thisPlatform.position, nextPosition, ref currentVelocity, smoothTime);

    }

    private void ChangeTarget()
    {

        nextPosition = positions[currentTarget];
        currentTarget++;
        if (currentTarget >= positions.Count)
        {
            currentTarget = 0;
        }
        
        Invoke("ChangeTarget", resetTime);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            collision.collider.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            collision.collider.transform.SetParent(null);
        }
    }
    
}
