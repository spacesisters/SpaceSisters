using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject[] waypointsInitial;
    public float viewingDistance = 20.0f;
    public float fov = 65.0f;
    public float minDistancePlayer = 5.0f;

    private Vector3[] waypoints;
    private NavMeshAgent agent;
    private int nextWaypoint;
    private Transform player1, player2;
    private Transform target;
    private GameObject eyes;
    private bool attack = false;
    private ArturForcefieldController forcefieldController;

    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1").transform;
        player2 = GameObject.FindGameObjectWithTag("Player2").transform;
        forcefieldController = GetComponent<ArturForcefieldController>();
        forcefieldController.Initialize(-1);

        eyes = GameObject.Find("Eyes");
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        waypoints = new Vector3[waypointsInitial.Length];
        for(int i=0; i<waypointsInitial.Length; ++i)
        {
            waypoints[i] = waypointsInitial[i].transform.position;
            Destroy(waypointsInitial[i]);
        }
    }

    void GotoNextPoint()
    {
        if (waypoints.Length == 0)
            return;

        agent.destination = waypoints[nextWaypoint];
        nextWaypoint = (nextWaypoint + 1) % waypoints.Length;
    }

    Vector3[] PlayerHits()
    {
        Vector3[] hits = new Vector3[3];
        int hit_counter = 0;
        Vector2[] targets = new Vector2[3];
        targets[0] = target.position - eyes.transform.position;
        targets[1] = target.position + new Vector3(0, target.localScale.y / 2, 0) - eyes.transform.position;
        targets[2] = target.position - new Vector3(0, target.localScale.y / 2, 0) - eyes.transform.position;
        for(int i=0; i<targets.Length; ++i)
        {
            if (Physics.Raycast(eyes.transform.position, targets[i], out RaycastHit hit, viewingDistance))
            {
                if(hit.transform.CompareTag("Player1") || hit.transform.CompareTag("Player2"))
                {
                    hits[hit_counter] = hit.point;
                    ++hit_counter;
                    Debug.DrawLine(eyes.transform.position, hit.point, Color.red);
                }
                else
                {
                    Debug.DrawLine(eyes.transform.position, hit.point);
                }                
            }
        }
        
        return hits;
    }

    void Update()
    {
        attack = false;
        float distancePlayer;
        float distancePlayer1 = Vector3.Distance(player1.position, eyes.transform.position);
        float distancePlayer2 = Vector3.Distance(player2.position, eyes.transform.position);
        if (distancePlayer1 > distancePlayer2)
        {
            target = player2;
            distancePlayer = distancePlayer2;
        }
        else
        {
            target = player1;
            distancePlayer = distancePlayer1;
        }


        forcefieldController.ActivateForcefield();
        if (distancePlayer <= viewingDistance)
        {
            Vector2 directionToPlayer = target.position - eyes.transform.position;
            float angleToPlayer = Vector2.Angle(directionToPlayer, eyes.transform.forward);
            if (angleToPlayer >= -fov && angleToPlayer <= fov)
            {
                Vector3[] hits = PlayerHits();
                if (hits.Length > 0)
                {
                    /*
                     * TODO: Enemy Attacks
                     */
                    //agent.destination = target.position;
                }
            }
        }


        // Debugging
        float halfFOV = fov / 2.0f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.back);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.back);
        Vector3 leftRayDirection = leftRayRotation * eyes.transform.forward;
        Vector3 rightRayDirection = rightRayRotation * eyes.transform.forward;
        if (attack)
        {
            Debug.DrawRay(eyes.transform.position, leftRayDirection * viewingDistance, Color.red);
            Debug.DrawRay(eyes.transform.position, rightRayDirection * viewingDistance, Color.red);
        }
        else
        {
            Debug.DrawRay(eyes.transform.position, leftRayDirection * viewingDistance);
            Debug.DrawRay(eyes.transform.position, rightRayDirection * viewingDistance);
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
}
