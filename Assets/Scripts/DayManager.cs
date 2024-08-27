using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayManager : MonoBehaviour, IDataPersistence
{
    [Header("General")]
    public int day;
    public bool dayIsStarted;

    [Header("UI Related")]
    [SerializeField] GameObject bookMenuButton;
    [SerializeField] TMP_Text dayTextHUD;

    [Header("References")]
    BookMenu bookMenu;
    Tutorial tutorial;
    
    public void LoadData(GameData data)
    {
        day = data.day;
    }

    public void SaveData(ref GameData data)
    {
        data.day = day;
    }

    private void Start() 
    {
        bookMenu = FindAnyObjectByType<BookMenu>();
        tutorial = FindAnyObjectByType<Tutorial>();
    }

    private void Update() 
    {
        if(dayIsStarted)
        {
            bookMenuButton.SetActive(false);
            bookMenu.CloseBook();
            dayTextHUD.text = "Hari ke-" + day;
        }
        else
        {
            if(tutorial.isStartTutor && !tutorial.isStepDone[4])
            {
                bookMenuButton.SetActive(false);
            }
            else
            {
                bookMenuButton.SetActive(true);
            }
        }    
    }

    public void StartNextDay()
    {
        if(!dayIsStarted)
        {
            dayIsStarted = true;
            day++;
        }
    }
}
