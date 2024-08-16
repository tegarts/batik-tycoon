using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject panelAbout;
    [SerializeField] GameObject panelOptions;
    [SerializeField] GameObject panelQuit;
    [SerializeField] GameObject buttonContinue;

    AudioManager audioManager;

    private void Start() 
    {
        // audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
        Time.timeScale = 1;
        panelAbout.SetActive(false);
        panelOptions.SetActive(false);
        panelQuit.SetActive(false);
        buttonContinue.SetActive(false);
        
        if(DataPersistenceManager.instance.HasGameData())
        {
            buttonContinue.SetActive(true);
        }   
    }

    public void StartButton()
    {
        // audioManager.PlaySFX(audioManager.clickedButton);
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadScene("Control");
    }

    public void ContinueButton()
    {
        // audioManager.PlaySFX(audioManager.clickedButton);
        SceneManager.LoadScene("Control");
    }

    public void AboutButton()
    {
        // audioManager.PlaySFX(audioManager.clickedButton);
        panelAbout.SetActive(true);
    }

    public void OptionsButton()
    {
        // audioManager.PlaySFX(audioManager.clickedButton);
        panelOptions.SetActive(true);
    }

    public void AboutClose()
    {
        // audioManager.PlaySFX(audioManager.clickedButton);
        panelAbout.SetActive(false);
    }

    public void OptionsClose()
    {
        // audioManager.PlaySFX(audioManager.clickedButton);
        panelOptions.SetActive(false);
    }

    public void QuitButton()
    {
        // audioManager.PlaySFX(audioManager.clickedButton);
        panelQuit.SetActive(true);
    }

    public void QuitClose()
    {
        // audioManager.PlaySFX(audioManager.clickedButton);
        panelQuit.SetActive(false);
    }
    

    public void ExitButton()
    {
        // audioManager.PlaySFX(audioManager.clickedButton);
        Application.Quit();
    }
}
