using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

#pragma warning disable 618
public class LobbyControl : NetworkBehaviour
{
    public string mainMenuSceneName = "MainMenu";
    public string gameScene = "MultiplayerScene";
    
    public short localPlayerId;
    private readonly Dictionary<int, PlayerInLobby> m_Players = new Dictionary<int, PlayerInLobby>();

    public GameObject[] playerNamePanels;
    public Text[] playerNameTexts;

    public GameObject playerNameInputPanel;
    public InputField playerNameInputField;

    public GameObject startButton;

    public GameObject serverFullMessage;

    public Dictionary<int, PlayerInLobby> GetPlayers()
    {
        return m_Players;
    }

    private void Start()
    {
        playerNameInputField.onEndEdit.AddListener(delegate
        {
            m_Players[localPlayerId].SetName(playerNameInputField.text);
        });

        FindObjectOfType<NetworkManagerHUD>().showGUI = true;
    }

    public void SetPlayerName(short playerId, string newName)
    {
        playerNameTexts[playerId].text = newName;
    }

    public void SetPlayerEnabled(short playerId, bool newIsEnabled)
    {
        Image panelImage = playerNamePanels[playerId].GetComponent<Image>();
        Color labelColor = panelImage.color;
        labelColor.a = newIsEnabled ? 1 : 0.2f;
        panelImage.color = labelColor;

        playerNameTexts[playerId].enabled = newIsEnabled;
    }

    public void ActivatePlayerInputPanel()
    {
        playerNameInputPanel.SetActive(true);
    }

    // Set in inspector
    public void OnStartButtonClick()
    {
        FindObjectOfType<NetworkManager>().ServerChangeScene(gameScene);
    }

    public void OnExitButtonClick()
    {
        SceneManager.LoadSceneAsync(mainMenuSceneName);
    }


    [ClientRpc]
    public void RpcRemovePlayer(short playerId)
    {
        m_Players.Remove(playerId);
        SetPlayerEnabled(playerId, false);
    }

    [TargetRpc]
    public void TargetShowServerFullMessage(NetworkConnection conn)
    {
        serverFullMessage.SetActive(true);
    }
}