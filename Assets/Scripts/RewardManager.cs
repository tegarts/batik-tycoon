using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] Machine[] scriptMesin;
    PlayerInfo playerInfo;
    [SerializeField] int[] rewardManual;
    private Coroutine autoMoneyCoroutine;
    bool isStarted;

    private void Start() 
    {
        playerInfo = FindAnyObjectByType<PlayerInfo>();

        
    }

    private void Update() 
    {
          if (scriptMesin[0].level >= 3 && scriptMesin[1].level >= 3 && scriptMesin[2].level >= 3 && scriptMesin[3].level >= 3 && scriptMesin[4].level >= 3 && !isStarted)
        {
            autoMoneyCoroutine = StartCoroutine(AutoMoneyIncreaseCoroutine());
            isStarted = true;
        }  
    }

    public void GiveRewardManual()
    {
        if(scriptMesin[0].level == 5 && scriptMesin[1].level == 5 && scriptMesin[2].level == 5 && scriptMesin[3].level == 5 && scriptMesin[4].level == 5)
        {
            playerInfo.AddMoney(rewardManual[4]);
        }
        else if(scriptMesin[0].level >= 4 && scriptMesin[1].level >= 4 && scriptMesin[2].level >= 4 && scriptMesin[3].level >= 4 && scriptMesin[4].level >= 4)
        {
            playerInfo.AddMoney(rewardManual[3]);
        }
        else if(scriptMesin[0].level >= 3 && scriptMesin[1].level >= 3 && scriptMesin[2].level >= 3 && scriptMesin[3].level >= 3 && scriptMesin[4].level >= 3)
        {
            playerInfo.AddMoney(rewardManual[2]);
        }
        else if(scriptMesin[0].level >= 2 && scriptMesin[1].level >= 2 && scriptMesin[2].level >= 2 && scriptMesin[3].level >= 2 && scriptMesin[4].level >= 2)
        {
            playerInfo.AddMoney(rewardManual[1]);
        }
        else if(scriptMesin[0].level >= 1 && scriptMesin[1].level >= 1 && scriptMesin[2].level >= 1 && scriptMesin[3].level >= 1 && scriptMesin[4].level >= 1)
        {
            playerInfo.AddMoney(rewardManual[0]);
        }
    }

    private IEnumerator AutoMoneyIncreaseCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1); // Tunggu selama interval uang otomatis
            playerInfo.AddMoney(GetAutoMoneyAmount()); // Tambahkan uang sesuai dengan level mesin
        }
    }

    private int GetAutoMoneyAmount()
    {
        if(scriptMesin[0].level == 5 && scriptMesin[1].level == 5 && scriptMesin[2].level == 5 && scriptMesin[3].level == 5 && scriptMesin[4].level == 5)
        {
            return 15;
        }
        else if(scriptMesin[0].level >= 4 && scriptMesin[1].level >= 4 && scriptMesin[2].level >= 4 && scriptMesin[3].level >= 4 && scriptMesin[4].level >= 4)
        {
            return 10;
        }
        else if(scriptMesin[0].level >= 3 && scriptMesin[1].level >= 3 && scriptMesin[2].level >= 3 && scriptMesin[3].level >= 3 && scriptMesin[4].level >= 3)
        {
            return 5;
        }
        else
        {
            return 0;
        }
    }
}
