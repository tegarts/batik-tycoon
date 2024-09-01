using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject panelPause;
    DayManager dayManager;
    [SerializeField] Animator pauseAnim;

    private void Start()
    {
        dayManager = FindAnyObjectByType<DayManager>();
        pauseAnim.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    public void PauseGame()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        panelPause.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    public void GameOver()
    {
        Time.timeScale = 1;
        DataPersistenceManager.instance.NewGame();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panelPause.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

    }
}
