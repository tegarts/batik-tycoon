using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] Machine[] scriptMesin;
    PlayerInfo playerInfo;
    [SerializeField] int[] rewardManual;
    TimeManager timeManager;
    public int upgradeCounter;
    public bool isStarted;
    public bool isStopped;
    IEnumerator autoKain;

    public void LoadData(GameData data)
    {
        upgradeCounter = data.upgradeCounter;
    }

    public void SaveData(ref GameData data)
    {
        data.upgradeCounter = upgradeCounter;
    }

    private void Start()
    {
        playerInfo = FindAnyObjectByType<PlayerInfo>();
        timeManager = FindAnyObjectByType<TimeManager>();
        autoKain = AutoKainIncreaseCoroutine();
    }

    private void Update()
    {
        if (timeManager.isStartDay)
        {
            if (scriptMesin[0].level >= 3 && !isStarted || scriptMesin[1].level >= 3 && !isStarted || scriptMesin[2].level >= 3 && !isStarted || scriptMesin[3].level >= 3 && !isStarted || scriptMesin[4].level >= 3 && !isStarted)
            {
                Debug.Log("cek rewardmanager");
                StartCoroutine(autoKain);
                isStarted = true;
                isStopped = false;
            }
        }
        else if(!timeManager.isStartDay && !isStopped)
        {
            Debug.Log("cek false");
            StopCoroutine(autoKain);
            isStarted = false;
            isStopped = true;
        }
    }

    public void GiveRewardManual()
    {
        if (scriptMesin[0].level == 5 && scriptMesin[1].level == 5 && scriptMesin[2].level == 5 && scriptMesin[3].level == 5 && scriptMesin[4].level == 5)
        {
            playerInfo.AddKain(rewardManual[4]);
        }
        else if (scriptMesin[0].level >= 4 && scriptMesin[1].level >= 4 && scriptMesin[2].level >= 4 && scriptMesin[3].level >= 4 && scriptMesin[4].level >= 4)
        {
            playerInfo.AddKain(rewardManual[3]);
        }
        else if (scriptMesin[0].level >= 3 && scriptMesin[1].level >= 3 && scriptMesin[2].level >= 3 && scriptMesin[3].level >= 3 && scriptMesin[4].level >= 3)
        {
            playerInfo.AddKain(rewardManual[2]);
        }
        else if (scriptMesin[0].level >= 2 && scriptMesin[1].level >= 2 && scriptMesin[2].level >= 2 && scriptMesin[3].level >= 2 && scriptMesin[4].level >= 2)
        {
            playerInfo.AddKain(rewardManual[1]);
        }
        else if (scriptMesin[0].level >= 1 && scriptMesin[1].level >= 1 && scriptMesin[2].level >= 1 && scriptMesin[3].level >= 1 && scriptMesin[4].level >= 1)
        {
            playerInfo.AddKain(rewardManual[0]);
        }
    }

    public IEnumerator AutoKainIncreaseCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(GetAutoKainAmount()); // Tunggu selama interval uang otomatis
            playerInfo.AddKain(1); // Tambahkan uang sesuai dengan level mesin
        }


    }

    private int GetAutoKainAmount()
    {
        switch (upgradeCounter)
        {
            case 15:
                return 6;
            case 14:
                return 7;
            case 13:
                return 8;
            case 12:
                return 9;
            case 11:
                return 10;
            case 10:
                return 11;
            case 9:
                return 12;
            case 8:
                return 13;
            case 7:
                return 14;
            case 6:
                return 15;
            case 5:
                return 16;
            case 4:
                return 17;
            case 3:
                return 18;
            case 2:
                return 19;
            case 1:
                return 20;
            default:
                return 0;
        }


    }
}
