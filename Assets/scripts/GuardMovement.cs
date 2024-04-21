using JetBrains.Annotations;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{

    //Pathfinding
    Transform[] waypoints;
    public Transform pathHolder;
    public Transform targetWaypoint;
    [SerializeField] int targetWaypointIndex = 1;

    public PlayerMovementController playerMovementController;
    public AIDestinationSetter destinationSetter;
    public Path path;
    public AIPath aipath;

    //guard states
    public enum guardStates
    {
        Patrol,
        Chase,
        Investigate
    }

    //guard state at the start
    public guardStates guardState = guardStates.Patrol;


    void Start()
    {
        //Patrolling
        waypoints = new Transform[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i);
        }
        targetWaypoint = waypoints[targetWaypointIndex];


        //Pathfinding
        playerMovementController = GameObject.FindAnyObjectByType<PlayerMovementController>();
        float dan = aipath.path.GetTotalLength();
        
    }


    //guard state update
    private void GuardStateUpdate()
    {
        Debug.Log("Guard Status Update");
        switch (guardState)
        {
            case guardStates.Patrol:
                
                break;
            case guardStates.Chase:
                
                break;
        }
    }



    IEnumerator FollowPath(Vector3[] waypoints)
    {
        yield return null;
    }

    //chasing player loop
    IEnumerator ChasePlayer()
    {
        //chasing
        while (guardState == guardStates.Chase)
        {
            destinationSetter.target = playerMovementController.transform;
            //playerMovementController.transform;
            yield return null;
        }
        destinationSetter.target = pathHolder.GetChild(targetWaypointIndex).transform;


        GuardStateUpdate();
    }

    void Update()
    {
        
    }
}
