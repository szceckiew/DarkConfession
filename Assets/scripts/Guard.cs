using JetBrains.Annotations;
using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    //Patrolling
    public float speed = 1;
    public float waitTime = .3f;
    public Transform pathHolder;
    [SerializeField] public Transform[] waypoints;
    Transform targetWaypoint;
    [SerializeField] int targetWaypointIndex = 1;
    [SerializeField] float distanceToTarget = 10;
    public AIPath aipath;


    //Pathfinding
    public PlayerMovementController playerMovementController;
    public AIDestinationSetter destinationSetter;


    //Player detection
    public bool PlayerDetected { get; private set; }
    public Vector2 DirectionToTarget => target.transform.position - detectorOrigin.position;
    private Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.one;
    public Vector2 detectorOriginOffset = Vector2.zero;

    public float detectorRadius = 2;

    public float detectionDelay = 0.3f;
    public LayerMask detectorLayerMask;
    public LayerMask visionLayerMask;

    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectedColor = Color.red;

    public Collider2D playerCollider;

    private GameObject target;

    public GameObject Target
    {
        get => target;
        private set
        {
            target = value;
            PlayerDetected = target != null;
        }
    }

    private bool showGizmos = true;

    //guard states
    public enum guardStates
    {
        Patrol,
        Chase,
        Investigate
    }

    //guard state at the start
    public guardStates guardState = guardStates.Patrol;




    private void Start()
    {


        //Patrolling
        waypoints = new Transform[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).transform;
            
        }
        targetWaypoint = waypoints[targetWaypointIndex];

        //Pathfinding
        playerMovementController = GameObject.FindAnyObjectByType<PlayerMovementController>();

        
        //Start default behaviour
        StartCoroutine(FollowPath(waypoints));

        detectorOrigin = transform;

        StartCoroutine(DetectionCoroutine());

        detectorLayerMask |= (1 << 3);
        detectorSize = new Vector2(5, 5);

        visionLayerMask |= (1 << 3);
        visionLayerMask |= (1 << 7);
        visionLayerMask |= (1 << 13);
    }

    //guard state update
    private void GuardStateUpdate()
    {
        Debug.Log("Guard Status Update: " + guardState);
        switch (guardState)
        {
            case guardStates.Patrol:
                StartCoroutine(FollowPath(waypoints));
                break;
            case guardStates.Chase:
                StartCoroutine(ChasePlayer());
                break;
        }
    }

    


    //patrolling loop
    IEnumerator FollowPath(Transform[] waypoints)
    {

        //patrolling
        //transform.position = waypoints[0];
        destinationSetter.target = targetWaypoint;

        while (guardState == guardStates.Patrol)
        {
            if (aipath.path != null)
            {
                if (aipath.path.GetTotalLength() <= 0.5)
                {
                    targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                    targetWaypoint = waypoints[targetWaypointIndex];
                    destinationSetter.target = targetWaypoint;
                    yield return new WaitForSeconds(waitTime);
                }
            }


            yield return null;
        }

        GuardStateUpdate();
    }


    //chasing player loop
    IEnumerator ChasePlayer()
    {
        //chasing
        while (guardState == guardStates.Chase)
        {
            destinationSetter.target = playerMovementController.transform;
            if (aipath.path != null)
            {
                distanceToTarget = aipath.path.GetTotalLength();
            }
            //playerMovementController.transform;
            yield return null;
        }
        destinationSetter.target = pathHolder.GetChild(targetWaypointIndex).transform;


        //if (distanceToTarget < 1)
        //{
        //    GuardStateUpdate();
        //}

        GuardStateUpdate();

    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    public void PerformDetection()
    {
        //Collider2D collider = Physics2D.OverlapBox((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize, 0, detectorLayerMask);
        Collider2D collider = Physics2D.OverlapCircle((Vector2)detectorOrigin.position + detectorOriginOffset, detectorRadius, detectorLayerMask);

        RaycastHit2D haveLoSToPlayer = Physics2D.Raycast((Vector2)detectorOrigin.position + detectorOriginOffset, playerMovementController.transform.position, distance: Vector2.Distance(playerMovementController.transform.position, transform.position), layerMask: visionLayerMask);
        if (collider != null && haveLoSToPlayer.collider == playerCollider)
        {
            Target = collider.gameObject;
            guardState = guardStates.Chase;
        }
        else
        {
            Target = null;
            guardState = guardStates.Patrol;
        }

        Debug.Log(haveLoSToPlayer.collider);
        //GuardStateUpdate();
    }

    public void Die()
    {
        //turning off every guard script and collider
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Seeker>().enabled = false;
        GetComponent<AIPath>().enabled = false;
        GetComponent<AIDestinationSetter>().enabled = false;

        this.enabled = false;
    }



    //Gizmos drawing
    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Vector3 startPosition = pathHolder.GetChild(0).position;
            Vector3 previousPosition = startPosition;
            foreach (Transform waypoint in pathHolder)
            {
                Gizmos.DrawSphere(waypoint.position, .1f);
                Gizmos.DrawLine(previousPosition, waypoint.position);
                previousPosition = waypoint.position;
            }
            Gizmos.DrawLine(previousPosition, startPosition);

            if (detectorOrigin != null)
            {
                if (PlayerDetected)
                {
                    Gizmos.color = gizmoDetectedColor;
                }
                //Gizmos.DrawCube((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize);
                Gizmos.DrawSphere((Vector2)detectorOrigin.position + detectorOriginOffset, detectorRadius);
            }
        }
    }
}
