using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu";
    public GameObject pauseMenuUi;
    
    public void OnPauseEnter()
    {
        Time.timeScale = 0;
        pauseMenuUi.SetActive(true);
    }

    public void OnPauseExit()
    {
        Time.timeScale = 1;
        pauseMenuUi.SetActive(false);
    }

    public void OnExit()
    {
        SceneManager.LoadSceneAsync(mainMenuSceneName);
        Time.timeScale = 1;
        LapTimer.minutes = 0;
        LapTimer.seconds = 0;
        LapTimer.millis = 0;
        LapTimer.display = "00:00:00";
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
