using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapTimer : MonoBehaviour {

	public static int minutes;
	public static int seconds;
	public static float millis;
	public static string display;

	public GameObject timeBox;
	// Update is called once per frame
	void Update () {
		millis += Time.deltaTime * 10;
		if(millis>=10){
			millis = 0;
			seconds += 1;
		}
		if(seconds>=60){
			minutes += 1;
			seconds = 0;
		}
		if (minutes < 10) {display = "0" + minutes + ":";}
		else{display = minutes + ":";}
		if (seconds < 10) {display += "0";}
		display += seconds + ".";
		display +=System.Math.Floor(millis).ToString("F0");

		timeBox.GetComponent<Text> ().text = display;
	}
}
