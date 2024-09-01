using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject panelPause;
    DayManager dayManager;
    [SerializeField] Animator pauseAnim;

    [Header("Audio")]
    AudioSetter audioSetter;
    private void Start()
    {
        dayManager = FindAnyObjectByType<DayManager>();
        pauseAnim.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
    private void Awake()
    {
        audioSetter = GameObject.FindWithTag("Audio").GetComponent<AudioSetter>();
    }

    public void PauseGame()
    {
        audioSetter.PlaySFX(audioSetter.OpenPanel);
        panelPause.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        audioSetter.PlaySFX(audioSetter.ClosePanel);
        Time.timeScale = 1;
        panelPause.SetActive(false);
    }

    public void MainMenu()
    {
        audioSetter.PlaySFX(audioSetter.OpenPanel);
        Time.timeScale = 1;
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
