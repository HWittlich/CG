using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class Tachometer : MonoBehaviour
{
    public CarController car;
    public TextMesh speedText;
    public Camera spectator;

    private void Update()
    {
        speedText.text = String.Format("{0,4:D} km/h", (int) Math.Floor(car.CurrentSpeed));
        var position = spectator.transform.position;
        speedText.transform.LookAt(position);
        //speedText.transform
        //var y = 90 - speedText.transform.rotation.eulerAngles.y;
        //speedText.transform.Rotate(new Vector3(0, y, 0), Space.World);
        speedText.transform.Rotate(new Vector3(0, 180, 0));
        var curRot = speedText.transform.rotation.eulerAngles.x;

        speedText.transform.Rotate(new Vector3(-curRot, 0, 0));
    }
}