using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Vehicles.Car;

#pragma warning disable 618
public class NetworkCar : NetworkBehaviour
{
    public GameObject cameraCube;
    public GameObject tachometer;
    public short playerId = -1;
    public string playerName;

    public override void OnStartLocalPlayer()
    {
        CmdRequestCarInformation();
    }

    [Command]
    public void CmdRequestCarInformation()
    {
        RpcSetCarInformation(playerId, playerName); // Update on all clients
        SetCarInformation(playerId, playerName); // Server doesn't get ClientRpcs

        foreach (var car in FindObjectsOfType<NetworkCar>())
            // Update other cars for this player
            car.TargetSetCarInformation(connectionToClient, car.playerId, car.playerName);
    }

    [TargetRpc]
    public void TargetSetCarInformation(NetworkConnection conn, short newPlayerId, string newPlayerName)
    {
        SetCarInformation(newPlayerId, newPlayerName);
    }

    [ClientRpc]
    public void RpcSetCarInformation(short newPlayerId, string newPlayerName)
    {
        SetCarInformation(newPlayerId, newPlayerName);
    }

    private void SetCarInformation(short newPlayerId, string newPlayerName)
    {
        playerId = newPlayerId;
        playerName = newPlayerName;

        if (isLocalPlayer)
        {
            tachometer.SetActive(true);
            cameraCube.SetActive(true);

            GetComponent<CarController>().enabled = true;
            GetComponent<CarUserControl>().enabled = true;

            GetComponentInChildren<PlayerNameDisplay>(true).SetSpectator(cameraCube.GetComponentInChildren<Camera>());

            foreach (var car in FindObjectsOfType<NetworkCar>())
            {
                if (car != this)
                {
                    car.GetComponentInChildren<PlayerNameDisplay>(true).gameObject.SetActive(true);
                }
            }
        }
        else
        {
            var playerNameDisplay = GetComponentInChildren<PlayerNameDisplay>(true);


            if (playerNameDisplay.GetSpectator() != null)
            {
                // This player was set up after the LocalCar, need to activate the PlayerNameDisplay
                playerNameDisplay.gameObject.SetActive(true);
            }
        }
    }
}