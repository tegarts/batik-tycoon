using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject popUpLoad;
    [SerializeField] GameObject panelMainMenu;
    [SerializeField] GameObject panelOption;
    [SerializeField] GameObject panelInfo;
    [SerializeField] Animator animCamera;
    [SerializeField] Animator animPKanan;
    [SerializeField] Animator animPKiri;
    [SerializeField] Animator animFlash;

    AudioSetter audioSetter;

    private void Awake()
    {
        audioSetter = GameObject.FindWithTag("Audio").GetComponent<AudioSetter>();
    }


    private void Start() 
    {
        panelMainMenu.SetActive(true);
        popUpLoad.SetActive(false);
        panelOption.SetActive(false);
        panelInfo.SetActive(false);
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
        audioSetter.StopBGM();
        animCamera.Play("CameraMove");
        animPKanan.Play("PintuKanan");
        animPKiri.Play("PintuKiri");
        audioSetter.PlaySFX(audioSetter.openedDoor);
        StartCoroutine(SFXLight(3f));
        animFlash.Play("Flash");

        
        StartCoroutine(LoadSceneWithDelay(6f)); // 3 detik penundaan
        panelMainMenu.SetActive(false);
        popUpLoad.SetActive(false);
    }

    public void ButtonLoadNo()
    {
        DataPersistenceManager.instance.NewGame();
        audioSetter.StopBGM();
        animCamera.Play("CameraMove");
        animPKanan.Play("PintuKanan");
        animPKiri.Play("PintuKiri");
        audioSetter.PlaySFX(audioSetter.openedDoor);
        StartCoroutine(SFXLight(3f));
        animFlash.Play("Flash");

        StartCoroutine(LoadSceneWithDelay(6f)); // 3 detik penundaan
        panelMainMenu.SetActive(false);
        popUpLoad.SetActive(false);
    }

    public void ButtonLoadExit()
    {
        popUpLoad.SetActive(false);
        panelMainMenu.SetActive(true);
    }

    public void ButtonOption()
    {
        panelMainMenu.SetActive(false);
        panelOption.SetActive(true);
    }

    public void ButtonOptionExit()
    {
        panelOption.SetActive(false);
        panelMainMenu.SetActive(true);
    }

    public void ButtonInfo()
    {
        panelMainMenu.SetActive(false);
        panelInfo.SetActive(true);
    }

    public void ButtonInfoExit()
    {
        panelInfo.SetActive(false);
        panelMainMenu.SetActive(true);
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }

    private IEnumerator LoadSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Ganti Scene");
    }

    private IEnumerator SFXLight(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSetter.PlaySFX(audioSetter.lightSpread);
    }
}
