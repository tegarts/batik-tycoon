using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager2 : MonoBehaviour
{
    [SerializeField] GameObject panelPause;
    [SerializeField] GameObject panelOptions;

    private void Start() 
    {
        Time.timeScale = 1;
        panelPause.SetActive(false);
        panelOptions.SetActive(false);
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // TODO - Tambahin pengecekan kalo ada panel lain lagi buka (contoh panel upgrade)
            if(!panelPause.activeSelf)
            {
                panelPause.SetActive(true);
                Time.timeScale = 0;
            }
            else if(panelPause.activeSelf && !panelOptions.activeSelf)
            {
                panelPause.SetActive(false);
                Time.timeScale = 1;
            }
            
        }    
    }

    public void PauseBack()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1;
    }

    public void OptionsButton()
    {
        panelOptions.SetActive(true);
    }

    public void OptionsBack()
    {
        panelOptions.SetActive(false);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    
}
