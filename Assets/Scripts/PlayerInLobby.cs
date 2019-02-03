using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 618
public class PlayerInLobby : NetworkBehaviour
{
    private LobbyControl m_LobbyControl;

    public short playerId;

    public string playerName;

    private LobbyControl GetLobbyUiControl()
    {
        if (m_LobbyControl == null)
        {
            m_LobbyControl = FindObjectOfType<LobbyControl>();
        }

        return m_LobbyControl;
    }

    public override void OnStartLocalPlayer()
    {
        CmdRequestPlayerInformation();
    }

    [Command]
    public void CmdRequestPlayerInformation()
    {
        RpcSetPlayerInformation(playerId, playerName); // To Clients

        SetPlayerInformation(playerId, playerName); // To Server

        var playersInLobby = GetLobbyUiControl().GetPlayers();

        foreach (var player in playersInLobby.Values)
            player.TargetSetPlayerInformation(connectionToClient, player.playerId, player.playerName);

        playersInLobby.Add(playerId, this);
    }

    [TargetRpc]
    public void TargetSetPlayerInformation(NetworkConnection conn, short newPlayerId, string newPlayerName)
    {
        SetPlayerInformation(newPlayerId, newPlayerName);
    }

    [ClientRpc]
    public void RpcSetPlayerInformation(short newPlayerId, string newPlayerName)
    {
        SetPlayerInformation(newPlayerId, newPlayerName);
    }

    private void SetPlayerInformation(short newPlayerId, string newPlayerName)
    {
        playerId = newPlayerId;
        playerName = newPlayerName;


        GetLobbyUiControl().SetPlayerName(playerId, playerName);
        GetLobbyUiControl().SetPlayerEnabled(playerId, true);

        GetLobbyUiControl().ActivatePlayerInputPanel();

        if (isServer)
            GetLobbyUiControl().startButton.SetActive(true);
        else
            GetLobbyUiControl().GetPlayers().Add(playerId, this);

        if (isLocalPlayer)
        {
            GetLobbyUiControl().localPlayerId = playerId;
        }
    }

    public void SetName(string newName)
    {
        //playerName = _name;
        CmdSetName(newName);
    }

    [Command]
    public void CmdSetName(string newPlayerName)
    {
        FindObjectOfType<CgRacerNetworkManager>().playerIdToName[playerId] = playerName = newPlayerName;
        RpcReceiveNewName(newPlayerName);
    }

    [ClientRpc]
    public void RpcReceiveNewName(string newName)
    {
        playerName = newName;
        GetLobbyUiControl().SetPlayerName(playerId, newName);
    }

    [TargetRpc]
    public void TargetRpcAddCar(NetworkConnection connect)
    {
        Debug.Log("Spawning Player (in PlayerInLobby");
        ClientScene.AddPlayer(connect, 0);
    }
}