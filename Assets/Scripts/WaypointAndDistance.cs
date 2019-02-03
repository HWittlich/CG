using UnityEngine;

internal struct WaypointAndDistance
{
    public GameObject waypoint;
    public float distance;

    public WaypointAndDistance(GameObject waypoint, float distance) : this()
    {
        this.waypoint = waypoint;
        this.distance = distance;
    }
}