using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointAI : MonoBehaviour {

    public GameObject currentWaypoint;

    public GameObject wp1;
    public GameObject wp2;
    public GameObject wp3;
    public GameObject wp4;
    public GameObject wp5;
    public GameObject wp6;
    public GameObject wp7;
    public GameObject wp8;
    public GameObject wp9;
    public GameObject wp10;
    public GameObject wp11;
    public GameObject wp12;
    public GameObject wp13;
    public GameObject wp14;
    public GameObject wp15;
    public GameObject wp16;
    public int wpCounter;

	
	// Update is called once per frame
	void Update () {

        switch (wpCounter) {
            case 0:
                currentWaypoint.transform.position = wp1.transform.position;
                break;
            case 1:
                currentWaypoint.transform.position = wp2.transform.position;
                break;
            case 2:
                currentWaypoint.transform.position = wp3.transform.position;
                break;
            case 3:
                currentWaypoint.transform.position = wp4.transform.position;
                break;
            case 4:
                currentWaypoint.transform.position = wp5.transform.position;
                break;
            case 5:
                currentWaypoint.transform.position = wp6.transform.position;
                break;
            case 6:
                currentWaypoint.transform.position = wp7.transform.position;
                break;
            case 7:
                currentWaypoint.transform.position = wp8.transform.position;
                break;
            case 8:
                currentWaypoint.transform.position = wp9.transform.position;
                break;
            case 9:
                currentWaypoint.transform.position = wp10.transform.position;
                break;
            case 10:
                currentWaypoint.transform.position = wp11.transform.position;
                break;
            case 11:
                currentWaypoint.transform.position = wp12.transform.position;
                break;
            case 12:
                currentWaypoint.transform.position = wp13.transform.position;
                break;
            case 13:
                currentWaypoint.transform.position = wp14.transform.position;
                break;
            case 14:
                currentWaypoint.transform.position = wp15.transform.position;
                break;
            case 15:
                currentWaypoint.transform.position = wp16.transform.position;
                break;

        }
        
		
	}

    IEnumerator OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "RivalCar") {
            this.GetComponent<BoxCollider>().enabled = false;
            wpCounter += 1;
            if (wpCounter == 16) { wpCounter = 0; };
        }
        yield return new WaitForSeconds(1);
        this.GetComponent<BoxCollider>().enabled = true;
    }
}
