using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class CarControls : MonoBehaviour {
    public GameObject carControl;

    private void Start()
    {
        carControl.GetComponent<CarController>().enabled = true;
    }
}
