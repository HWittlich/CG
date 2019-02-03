using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerPauseMenu : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu";
    public GameObject pauseMenuUi;

    public void OnPauseEnter()
    {
        pauseMenuUi.SetActive(true);
    }

    public void OnPauseExit()
    {
        pauseMenuUi.SetActive(false);
    }

    public void OnExit()
    {
        SceneManager.LoadSceneAsync(mainMenuSceneName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuUi.activeSelf)
            {
                OnPauseExit();
            }
            else
            {
                OnPauseEnter();
            }
        }
    }
}
