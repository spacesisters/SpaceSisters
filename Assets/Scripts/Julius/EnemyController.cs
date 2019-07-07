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
    public LayerMask blockFromLayer;
    // Attributes
    public float energyDrainPerSecond = 10.0f;
    public float energyMax = 100.0f;
    public float fireRate = 1.0f;
    public Transform shotSpawn;
    public GameObject projectile;

    private Vector3[] waypoints;
    private NavMeshAgent agent;
    private int nextWaypoint;
    private Transform player1, player2;
    private Transform target;
    private GameObject eyes;
    private ArturForcefieldController forcefieldController;
    private float nextFire;
    private Transform forcefield;
    private float energy;

    private enum Actions
    {
        WALK,
        BLOCK,
        ATTACK
    }
    private Actions action;

    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1").transform;
        player2 = GameObject.FindGameObjectWithTag("Player2").transform;
        forcefieldController = GetComponent<ArturForcefieldController>();
        forcefieldController.Initialize(-1);
        forcefield = GameObject.Find("Shield").transform;
        energy = energyMax;

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

    List<Vector3> PlayerHits()
    {
        List<Vector3> hits = new List<Vector3>();
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
                    hits.Add(hit.point);
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
        // Default
        action = Actions.WALK;

        // Check for entering projectiles
        Collider[] projectiles = Physics.OverlapSphere(eyes.transform.position, viewingDistance/2, blockFromLayer);
        foreach (Collider c in projectiles)
        {
            //Vector3 direction = transform.position - c.transform.position;
            //float distance = direction.magnitude;
            // TODO: Check direction
            if(energy>0)
                action = Actions.BLOCK;
        }

        if (action == Actions.WALK)
        {
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


            if (distancePlayer <= viewingDistance)
            {
                Vector2 directionToPlayer = target.position - eyes.transform.position;
                float angleToPlayer = Vector2.Angle(directionToPlayer, eyes.transform.forward);
                if (angleToPlayer >= -fov && angleToPlayer <= fov)
                {
                    List<Vector3> hits = PlayerHits();
                    if (hits.Count > 0)
                    {
                        /*
                         * TODO: Enemy Attacks
                         */
                        //agent.destination = target.position;
                        action = Actions.ATTACK;
                        agent.isStopped = true;
                        if (Time.time >= nextFire)
                        {
                            GameObject tmp = Instantiate(projectile, shotSpawn.transform.position, shotSpawn.transform.rotation);
                            tmp.layer = 0;
                            Vector2 aimAt = hits[Random.Range(0, hits.Count-1)] - shotSpawn.transform.position; // INFO: Random/Pick from hits
                            tmp.GetComponent<ProjectileController>().setDirection(aimAt);
                            nextFire = Time.time + fireRate;
                        }
                    }
                }
            }
        }

        /*
         * Perform actions
         */
        if (action == Actions.BLOCK)
        {
            forcefieldController.ActivateForcefield();
            energy -= energyDrainPerSecond * Time.deltaTime;
            float forceFieldSize = forcefieldController.radius * (energy / energyMax);
            Debug.Log(forceFieldSize);
            if (forceFieldSize <= 3.0f && forceFieldSize > .0f)
                forceFieldSize = 3.0f;
            else if (forceFieldSize <= .0f)
                forceFieldSize = .0f;
            forcefield.localScale = new Vector3(forceFieldSize, forceFieldSize, forceFieldSize);
        }else if(action == Actions.ATTACK)
        {
            // TODO: Shoot bullet
        }


        // Debugging
        float halfFOV = fov / 2.0f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.back);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.back);
        Vector3 leftRayDirection = leftRayRotation * eyes.transform.forward;
        Vector3 rightRayDirection = rightRayRotation * eyes.transform.forward;
        if (action == Actions.ATTACK)
        {
            Debug.DrawRay(eyes.transform.position, leftRayDirection * viewingDistance, Color.red);
            Debug.DrawRay(eyes.transform.position, rightRayDirection * viewingDistance, Color.red);
        }
        else
        {
            Debug.DrawRay(eyes.transform.position, leftRayDirection * viewingDistance);
            Debug.DrawRay(eyes.transform.position, rightRayDirection * viewingDistance);
        }

        if (action == Actions.WALK)
        {
            agent.isStopped = false;
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }
        
    }
}
