using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HireSystem : MonoBehaviour // TODO - reference ke datapersistence buat simpen data karyawan yang udah di hire
{
    PlayerInfo playerInfo;
    AddSystem addSystem;
    [SerializeField] GameObject[] listWorker1, listWorker2, listWorker3, listWorker4, listWorker5;
    [SerializeField] int hargaHire;
    [SerializeField] int jumlahTimPegawai;

    [Header("UI Section")]
    [SerializeField] public TMP_Text jumlahTimPegawaiText;
    [SerializeField] public TMP_Text[] addingCostText;
    [SerializeField] public GameObject addingMessagePanel; // Panel untuk menampilkan pesan upgrade
    [SerializeField] public TMP_Text addingMessageText; // TMP_Text untuk menampilkan pesan upgrade
    private void Start() 
    {
        playerInfo = FindObjectOfType<PlayerInfo>();
        addSystem = FindObjectOfType<AddSystem>();

        // TODO - instantiate pegawai kalo ada data di data persistence
    }

    public void HireWorker()
    {
        if(addSystem.tools1Prefab[0].activeSelf && addSystem.tools1Prefab[1].activeSelf && addSystem.tools1Prefab[2].activeSelf 
        && addSystem.tools1Prefab[3].activeSelf && addSystem.tools1Prefab[4].activeSelf && jumlahTimPegawai < 1)
        {
            if(playerInfo.CanAfford(hargaHire))
            {
                playerInfo.ReduceMoney(hargaHire);
                jumlahTimPegawai++;
                ShowAddedMessage("Jumlah pegawai bertambah menjadi " + jumlahTimPegawai + "Tim");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f));
                UpdateJumlahText();
                for(int i = 0; i < listWorker1.Length; i++)
                {
                    if(!listWorker1[i].activeSelf)
                    {
                        listWorker1[i].SetActive(true);
                        break;
                    }
                }
            }
            else if (!playerInfo.CanAfford(hargaHire))
            {
                ShowAddedMessage("Uang tidak cukup untuk menambah pegawai.");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f)); 
            }
        }
        else if(addSystem.tools2Prefab[0].activeSelf && addSystem.tools2Prefab[1].activeSelf && addSystem.tools2Prefab[2].activeSelf 
        && addSystem.tools2Prefab[3].activeSelf && addSystem.tools2Prefab[4].activeSelf && jumlahTimPegawai < 2)
        {
            if(playerInfo.CanAfford(hargaHire))
            {
                playerInfo.ReduceMoney(hargaHire);
                jumlahTimPegawai++;
                ShowAddedMessage("Jumlah pegawai bertambah menjadi " + jumlahTimPegawai + "Tim");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f));
                UpdateJumlahText();
                for(int i = 0; i < listWorker2.Length; i++)
                {
                    if(!listWorker2[i].activeSelf)
                    {
                        listWorker2[i].SetActive(true);
                        break;
                    }
                }
            }
            else if (!playerInfo.CanAfford(hargaHire))
            {
                ShowAddedMessage("Uang tidak cukup untuk menambah pegawai.");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f)); 
            }
        }
        else if(addSystem.tools3Prefab[0].activeSelf && addSystem.tools3Prefab[1].activeSelf && addSystem.tools3Prefab[2].activeSelf 
        && addSystem.tools3Prefab[3].activeSelf && addSystem.tools3Prefab[4].activeSelf && jumlahTimPegawai < 3)
        {
            if(playerInfo.CanAfford(hargaHire))
            {
                playerInfo.ReduceMoney(hargaHire);
                jumlahTimPegawai++;
                ShowAddedMessage("Jumlah pegawai bertambah menjadi " + jumlahTimPegawai + "Tim");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f));
                UpdateJumlahText();
                for(int i = 0; i < listWorker3.Length; i++)
                {
                    if(!listWorker3[i].activeSelf)
                    {
                        listWorker3[i].SetActive(true);
                        break;
                    }
                }
            }
            else if (!playerInfo.CanAfford(hargaHire))
            {
                ShowAddedMessage("Uang tidak cukup untuk menambah pegawai.");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f)); 
            }
        }
        else if(addSystem.tools4Prefab[0].activeSelf && addSystem.tools4Prefab[1].activeSelf && addSystem.tools4Prefab[2].activeSelf && 
        addSystem.tools4Prefab[3].activeSelf && addSystem.tools4Prefab[4].activeSelf && jumlahTimPegawai < 4)
        {
            if(playerInfo.CanAfford(hargaHire))
            {
                playerInfo.ReduceMoney(hargaHire);
                jumlahTimPegawai++;
                ShowAddedMessage("Jumlah pegawai bertambah menjadi " + jumlahTimPegawai + "Tim");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f));
                UpdateJumlahText();
                for(int i = 0; i < listWorker4.Length; i++)
                {
                    if(!listWorker4[i].activeSelf)
                    {
                        listWorker4[i].SetActive(true);
                        break;
                    }
                }
            }
            else if (!playerInfo.CanAfford(hargaHire))
            {
                ShowAddedMessage("Uang tidak cukup untuk menambah pegawai.");
                StartCoroutine(HideUpgradeMessageDelayed(1.5f)); 
            }
        }
        else if(jumlahTimPegawai >= 4)
        {
            ShowAddedMessage("Jumlah pegawai sudah mencapai jumlah maksimal");
            StartCoroutine(HideUpgradeMessageDelayed(1.5f)); 
        }
        else
        {
            ShowAddedMessage("Lengkapi alat batik terlebih dahulu");
            StartCoroutine(HideUpgradeMessageDelayed(1.5f)); 
        }
    }

    private void UpdateJumlahText()
    {
        if (jumlahTimPegawaiText != null)
        {
            jumlahTimPegawaiText.text = "Jumlah: " + jumlahTimPegawai;
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
