using UnityEngine;
using TMPro;
using System.Collections;

public class AddSystem : MonoBehaviour, IDataPersistence
{
    TimeManager timeManager;
    PlayerInfo playerInfo;
    public int jumlahTools1, jumlahTools2, jumlahTools3, jumlahTools4, jumlahTools5;
    public int hargaTools;
    public int maxJumlahAlat;
    public GameObject[] tools1Prefab, tools2Prefab, tools3Prefab, tools4Prefab, tools5Prefab;

    [Header("UI Section")]
    [SerializeField] public TMP_Text[] jumlahMachineText;
    [SerializeField] public TMP_Text[] addingCostText;
    [SerializeField] public GameObject addingMessagePanel; // Panel untuk menampilkan pesan upgrade
    [SerializeField] public TMP_Text addingMessageText; // TMP_Text untuk menampilkan pesan upgrade

    public void LoadData(GameData data)
    {
        jumlahTools1 = data.jumlahTools1;
        jumlahTools2 = data.jumlahTools2;
        jumlahTools3 = data.jumlahTools3;
        jumlahTools4 = data.jumlahTools4;
        jumlahTools5 = data.jumlahTools5;
    }

    public void SaveData(ref GameData data)
    {
        if (!timeManager.isStartDay)
        {
            data.jumlahTools1 = jumlahTools1;
            data.jumlahTools2 = jumlahTools2;
            data.jumlahTools3 = jumlahTools3;
            data.jumlahTools4 = jumlahTools4;
        }
    }

    private void Start()
    {
        timeManager = FindAnyObjectByType<TimeManager>();
        playerInfo = FindAnyObjectByType<PlayerInfo>();

        for(int i = 0; i < addingCostText.Length; i++)
        {
            addingCostText[i].text = "Rp. " + hargaTools; 
        }

        UpdateJumlah1Text();
        UpdateJumlah2Text();
        UpdateJumlah3Text();
        UpdateJumlah4Text();
        UpdateJumlah5Text();
        HideUpgradeMessage(); 
        

        for (int i = 0; i < tools1Prefab.Length; i++)
        {
            tools1Prefab[i].SetActive(false);
            tools2Prefab[i].SetActive(false);
            tools3Prefab[i].SetActive(false);
            tools4Prefab[i].SetActive(false);
            tools5Prefab[i].SetActive(false);
        }

        // TODO - Pengecekan kalo misal udah hire orang, berarti game object nya ilangin
        for (int i = 0; i < jumlahTools1 - 1; i++)
        {
            tools1Prefab[i].SetActive(true);
        }

        for(int i = 0; i < jumlahTools2- 1; i++)
        {
            tools2Prefab[i].SetActive(true);
        }

        for(int i = 0; i < jumlahTools3 - 1; i++)
        {
            tools3Prefab[i].SetActive(true);
        }

        for(int i = 0; i < jumlahTools4 - 1; i++)
        {
            tools4Prefab[i].SetActive(true);
        }

        for(int i = 0; i < jumlahTools5 - 1; i++)
        {
            tools5Prefab[i].SetActive(true);
        }
    }

