using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayManager : MonoBehaviour, IDataPersistence
{
    [Header("General")]
    public int day;
    public bool dayIsStarted;
    bool isAlreadyStarted;

    [Header("UI Related")]
    [SerializeField] GameObject bookMenuButton;
    [SerializeField] TMP_Text dayTextHUD;
    [SerializeField] Animator animBookButton;

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
        if (dayIsStarted)
        {
            if (!isAlreadyStarted)
            {
                bookMenu.CloseBook();
                dayTextHUD.text = day.ToString();
                isAlreadyStarted = true;
                CloseBookButton();
            }
            // bookMenuButton.SetActive(false);
        }
        else
        {
            if (tutorial.isStartTutor && !tutorial.isStepDone[4])
            {
                bookMenuButton.SetActive(false);
                //CloseBookButton();

            }
            else
            {
                bookMenuButton.SetActive(true);
            }
        }
    }

    public void StartNextDay()
    {
        if (!dayIsStarted)
        {
            dayIsStarted = true;
            isAlreadyStarted = false;
            day++;
        }
    }

    private void CloseBookButton()
    {
        StartCoroutine(CloseBookButtonDelay());
    }

    IEnumerator CloseBookButtonDelay()
    {
        animBookButton.SetTrigger("IsEnd");
        yield return new WaitForSeconds(0.5f);
        bookMenuButton.SetActive(false);
    }
}
