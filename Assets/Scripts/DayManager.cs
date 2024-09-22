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
    [SerializeField] Animator animStartDayButton;
    [SerializeField] TMP_Text buttonText;
    [SerializeField] GameObject[] motifBatikStartDay;

    [Header("References")]
    BookMenu bookMenu;
    StartDay startDay;
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
        startDay = FindAnyObjectByType<StartDay>();
        buttonText.text = "Mulai Hari ke-" + (day + 1) + "?";

        for(int i = 0; i < motifBatikStartDay.Length; i++)
        {
            motifBatikStartDay[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (dayIsStarted)
        {
            if (!isAlreadyStarted)
            {
                directionalLight.intensity = 1.2f;
                // bookMenu.CloseBook();
                startDay.CloseStartDayPanel();
                dayTextHUD.text = day.ToString();
                isAlreadyStarted = true;
                CloseBookButton();
                startDay.CloseStartDayButton();
                doors[0].SetActive(true);
                doors[1].SetActive(false);
                mainCharacter.SetActive(true);
                assignAreaMC.SetActive(true);
            }
            // bookMenuButton.SetActive(false);
        }
        else
        {

            if(tutorial.isStartTutor)
            {
                bookMenuButton.SetActive(false);
                startDay.startDayButton.SetActive(false);
            }
            else
            {
                startDay.startDayButton.SetActive(true);
                bookMenuButton.SetActive(true);
                if((day + 1) < 3)
                {
                    motifBatikStartDay[0].SetActive(true);
                    motifBatikStartDay[1].SetActive(true);
                    motifBatikStartDay[2].SetActive(false);
                    motifBatikStartDay[3].SetActive(false);
                    motifBatikStartDay[4].SetActive(false);
                }
                else if((day + 1) < 5)
                {
                    motifBatikStartDay[0].SetActive(true);
                    motifBatikStartDay[1].SetActive(true);
                    motifBatikStartDay[2].SetActive(true);
                    motifBatikStartDay[3].SetActive(false);
                    motifBatikStartDay[4].SetActive(false);
                }
                else if((day + 1) < 7)
                {
                    motifBatikStartDay[0].SetActive(true);
                    motifBatikStartDay[1].SetActive(true);
                    motifBatikStartDay[2].SetActive(true);
                    motifBatikStartDay[3].SetActive(true);
                    motifBatikStartDay[4].SetActive(false);
                }
                else
                {
                    motifBatikStartDay[0].SetActive(true);
                    motifBatikStartDay[1].SetActive(true);
                    motifBatikStartDay[2].SetActive(true);
                    motifBatikStartDay[3].SetActive(true);
                    motifBatikStartDay[4].SetActive(true);
                }
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
