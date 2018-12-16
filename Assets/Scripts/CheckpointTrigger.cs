using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour {

	public GameObject goalTrigger;
	public GameObject checkpointTrigger;

	void OnTriggerEnter(){
		goalTrigger.SetActive (true);
		checkpointTrigger.SetActive (false);
	}
}
