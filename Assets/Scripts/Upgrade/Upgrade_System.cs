using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private Machine machine;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] public TMP_Text levelMachineText;
    [SerializeField] public TMP_Text upgradeCostText;
    [SerializeField] public GameObject upgradeMessagePanel; // Panel untuk menampilkan pesan upgrade
    [SerializeField] public TMP_Text upgradeMessageText; // TMP_Text untuk menampilkan pesan upgrade

    private void Start()
    {
        UpdateLevelText();
        UpdateUpgradeCostText();
        HideUpgradeMessage(); // Menyembunyikan panel pesan upgrade saat memulai game
    }

    public void UpgradeMachine()
    {
        if (machine.Upgrade(playerInfo))
        {
            UpdateLevelText();
            UpdateUpgradeCostText();
            ShowUpgradeMessage("Mesin di-upgrade ke level " + machine.level); // Menampilkan pesan upgrade berhasil
            StartCoroutine(HideUpgradeMessageDelayed(1.5f)); // Menyembunyikan pesan setelah 1.5 detik
        }
        else
        {
            if (!playerInfo.CanAfford(machine.upgradeCosts[machine.level - 1]))
            {
                ShowUpgradeMessage("Uang tidak cukup untuk melakukan upgrade.");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f)); // Menyembunyikan pesan setelah 1.5 detik
            }
            else
            {
                ShowUpgradeMessage("Mesin sudah mencapai level maksimal.");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f)); // Menyembunyikan pesan setelah 1.5 detik
            }
        }
    }

    private void UpdateLevelText()
    {
        if (levelMachineText != null)
        {
            levelMachineText.text = "( " + machine.level.ToString() + " )";
        }
    }

    private void UpdateUpgradeCostText()
    {
        if (upgradeCostText != null)
        {
            int currentUpgradeCost = machine.upgradeCosts[machine.level - 1]; // Biaya upgrade untuk level saat ini
            upgradeCostText.text = "Rp. " + currentUpgradeCost.ToString();
        }
    }

    private void ShowUpgradeMessage(string message)
    {
        if (upgradeMessageText != null)
        {
            upgradeMessageText.text = message;
        }
        if (upgradeMessagePanel != null)
        {
            upgradeMessagePanel.SetActive(true); // Menampilkan panel pesan upgrade
        }
    }

    private void HideUpgradeMessage()
    {
        if (upgradeMessagePanel != null)
        {
            upgradeMessagePanel.SetActive(false); // Menyembunyikan panel pesan upgrade
        }
    }

    private IEnumerator HideUpgradeMessageDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideUpgradeMessage(); // Menyembunyikan panel pesan setelah delay tertentu
    }
}

[System.Serializable]
public class Machine
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
                Debug.Log("Mesin di-upgrade ke level " + level);
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
