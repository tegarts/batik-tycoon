using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public int level = 1;
    public int maxLevel = 5;
    public int[] upgradeCosts; // Array untuk menyimpan biaya upgrade untuk setiap level

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
}
