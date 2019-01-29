using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Vehicles.Car;

namespace DefaultNamespace
{
    public class LocalCar : NetworkBehaviour
    {
        public GameObject cameraCube;
        public GameObject tachoMeter;
        
        private void Start()
        {
            if (isLocalPlayer)
            {
                tachoMeter.SetActive(true);
                cameraCube.SetActive(true);
                GetComponent<CarController>().enabled = true;
                GetComponent<CarUserControl>().enabled = true;
            }
        }
    }
}