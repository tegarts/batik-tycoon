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
        audioSetter.PlaySFX(audioSetter.OpenPanel);
        if(DataPersistenceManager.instance.HasGameData())
        {
            popUpLoad.SetActive(true);
        }
        else
        {
            DataPersistenceManager.instance.NewGame();
            SceneManager.LoadScene("New Mechanic 2"); // TODO - Ganti nama scene
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
        audioSetter.PlaySFX(audioSetter.ClosePanel);
        popUpLoad.SetActive(false);
        panelMainMenu.SetActive(true);
    }

    public void ButtonOption()
    {
        audioSetter.PlaySFX(audioSetter.OpenPanel);
        panelMainMenu.SetActive(false);
        panelOption.SetActive(true);
    }

    public void ButtonOptionExit()
    {
        audioSetter.PlaySFX(audioSetter.ClosePanel);
        panelOption.SetActive(false);
        panelMainMenu.SetActive(true);
    }

    public void ButtonInfo()
    {
        audioSetter.PlaySFX(audioSetter.OpenPanel);
        panelMainMenu.SetActive(false);
        panelInfo.SetActive(true);
    }

    public void ButtonInfoExit()
    {
        audioSetter.PlaySFX(audioSetter.ClosePanel);
        panelInfo.SetActive(false);
        panelMainMenu.SetActive(true);
    }

    public void ButtonQuit()
    {
        audioSetter.PlaySFX(audioSetter.ClosePanel);
        Application.Quit();
    }

    private IEnumerator LoadSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Ganti Scene");
        SceneManager.LoadScene("New Mechanic 2");
    }

    private IEnumerator SFXLight(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSetter.PlaySFX(audioSetter.lightSpread);
    }
}
