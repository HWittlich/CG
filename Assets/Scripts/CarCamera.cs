using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCamera : MonoBehaviour {

	public GameObject car;
	public float carX;
	public float carY;
	
	// Update is called once per frame
	void Update () {
		var eulerAngles = car.transform.eulerAngles;
		carX = eulerAngles.x;
		carY = eulerAngles.y;
		
		transform.eulerAngles = new Vector3(carX, carY, 0);
	}
}
