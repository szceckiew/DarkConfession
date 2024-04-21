using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public float speed = 1;
    public float waitTime = .3f;


    public enum guardStates
    {
        Patrol,
        Chase,
        Investigate
    }

    public guardStates guardState = guardStates.Patrol;


    public Transform pathHolder;

    private void Start()
    {
        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i< waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, waypoints[i].y, 0);
        }

        StartCoroutine(FollowPath(waypoints));

    }

    IEnumerator FollowPath(Vector3[] waypoints)
    {
        transform.position = waypoints[0];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];

        while (guardState == guardStates.Patrol)
        {
            transform.position = Vector3.MoveTowards (transform.position, targetWaypoint, speed * Time.deltaTime);
            if (transform.position ==  targetWaypoint)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds (waitTime);
            }
            yield return null;
        }

    }

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
