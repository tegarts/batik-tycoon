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

    private Coroutine autoMoneyCoroutine; // Coroutine untuk menambah uang secara otomatis
    RewardManager rewardManager;

    private void Start()
    {
        rewardManager = FindAnyObjectByType<RewardManager>();
        machine = GetComponent<Machine>();
        UpdateLevelText();
        UpdateUpgradeCostText();
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
            if(machine.level >= 3)
            {
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
            levelMachineText.text = "( " + machine.level.ToString() + " )";
        }
    }

    private void UpdateUpgradeCostText()
    {
        if (upgradeCostText != null)
        {
            if(machine.level == 5)
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

    private IEnumerator HideUpgradeMessageDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideUpgradeMessage(); // Menyembunyikan panel pesan setelah delay tertentu
    }

    // private IEnumerator AutoMoneyIncreaseCoroutine()
    // {
    //     while (true)
    //     {
    //         yield return new WaitForSeconds(GetAutoMoneyInterval()); // Tunggu selama interval uang otomatis
    //         playerInfo.AddMoney(GetAutoMoneyAmount()); // Tambahkan uang sesuai dengan level mesin
    //     }
    // }

    // private float GetAutoMoneyInterval()
    // {
    //     // Tentukan interval berdasarkan level mesin
    //     switch (machine.level)
    //     {
    //         case 3:
    //             return 3f; // Setiap 3 detik untuk level 3
    //         case 4:
    //             return 3f; // Setiap 2 detik untuk level 4
    //         case 5:
    //             return 3f; // Setiap 1 detik untuk level 5
    //         default:
    //             return 3f; // Default setiap 3 detik
    //     }
    // }

    // private int GetAutoMoneyAmount()
    // {
    //     // Tentukan jumlah uang yang ditambahkan berdasarkan level mesin
    //     switch (machine.level)
    //     {
    //         case 3:
    //             return 1; // Tambah 1 uang untuk level 3
    //         case 4:
    //             return 1; // Tambah 2 uang untuk level 4
    //         case 5:
    //             return 1; // Tambah 3 uang untuk level 5
    //         default:
    //             return 0; // Tidak menambah uang untuk level lainnya
    //     }
    // }

    private void OnDestroy()
    {
        // Hentikan coroutine ketika objek dihancurkan
        if (autoMoneyCoroutine != null)
        {
            StopCoroutine(autoMoneyCoroutine);
        }
    }
}






