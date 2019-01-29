using DefaultNamespace;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 618
public class CarNetworkManager : NetworkManager

{
    public Transform[] spawnPoints;
    public GameObject checkpoint;
        
    private int m_NextSpawn = 0;
    
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        var spawnPoint = spawnPoints[m_NextSpawn++];
        
        GameObject newCar = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        newCar.transform.rotation = spawnPoint.transform.rotation;
        NetworkServer.AddPlayerForConnection(conn, newCar, playerControllerId);
    }
}
#pragma warning restore 618