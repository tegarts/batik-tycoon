using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject popUpLoad;
    private void Start() 
    {
        popUpLoad.SetActive(false);
    }

    public void ButtonStart()
    {
        if(DataPersistenceManager.instance.HasGameData())
        {
            popUpLoad.SetActive(true);
        }
        else
        {
            DataPersistenceManager.instance.NewGame();
            SceneManager.LoadScene("New Mechanic"); // TODO - Ganti nama scene
        }
    }

    public void ButtonLoadYes()
    {
        SceneManager.LoadScene("New Mechanic"); // TODO - Ganti nama scene
    }

    public void ButtonLoadNo()
    {
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadScene("New Mechanic"); // TODO - Ganti nama scene
    }

    public void ButtonLoadExit()
    {
        popUpLoad.SetActive(false);
    }
}
