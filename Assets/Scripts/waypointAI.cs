using System.Collections;
using UnityEngine;

public class waypointAI : MonoBehaviour
{
    private string aiControlledCarTag = "TestAi";
    public GameObject targetWaypoint;
    public GameObject[] waypoints;

    private int m_WpCounter = 0;

    IEnumerator OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(aiControlledCarTag))
        {
            GetComponent<BoxCollider>().enabled = false;
            m_WpCounter = (m_WpCounter + 1) % waypoints.Length;
            targetWaypoint.transform.position = waypoints[m_WpCounter].transform.position;
        }

        yield return new WaitForSeconds(1);
        GetComponent<BoxCollider>().enabled = true;
    }
}