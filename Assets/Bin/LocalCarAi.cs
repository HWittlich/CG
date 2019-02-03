using UnityEngine.Networking;
using UnityStandardAssets.Vehicles.Car;


#pragma warning disable 618
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