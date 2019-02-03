using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


#pragma warning disable 618
public class CgRacerNetworkManager : NetworkManager
{
    public string multiplayerLobbySceneName = "MultiplayerLobby";

    public GameObject playerInLobbyPrefab;
    public GameObject carPrefab;

    private short m_NextPlayer = 0;
    private GameObject[] m_Spawns;

    public Dictionary<short, NetworkConnection>
        playerIdToClientConnection = new Dictionary<short, NetworkConnection>();

    public Dictionary<short, string> playerIdToName = new Dictionary<short, string>();

    private GameObject[] GetSpawns()
    {
        if (m_Spawns == null)
        {
            m_Spawns = new[]
            {
                GameObject.Find("SpawnP1"),
                GameObject.Find("SpawnP2"),
                GameObject.Find("SpawnP3"),
                GameObject.Find("SpawnP4"),
            };
        }

        return m_Spawns;
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (m_NextPlayer > 3)
        {
            StartCoroutine(SendShowServerFullMessage(conn));
            return;
        }

        playerIdToClientConnection.Add(m_NextPlayer++, conn);

        while (playerIdToClientConnection.ContainsKey(m_NextPlayer))
            m_NextPlayer++;
    }

    private IEnumerator SendShowServerFullMessage(NetworkConnection conn)
    {
        yield return new WaitForSeconds(1.0f);

        var lobbyControl = FindObjectOfType<LobbyControl>();
        lobbyControl.TargetShowServerFullMessage(conn);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        ClientScene.Ready(conn);
        ClientScene.AddPlayer(0);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (!playerIdToClientConnection.ContainsValue(conn))
            return;

        var playerId = playerIdToClientConnection.First(kv => kv.Value == conn).Key;

        playerIdToClientConnection.Remove(playerId);

        playerIdToName.Remove(playerId);

        m_NextPlayer = playerId;

        FindObjectOfType<LobbyControl>().RpcRemovePlayer(playerId);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        ClientScene.Ready(conn);
        ClientScene.AddPlayer(0);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (!playerIdToClientConnection.ContainsValue(conn))
            return;

        if (SceneManager.GetActiveScene().name == multiplayerLobbySceneName)
        {
            var newPlayer = Instantiate(playerInLobbyPrefab, Vector3.zero, Quaternion.identity);
            var newPlayerInLobby = newPlayer.GetComponent<PlayerInLobby>();

            newPlayerInLobby.playerId =
                playerIdToClientConnection.First(kv => kv.Value == conn).Key;

            newPlayerInLobby.playerName =
                "Player " + (newPlayerInLobby.playerId + 1);

            playerIdToName[newPlayerInLobby.playerId] = newPlayerInLobby.playerName;

            NetworkServer.AddPlayerForConnection(conn, newPlayer, playerControllerId);
        }
        else // MultiplayerScene
        {
            var newCarGameObject = Instantiate(carPrefab, Vector3.zero, Quaternion.identity);

            var newCar = newCarGameObject.GetComponent<NetworkCar>();

            newCar.playerId =
                playerIdToClientConnection.First(kv => kv.Value == conn).Key;

            newCar.playerName = playerIdToName[newCar.playerId];

            newCarGameObject.transform.position = GetSpawns()[newCar.playerId].transform.position;
            newCarGameObject.transform.rotation = GetSpawns()[newCar.playerId].transform.rotation;

            NetworkServer.AddPlayerForConnection(conn, newCarGameObject, playerControllerId);
        }
    }
}