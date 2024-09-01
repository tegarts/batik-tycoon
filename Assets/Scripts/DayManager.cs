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
    public GameObject[] doors;
    public GameObject mainCharacter;
    public GameObject assignAreaMC;

    [Header("Lighting")]
    public Light directionalLight;

    [Header("UI Related")]
    [SerializeField] GameObject bookMenuButton;
    [SerializeField] TMP_Text dayTextHUD;
    [SerializeField] Animator animBookButton;
    [SerializeField] TMP_Text buttonText;

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
        buttonText.text = "Mulai Hari ke-" + (day + 1);
    }

    private void Update()
    {
        if (dayIsStarted)
        {
            if (!isAlreadyStarted)
            {
                directionalLight.intensity = 1.2f;
                bookMenu.CloseBook();
                dayTextHUD.text = day.ToString();
                isAlreadyStarted = true;
                CloseBookButton();
                doors[0].SetActive(true);
                doors[1].SetActive(false);
                mainCharacter.SetActive(true);
                assignAreaMC.SetActive(true);
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
                buttonText.text = "Mulai hari ke-" + (day + 1);
            }

            doors[0].SetActive(false);
            doors[1].SetActive(true);

            if(!tutorial.isStartTutor)
            {
                mainCharacter.SetActive(false);
                assignAreaMC.SetActive(false);
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
