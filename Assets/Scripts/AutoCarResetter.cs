using System;
using System.Linq;
using UnityEngine;

public class AutoCarResetter : MonoBehaviour
{
    public uint movementThreshold = 2;
    public uint timeThreshold = 5;

    private Vector3 lastPosition;
    private int timerStart = -1;

    private void Start()
    {
        lastPosition = gameObject.transform.position;
    }

    public void ResetToNearestWaypoint()
    {
        var waypoints = FindObjectOfType<waypointAI>().waypoints;

        var carPosition = gameObject.transform.position;

        var targetTransform = waypoints
            .Select(wp =>
            {
                Vector3 waypointPosition = wp.gameObject.transform.position;
                return new WaypointAndDistance(
                    wp,
                    Math.Abs(waypointPosition.x - carPosition.x)
                    + Math.Abs(waypointPosition.z - carPosition.z)
                );
            })
            .Aggregate((a, b) => a.distance < b.distance ? a : b)
            .waypoint.transform;

        carPosition.x = targetTransform.position.x;
        carPosition.z = targetTransform.position.z;
        carPosition.y = 12;

        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        gameObject.transform.position = carPosition;

        var transformRotation = targetTransform.rotation.eulerAngles;
        transformRotation.x = 0;
        transformRotation.z = 0;
        gameObject.transform.rotation = Quaternion.Euler(transformRotation);
    }


    private void Update()
    {
        var deltaVector3 = lastPosition - gameObject.transform.position;
        var delta = Math.Abs(deltaVector3.x) + Math.Abs(deltaVector3.z);

        if (delta > movementThreshold)
        {
            timerStart = -1;
            lastPosition = gameObject.transform.position;
        }
        else
        {
            var now = (int)
                (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

            if (timerStart == -1)
                timerStart = now;
            else if (now - timerStart > timeThreshold)
                ResetToNearestWaypoint();
        }
    }
}