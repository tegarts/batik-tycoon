using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeSystem : MonoBehaviour
{
    private Machine machine;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] public TMP_Text levelMachineText;
    [SerializeField] public TMP_Text upgradeCostText;
    [SerializeField] public GameObject upgradeMessagePanel; // Panel untuk menampilkan pesan upgrade
    [SerializeField] public TMP_Text upgradeMessageText; // TMP_Text untuk menampilkan pesan upgrade
    [SerializeField] public TMP_Text ExplainText;
    [SerializeField] private string[] explanationTexts;

    private Coroutine autoMoneyCoroutine; // Coroutine untuk menambah uang secara otomatis
    RewardManager rewardManager;
    WorkerManager workerManager;

    private void Start()
    {
        rewardManager = FindAnyObjectByType<RewardManager>();
        workerManager = FindAnyObjectByType<WorkerManager>();
        machine = GetComponent<Machine>();
        UpdateLevelText();
        UpdateUpgradeCostText();
        UpdateExplanationText(); // Memperbarui teks penjelasan saat memulai game
        HideUpgradeMessage(); // Menyembunyikan panel pesan upgrade saat memulai game

        // Mulai coroutine untuk menambah uang secara otomatis jika mesin sudah level 3 atau lebih
        // if (machine.level >= 3)
        // {
        //     autoMoneyCoroutine = StartCoroutine(AutoMoneyIncreaseCoroutine());
        // }
    }

    public void UpgradeMachine()
    {
        if (machine.Upgrade(playerInfo))
        {
            UpdateLevelText();
            UpdateUpgradeCostText();
            ShowUpgradeMessage("Mesin di-upgrade ke level " + machine.level); // Menampilkan pesan upgrade berhasil
            StartCoroutine(HideUpgradeMessageDelayed(1.5f)); // Menyembunyikan pesan setelah 1.5 detik
            UpdateExplanationText(); // Memperbarui teks penjelasan setelah upgrade

            // Memulai atau hentikan coroutine untuk menambah uang secara otomatis sesuai level
            // if (machine.level >= 3 && autoMoneyCoroutine == null)
            // {
            //     autoMoneyCoroutine = StartCoroutine(AutoMoneyIncreaseCoroutine());
            // }
            // else if (machine.level < 3 && autoMoneyCoroutine != null)
            // {
            //     StopCoroutine(autoMoneyCoroutine);
            //     autoMoneyCoroutine = null;
            // }

            // ini buat nambahin value mesin yang udah di upgrade buat auto
            if (machine.level >= 3)
            {
                workerManager.EnableMachines();
                rewardManager.upgradeCounter++;
                Debug.Log("cek upgradesystem");
            }

        }
        else
        {
            if (machine.IsMaxLevel())
            {
                ShowUpgradeMessage("Mesin sudah mencapai level maksimal."); // Menampilkan pesan mesin sudah maksimal
                StartCoroutine(HideUpgradeMessageDelayed(1.5f)); // Menyembunyikan pesan setelah 1.5 detik
            }
            else if (!playerInfo.CanAfford(machine.upgradeCosts[machine.level - 1]))
            {
                ShowUpgradeMessage("Uang tidak cukup untuk melakukan upgrade.");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f)); // Menyembunyikan pesan setelah 1.5 detik
            }
        }
    }

    private void UpdateLevelText()
    {
        if (levelMachineText != null)
        {
            levelMachineText.text = "Level " + machine.level.ToString();
        }
    }

    private void UpdateUpgradeCostText()
    {
        if (upgradeCostText != null)
        {
            if (machine.level == 5)
            {
                upgradeCostText.text = "Max";
            }
            else
            {
                int currentUpgradeCost = machine.upgradeCosts[machine.level - 1]; // Biaya upgrade untuk level saat ini
                upgradeCostText.text = "Rp. " + currentUpgradeCost.ToString();
            }
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

    private void UpdateExplanationText()
    {
        if (ExplainText != null && machine.level <= explanationTexts.Length)
        {
            ExplainText.text = explanationTexts[machine.level - 1];
        }
    }

    private IEnumerator HideUpgradeMessageDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideUpgradeMessage(); // Menyembunyikan panel pesan setelah delay tertentu
    }

    private void OnDestroy()
    {
        // Hentikan coroutine ketika objek dihancurkan
        if (autoMoneyCoroutine != null)
        {
            StopCoroutine(autoMoneyCoroutine);
        }
    }
}






