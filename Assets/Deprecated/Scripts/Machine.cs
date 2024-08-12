using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour, IDataPersistence
{
    [SerializeField] int machineNumber;
    TimeManager timeManager;
    public int level = 1;
    public int maxLevel = 5;
    public int[] upgradeCosts; // Array untuk menyimpan biaya upgrade untuk setiap level
    public void LoadData(GameData data)
    {
        if (machineNumber == 1)
        {
            level = data.machineLevel1;
        }
        else if (machineNumber == 2)
        {
            level = data.machineLevel2;
        }
        else if (machineNumber == 3)
        {
            level = data.machineLevel3;
        }
        else if (machineNumber == 4)
        {
            level = data.machineLevel4;
        }
        else if (machineNumber == 5)
        {
            level = data.machineLevel5;
        }

    }

    public void SaveData(ref GameData data)
    {
        if (!timeManager.isStartDay)
        {
            if (machineNumber == 1)
            {
                data.machineLevel1 = level;
            }
            else if (machineNumber == 2)
            {
                data.machineLevel2 = level;
            }
            else if (machineNumber == 3)
            {
                data.machineLevel3 = level;
            }
            else if (machineNumber == 4)
            {
                data.machineLevel4 = level;
            }
            else if (machineNumber == 5)
            {
                data.machineLevel5 = level;
            }
        }
    }

    public bool Upgrade(PlayerInfo playerInfo)
    {
        if (!IsMaxLevel())
        {
            int currentUpgradeCost = upgradeCosts[level - 1]; // Biaya upgrade untuk level saat ini
            if (playerInfo.CanAfford(currentUpgradeCost))
            {
                playerInfo.ReduceMoney(currentUpgradeCost);
                level++;
                return true;
            }
        }
        return false;
    }

    public bool IsMaxLevel()
    {
        return level >= maxLevel;
    }

    private void Start()
    {
        timeManager = FindAnyObjectByType<TimeManager>();
    }
}
