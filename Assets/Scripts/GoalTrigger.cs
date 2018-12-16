using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalTrigger : MonoBehaviour {

	public GameObject goalTrigger;
	public GameObject checkpointTrigger;
	public GameObject bestLapDisplay;
	public GameObject currentLapDisplay;

	void OnTriggerEnter(){
		goalTrigger.SetActive (false);
		checkpointTrigger.SetActive (true);
		bestLapDisplay.GetComponent<Text>().text = currentLapDisplay.GetComponent<Text>().text;
		LapTimer.minutes = LapTimer.seconds = 0;
		LapTimer.millis = 0f;
	}
}
