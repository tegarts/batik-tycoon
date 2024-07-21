using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UI;

public class AdvertisingManager : MonoBehaviour
{
    TimeManager timeManager;
    PlayerInfo playerInfo;
    public bool isKoran, isPoster, isBaliho, isPesbuk, isYutup;
    [SerializeField] private int[] advPrice;
    public GameObject panelNotif;
    public TMP_Text notifText;
    public Button[] buttonAdv;
    private bool isAlreadyEnabled;
    private int koranStartDay = -1;
    private int posterStartDay = -1;
    private int balihoStartDay = -1;
    private int pesbukStartDay = -1;
    private int yutupStartDay = -1;

    private void Start()
    {
        timeManager = FindAnyObjectByType<TimeManager>();
        playerInfo = FindAnyObjectByType<PlayerInfo>();
    }

    private void Update()
    {
        if (timeManager.isStartDay)
        {
            isAlreadyEnabled = false;
            for (int i = 0; i < buttonAdv.Length; i++)
            {
                buttonAdv[i].interactable = false;
            }

            if (isKoran && timeManager.day >= koranStartDay + 1)
            {
                isKoran = false;
                koranStartDay = -1;
            }

            if (isPoster && timeManager.day >= posterStartDay + 1)
            {
                isPoster = false;
                posterStartDay = -1;
            }

            if (isBaliho && timeManager.day >= balihoStartDay + 3)
            {
                isBaliho = false;
                balihoStartDay = -1;
            }

            if (isPesbuk && timeManager.day >= pesbukStartDay + 3)
            {
                isPesbuk = false;
                pesbukStartDay = -1;
            }

            if (isYutup && timeManager.day >= yutupStartDay + 7)
            {
                isYutup = false;
                yutupStartDay = -1;
            }
        }
        else
        {
            if (!isAlreadyEnabled)
            {
                isAlreadyEnabled = true;
                for (int i = 0; i < buttonAdv.Length; i++)
                {
                    buttonAdv[i].interactable = true;
                }
            }
        }
    }

    public void KoranAdvertisement()
    {
        if (!timeManager.isStartDay)
        {
            if (playerInfo.CanAfford(advPrice[0]))
            {
                playerInfo.ReduceMoney(advPrice[0]);
                isKoran = true;
                koranStartDay = timeManager.day;

                for (int i = 0; i < buttonAdv.Length; i++)
                {
                    buttonAdv[i].interactable = false;
                }

                ShowUpgradeMessage("Iklan Koran Akan Berjalan Satu Hari"); 
                StartCoroutine(HideUpgradeMessageDelayed(1.5f));
            }
        }
    }

    public void PosterAdvertisement()
    {
        if (!timeManager.isStartDay)
        {
            if (playerInfo.CanAfford(advPrice[1]))
            {
                playerInfo.ReduceMoney(advPrice[1]);
                isPoster = true;
                posterStartDay = timeManager.day;

                for (int i = 0; i < buttonAdv.Length; i++)
                {
                    buttonAdv[i].interactable = false;
                }

                ShowUpgradeMessage("Iklan Poster Akan Berjalan Satu Hari"); 
                StartCoroutine(HideUpgradeMessageDelayed(1.5f));
            }
        }
    }

    public void BalihoAdvertisement()
    {
        if (!timeManager.isStartDay)
        {
            if (playerInfo.CanAfford(advPrice[2]))
            {
                playerInfo.ReduceMoney(advPrice[2]);
                isBaliho = true;
                posterStartDay = timeManager.day;

                for (int i = 0; i < buttonAdv.Length; i++)
                {
                    buttonAdv[i].interactable = false;
                }

                ShowUpgradeMessage("Iklan Baliho Akan Berjalan Tiga Hari"); 
                StartCoroutine(HideUpgradeMessageDelayed(1.5f));
            }
        }
    }

    public void PesbukAdvertisement()
    {
        if (!timeManager.isStartDay)
        {
            if (playerInfo.CanAfford(advPrice[3]))
            {
                playerInfo.ReduceMoney(advPrice[3]);
                isPesbuk = true;
                posterStartDay = timeManager.day;

                for (int i = 0; i < buttonAdv.Length; i++)
                {
                    buttonAdv[i].interactable = false;
                }

                ShowUpgradeMessage("Iklan Pesbuk Akan Berjalan Tiga Hari"); 
                StartCoroutine(HideUpgradeMessageDelayed(1.5f));
            }
        }
    }

    public void YutupAdvertisement()
    {
        if (!timeManager.isStartDay)
        {
            if (playerInfo.CanAfford(advPrice[4]))
            {
                playerInfo.ReduceMoney(advPrice[4]);
                isYutup = true;
                posterStartDay = timeManager.day;

                for (int i = 0; i < buttonAdv.Length; i++)
                {
                    buttonAdv[i].interactable = false;
                }

                ShowUpgradeMessage("Iklan Yutup Akan Berjalan Tujuh Hari"); 
                StartCoroutine(HideUpgradeMessageDelayed(1.5f));
            }
        }
    }


    private void ShowUpgradeMessage(string message)
    {
        if (notifText != null)
        {
            notifText.text = message;
        }
        if (panelNotif != null)
        {
            panelNotif.SetActive(true); 
        }
    }

    private void HideUpgradeMessage()
    {
        if (panelNotif != null)
        {
            panelNotif.SetActive(false); 
        }
    }

    private IEnumerator HideUpgradeMessageDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideUpgradeMessage();
    }
}
