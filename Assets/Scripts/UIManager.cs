using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] GameObject panelPause;
    [SerializeField] GameObject panelOptions;
    [SerializeField] GameObject panelUpgrade;
    [SerializeField] GameObject panelTutorial;
    public bool isAlreadyTutor;
    private Dialogue dialogue;
    TimeManager timeManager;

    public void LoadData(GameData data)
    {
        isAlreadyTutor = data.isAlreadyTutor;
    }

    public void SaveData(ref GameData data)
    {
        data.isAlreadyTutor = isAlreadyTutor;
    }

    private void Start()
    {
        dialogue = FindAnyObjectByType<Dialogue>();
        timeManager = FindAnyObjectByType<TimeManager>();
        panelUpgrade.SetActive(false);
        panelOptions.SetActive(false);
        panelPause.SetActive(false);
        Time.timeScale = 1;
        panelPause.SetActive(false);
        panelOptions.SetActive(false);
        if (isAlreadyTutor)
        {
            panelTutorial.SetActive(false);
        }
        else
        {
            panelTutorial.SetActive(true);
            isAlreadyTutor = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // TODO - Tambahin pengecekan kalo ada panel lain lagi buka (contoh panel upgrade)
            if (!panelPause.activeSelf && !panelUpgrade.activeSelf && !panelTutorial.activeSelf)
            {
                panelPause.SetActive(true);
                Time.timeScale = 0;
            }
            else if (panelPause.activeSelf && !panelOptions.activeSelf)
            {
                panelPause.SetActive(false);
                Time.timeScale = 1;
            }

        }

        // Cek apakah tombol 'U' ditekan
        if (Input.GetKeyDown(KeyCode.U))
        {
            // Jika panel belum aktif, aktifkan
            if (!panelUpgrade.activeSelf && !panelPause.activeSelf && !panelTutorial.activeSelf)
            {
                OpenUpgradePanel();
            }
            else // Jika panel sudah aktif, nonaktifkan
            {
                CloseUpgradePanel();
            }
        }
    }

    public void OpenUpgradePanel()
    {
        panelUpgrade.SetActive(true);
        if (dialogue.isStartTutor)
        {
            dialogue.indicatorUpgrade.SetActive(false);
            dialogue.gameObject.SetActive(true);
        }
    }

    public void CloseUpgradePanel()
    {
        panelUpgrade.SetActive(false);
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

    public void StartDay()
    {
        if(!timeManager.isStartDay)
        {
            timeManager.isStartDay = true;
            timeManager.day += 1;
            timeManager.hour = 8;
            timeManager.minute = 0;
        }
    }


}
