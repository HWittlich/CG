﻿using System;
using System.Linq;
using UnityEngine;

public partial class CarResetter : MonoBehaviour
{
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetToNearestWaypoint();
        }
    }
}