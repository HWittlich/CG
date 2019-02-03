using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

#pragma warning disable 618
public class MainMenuScript : MonoBehaviour
{
    public string singleplayerSceneName = "SingleplayerScene";
    public string multiplayerSceneName = "MultiplayerLobby";

    private void Start()
    {
        var nm = FindObjectOfType<CgRacerNetworkManager>();

        if (nm != null)
        {
            Destroy(FindObjectOfType<NetworkManagerHUD>());
            Destroy(nm);
            NetworkManager.Shutdown();
        }
    }

    public void OnSingleplayerStart()
    {
        SceneManager.LoadSceneAsync(singleplayerSceneName);
    }

    public void OnMultiplayerStart()
    {
        SceneManager.LoadSceneAsync(multiplayerSceneName);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}