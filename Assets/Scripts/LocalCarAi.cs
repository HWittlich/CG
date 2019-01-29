using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Vehicles.Car;

namespace DefaultNamespace
{
    public class LocalCarAi : NetworkBehaviour
    {
        private void Start()
        {
            if (isServer)
            {
                GetComponent<CarController>().enabled = true;
                GetComponent<CarAIControl>().enabled = true;
            }
        }
    }
}