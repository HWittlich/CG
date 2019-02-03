using System;
using System.Linq;
using UnityEngine;

public class CarResetter : MonoBehaviour
{
    struct WaypointAndDistance
    {
        public GameObject waypoint;
        public float distance;

        public WaypointAndDistance(GameObject waypoint, float distance) : this()
        {
            this.waypoint = waypoint;
            this.distance = distance;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            var waypoints = FindObjectOfType<waypointAI>().waypoints;

            var carPosition = gameObject.transform.position;

            var targetPosition = waypoints
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
                .waypoint.transform.position;

            carPosition.x = targetPosition.x;
            carPosition.z = targetPosition.z;
            carPosition.y = 12;

            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

            gameObject.transform.position = carPosition;

            var transformRotation = gameObject.transform.rotation;
            transformRotation.z = 0;
            gameObject.transform.rotation = transformRotation;
        }
    }
}