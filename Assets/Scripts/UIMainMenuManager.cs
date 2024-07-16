using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenuManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] GameObject panelAbout;
    [SerializeField] GameObject panelOptions;
    [SerializeField] GameObject buttonContinue;
    public void LoadData(GameData data)
    {
    }

    public void SaveData(ref GameData data)
    {
    }

    private void Start() 
    {
        Time.timeScale = 1;
        panelAbout.SetActive(false);
        panelOptions.SetActive(false);
        buttonContinue.SetActive(false);

        if(DataPersistenceManager.instance.HasGameData())
        {
            buttonContinue.SetActive(true);
        }   
    }

    public void StartButton()
    {
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadScene("Control");
    }

    public void AboutButton()
    {
        panelAbout.SetActive(true);
    }

    public void OptionsButton()
    {
        panelOptions.SetActive(true);
    }

    public void AboutClose()
    {
        panelAbout.SetActive(false);
    }

    public void OptionsClose()
    {
        panelOptions.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
