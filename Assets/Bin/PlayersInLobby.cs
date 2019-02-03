using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 618
public class PlayersInLobby : NetworkBehaviour
{
/*    public short localPlayerId;

    private LobbyUiControl m_LobbyUiControl;

    private readonly Dictionary<int, PlayerInLobby> m_Players = new Dictionary<int, PlayerInLobby>();

    public LobbyUiControl GetLobbyUiControl()
    {
        if (m_LobbyUiControl == null)
        {
            m_LobbyUiControl = FindObjectOfType<LobbyUiControl>();
        }

        return m_LobbyUiControl;
    }

    public Dictionary<int, PlayerInLobby> GetPlayers()
    {
        return m_Players;
    }

    public void SetOwnName(string ownName)
    {
        GetPlayers()[localPlayerId].SetName(ownName);
    }

    [TargetRpc]
    public void TargetSetLocalPlayerId(NetworkConnection target, short localPlayerIdToSet)
    {
        this.localPlayerId = localPlayerIdToSet;
        GetLobbyUiControl().ActivatePlayerInputPanel();
    }

    [ClientRpc]
    public void RpcSetName(short playerId, string playerName)
    {
        GetLobbyUiControl().SetPlayerName(playerId, playerName);
    }*/
}