    public void AddTools1()
    {
        if (jumlahTools1 < maxJumlahAlat && playerInfo.CanAfford(hargaTools))
        {
            playerInfo.ReduceMoney(hargaTools);
            tools1Prefab[jumlahTools1 - 1].SetActive(true);
            jumlahTools1++;
            ShowAddedMessage("Jumlah alat bertambah menjadi " + jumlahTools1);
            StartCoroutine(HideUpgradeMessageDelayed(1.5f));
            UpdateJumlah1Text();
        }
        else
        {
            if (jumlahTools1 >= maxJumlahAlat)
            {
                ShowAddedMessage("Alat sudah mencapai jumlah maksimal.");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f)); 
            }
            else if (!playerInfo.CanAfford(hargaTools))
            {
                ShowAddedMessage("Uang tidak cukup untuk melakukan penambahan alat.");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f)); 
            }
        }
    }

    public void AddTools2()
    {
        if (jumlahTools2 < maxJumlahAlat && playerInfo.CanAfford(hargaTools))
        {
            playerInfo.ReduceMoney(hargaTools);
            tools2Prefab[jumlahTools2 - 1].SetActive(true);
            jumlahTools2++;
            ShowAddedMessage("Jumlah alat bertambah menjadi " + jumlahTools2);
            StartCoroutine(HideUpgradeMessageDelayed(1.5f));
            UpdateJumlah2Text();
        }
        else
        {
            if (jumlahTools2 >= maxJumlahAlat)
            {
                ShowAddedMessage("Alat sudah mencapai jumlah maksimal.");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f)); 
            }
            else if (!playerInfo.CanAfford(hargaTools))
            {
                ShowAddedMessage("Uang tidak cukup untuk melakukan penambahan alat.");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f)); 
            }
        }
    }

    public void AddTools3()
    {
        if (jumlahTools3 < maxJumlahAlat && playerInfo.CanAfford(hargaTools))
        {
            playerInfo.ReduceMoney(hargaTools);
            tools3Prefab[jumlahTools3 - 1].SetActive(true);
            jumlahTools3++;
            ShowAddedMessage("Jumlah alat bertambah menjadi " + jumlahTools3);
            StartCoroutine(HideUpgradeMessageDelayed(1.5f));
            UpdateJumlah3Text();
        }
        else
        {
            if (jumlahTools3 >= maxJumlahAlat)
            {
                ShowAddedMessage("Alat sudah mencapai jumlah maksimal.");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f)); 
            }
            else if (!playerInfo.CanAfford(hargaTools))
            {
                ShowAddedMessage("Uang tidak cukup untuk melakukan penambahan alat.");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f)); 
            }
        }
    }

    public void AddTools4()
    {
        if (jumlahTools4 < maxJumlahAlat && playerInfo.CanAfford(hargaTools))
        {
            playerInfo.ReduceMoney(hargaTools);
            tools4Prefab[jumlahTools4 - 1].SetActive(true);
            jumlahTools4++;
            ShowAddedMessage("Jumlah alat bertambah menjadi " + jumlahTools4);
            StartCoroutine(HideUpgradeMessageDelayed(1.5f));
            UpdateJumlah4Text();
        }
        else
        {
            if (jumlahTools4 >= maxJumlahAlat)
            {
                ShowAddedMessage("Alat sudah mencapai jumlah maksimal.");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f)); 
            }
            else if (!playerInfo.CanAfford(hargaTools))
            {
                ShowAddedMessage("Uang tidak cukup untuk melakukan penambahan alat.");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f)); 
            }
        }
    }

    public void AddTools5()
    {
        if (jumlahTools5 < maxJumlahAlat && playerInfo.CanAfford(hargaTools))
        {
            playerInfo.ReduceMoney(hargaTools);
            tools5Prefab[jumlahTools5 - 1].SetActive(true);
            jumlahTools5++;
            ShowAddedMessage("Jumlah alat bertambah menjadi " + jumlahTools5);
            StartCoroutine(HideUpgradeMessageDelayed(1.5f));
            UpdateJumlah5Text();
        }
        else
        {
            if (jumlahTools5 >= maxJumlahAlat)
            {
                ShowAddedMessage("Alat sudah mencapai jumlah maksimal.");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f)); 
            }
            else if (!playerInfo.CanAfford(hargaTools))
            {
                ShowAddedMessage("Uang tidak cukup untuk melakukan penambahan alat.");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f)); 
            }
        }
    }

    private void UpdateJumlah1Text()
    {
        if (jumlahMachineText[0] != null)
        {
            jumlahMachineText[0].text = "Jumlah: " + jumlahTools1;
        }
    }

    private void UpdateJumlah2Text()
    {
        if (jumlahMachineText[1] != null)
        {
            jumlahMachineText[1].text = "Jumlah: " + jumlahTools2;
        }
    }

    private void UpdateJumlah3Text()
    {
        if (jumlahMachineText[2] != null)
        {
            jumlahMachineText[2].text = "Jumlah: " + jumlahTools3;
        }
    }

    private void UpdateJumlah4Text()
    {
        if (jumlahMachineText[3] != null)
        {
            jumlahMachineText[3].text = "Jumlah: " + jumlahTools4;
        }
    }

    private void UpdateJumlah5Text()
    {
        if (jumlahMachineText[4] != null)
        {
            jumlahMachineText[4].text = "Jumlah: " + jumlahTools5;
        }
    }

    private void ShowAddedMessage(string message)
    {
        if (addingMessageText != null)
        {
            addingMessageText.text = message;
        }
        if (addingMessagePanel != null)
        {
            addingMessagePanel.SetActive(true); 
        }
    }

    private void HideUpgradeMessage()
    {
        if (addingMessagePanel != null)
        {
            addingMessagePanel.SetActive(false); 
        }
    }

    private IEnumerator HideUpgradeMessageDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideUpgradeMessage();
    }
}
