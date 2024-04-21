using JetBrains.Annotations;
using Pathfinding;
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

    }

    //guard state update
    private void GuardStateUpdate()
    {
        Debug.Log("Guard Status Update");
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



        if (distanceToTarget < 1)
        {
            GuardStateUpdate();
        }
        
    }

    //Gizmos drawing
    private void OnDrawGizmos()
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
    }
}
