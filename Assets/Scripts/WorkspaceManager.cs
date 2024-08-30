using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WorkspaceManager : MonoBehaviour, IDataPersistence
{
    [Header("User Interface")]
    [SerializeField] private Camera cam;
    [SerializeField] private Canvas canvasWorldSpace;
    [SerializeField] Button[] unlocks;
    [SerializeField] GameObject[] textUnlocks;
    [SerializeField] GameObject[] textOwned;
    [SerializeField] GameObject panelNotif;
    [SerializeField] Animator animNotif;
    [SerializeField] TMP_Text notifText;


    [Header("General")]
    public int motifUnlocked;
    [SerializeField] GameObject[] workspaces;
    [SerializeField] int[] workspacePrice;

    [Header("Upgrade Settings")]
    [SerializeField] private int maxUpgradeLevel = 5;
    [SerializeField] private float[] timePerLevel;
    private int[] currentUpgradeLevel;
    [SerializeField] private int[] upgradePrice1;
    [SerializeField] private int[] upgradePrice2;
    [SerializeField] private int[] upgradePrice3;
    [SerializeField] private int[] upgradePrice4;
    [SerializeField] private int[] upgradePrice5;

    [Header("Reference")]
    Money money;
    BookMenu bookMenu;

    public void LoadData(GameData data)
    {
        motifUnlocked = data.motifUnlocked;
    }

    public void SaveData(ref GameData data)
    {
        data.motifUnlocked = motifUnlocked;
    }

    private void Start()
    {
        money = FindAnyObjectByType<Money>();
        currentUpgradeLevel = new int[workspaces.Length];
        for (int i = 0; i < currentUpgradeLevel.Length; i++)
        {
            currentUpgradeLevel[i] = 1; // TODO - Nanti ganti ambil data dari datapersistence
        }
        EnableWorkspace();

        bookMenu = FindAnyObjectByType<BookMenu>();

        if (motifUnlocked == 0)
        {
            for (int i = 0; i < unlocks.Length; i++)
            {
                unlocks[i].interactable = true;
                textUnlocks[i].SetActive(true);
                textOwned[i].SetActive(false);

                textUnlocks[i].GetComponentInChildren<TMP_Text>().text = PriceCount(workspacePrice[i]);
            }
        }
        else if (motifUnlocked == 1)
        {
            unlocks[0].interactable = false;
            textUnlocks[0].SetActive(false);
            textOwned[0].SetActive(true);

            for (int i = 1; i < unlocks.Length; i++)
            {
                unlocks[i].interactable = true;
                textUnlocks[i].SetActive(true);
                textOwned[i].SetActive(false);

                textUnlocks[i].GetComponentInChildren<TMP_Text>().text = PriceCount(workspacePrice[i]);
            }
        }
        else if (motifUnlocked == 2)
        {
            unlocks[0].interactable = false;
            textUnlocks[0].SetActive(false);
            textOwned[0].SetActive(true);
            unlocks[1].interactable = false;
            textUnlocks[1].SetActive(false);
            textOwned[1].SetActive(true);

            for (int i = 2; i < unlocks.Length; i++)
            {
                unlocks[i].interactable = true;
                textUnlocks[i].SetActive(true);
                textOwned[i].SetActive(false);

                textUnlocks[i].GetComponentInChildren<TMP_Text>().text = PriceCount(workspacePrice[i]);
            }
        }
        else if (motifUnlocked == 3)
        {
            unlocks[0].interactable = false;
            textUnlocks[0].SetActive(false);
            textOwned[0].SetActive(true);
            unlocks[1].interactable = false;
            textUnlocks[1].SetActive(false);
            textOwned[1].SetActive(true);
            unlocks[2].interactable = false;
            textUnlocks[2].SetActive(false);
            textOwned[2].SetActive(true);

            unlocks[4].interactable = true;
            textUnlocks[4].SetActive(true);
            textOwned[4].SetActive(false);
            textUnlocks[4].GetComponentInChildren<TMP_Text>().text = PriceCount(workspacePrice[4]);
        }
        else if (motifUnlocked == 4)
        {
            for (int i = 0; i < unlocks.Length; i++)
            {
                unlocks[i].interactable = false;
                textUnlocks[i].SetActive(false);
                textOwned[i].SetActive(true);
            }
        }
    }

    private void EnableWorkspace()
    {
        for (int i = 0; i < workspaces.Length; i++)
        {
            workspaces[i].SetActive(false);
        }
        for (int i = 0; i < motifUnlocked + 1; i++)
        {
            workspaces[i].SetActive(true);
            workspaces[i].GetComponent<Workspace>().SetupAssignArea(canvasWorldSpace, cam);
        }
    }

    public void UnlockWorkspace(int level)
    {
        if (level == 2)
        {
            if (motifUnlocked == 0)
            {
                if (money.CanAfford(workspacePrice[0]))
                {
                    unlocks[0].interactable = false;
                    textUnlocks[0].SetActive(false);
                    textOwned[0].SetActive(true);

                    money.ReduceMoney(workspacePrice[0]);
                    motifUnlocked = 1;
                    EnableWorkspace();
                    bookMenu.CloseBook();
                }
                else
                {
                    notifText.text = "Uang tidak cukup untuk melakukan unlock workspace motif megamendung";
                    ShowNotif();
                }
            }
            else
            {
                notifText.text = "Unlock workspace motif sebelumnya terlebih dahulu";
                ShowNotif();
            }
        }
        else if (level == 3)
        {
            if (motifUnlocked == 1)
            {
                if (money.CanAfford(workspacePrice[1]))
                {
                    unlocks[1].interactable = false;
                    textUnlocks[1].SetActive(false);
                    textOwned[1].SetActive(true);

                    money.ReduceMoney(workspacePrice[1]);
                    motifUnlocked = 2;
                    EnableWorkspace();
                    bookMenu.CloseBook();
                }
                else
                {
                    notifText.text = "Uang tidak cukup untuk melakukan unlock workspace motif truntum";
                    ShowNotif();
                }
            }
            else
            {
                notifText.text = "Unlock workspace motif sebelumnya terlebih dahulu";
                ShowNotif();
            }
        }
        else if (level == 4)
        {
            if (motifUnlocked == 2)
            {
                if (money.CanAfford(workspacePrice[2]))
                {
                    unlocks[2].interactable = false;
                    textUnlocks[2].SetActive(false);
                    textOwned[2].SetActive(true);

                    money.ReduceMoney(workspacePrice[2]);
                    motifUnlocked = 3;
                    EnableWorkspace();
                    bookMenu.CloseBook();
                }
                else
                {
                    notifText.text = "Uang tidak cukup untuk melakukan unlock workspace motif parang";
                    ShowNotif();
                }
            }
            else
            {
                notifText.text = "Unlock workspace motif sebelumnya terlebih dahulu";
                ShowNotif();
            }
        }
        if (level == 5)
        {
            if (motifUnlocked == 3)
            {
                if (money.CanAfford(workspacePrice[3]))
                {
                    unlocks[3].interactable = false;
                    textUnlocks[3].SetActive(false);
                    textOwned[3].SetActive(true);

                    money.ReduceMoney(workspacePrice[3]);
                    motifUnlocked = 4;
                    EnableWorkspace();
                    bookMenu.CloseBook();
                }
                else
                {
                    notifText.text = "Uang tidak cukup untuk melakukan unlock workspace motif simbut";
                    ShowNotif();
                }
            }
            else
            {
                notifText.text = "Unlock workspace motif sebelumnya terlebih dahulu";
                ShowNotif();
            }
        }
    }

    public void UpgradeWorkspace(int workspaceIndex)
    {
        if (workspaceIndex >= 0 && workspaceIndex < workspaces.Length)
        {
            Workspace workspace = workspaces[workspaceIndex].GetComponent<Workspace>();
            int upgradeLevel = currentUpgradeLevel[workspaceIndex] + 1;

            if (upgradeLevel <= maxUpgradeLevel)
            {
                if (workspaceIndex == 0)
                {
                    if (money.CanAfford(upgradePrice1[upgradeLevel - 1]))
                    {
                        money.ReduceMoney(upgradePrice1[upgradeLevel - 1]);
                        int newProgresTime = Mathf.RoundToInt(timePerLevel[upgradeLevel - 1]);
                        workspace.UpdateProgresTime(newProgresTime);
                        currentUpgradeLevel[workspaceIndex] = upgradeLevel;
                        workspace.level_workspace = upgradeLevel;
                        Debug.Log($"Workspace {workspaceIndex + 1} upgraded to level {upgradeLevel} with new progress time: {newProgresTime} seconds.");
                        bookMenu.CloseBook();
                    }
                    else
                    {
                        ShowNotif();
                        return;
                    }
                }
                else if (workspaceIndex == 1)
                {
                    if (money.CanAfford(upgradePrice2[upgradeLevel - 1]))
                    {
                        money.ReduceMoney(upgradePrice2[upgradeLevel - 1]);
                        int newProgresTime = Mathf.RoundToInt(timePerLevel[upgradeLevel - 1]);
                        workspace.UpdateProgresTime(newProgresTime);
                        currentUpgradeLevel[workspaceIndex] = upgradeLevel;
                        workspace.level_workspace = upgradeLevel;
                        Debug.Log($"Workspace {workspaceIndex + 1} upgraded to level {upgradeLevel} with new progress time: {newProgresTime} seconds.");
                        bookMenu.CloseBook();
                    }
                    else
                    {
                        ShowNotif();
                        return;
                    }
                }
                else if (workspaceIndex == 1)
                {
                    if (money.CanAfford(upgradePrice2[upgradeLevel - 1]))
                    {
                        money.ReduceMoney(upgradePrice2[upgradeLevel - 1]);
                        int newProgresTime = Mathf.RoundToInt(timePerLevel[upgradeLevel - 1]);
                        workspace.UpdateProgresTime(newProgresTime);
                        currentUpgradeLevel[workspaceIndex] = upgradeLevel;
                        workspace.level_workspace = upgradeLevel;
                        Debug.Log($"Workspace {workspaceIndex + 1} upgraded to level {upgradeLevel} with new progress time: {newProgresTime} seconds.");
                        bookMenu.CloseBook();
                    }
                    else
                    {
                        ShowNotif();
                        return;
                    }
                }
                else if (workspaceIndex == 2)
                {
                    if (money.CanAfford(upgradePrice3[upgradeLevel - 1]))
                    {
                        money.ReduceMoney(upgradePrice3[upgradeLevel - 1]);
                        int newProgresTime = Mathf.RoundToInt(timePerLevel[upgradeLevel - 1]);
                        workspace.UpdateProgresTime(newProgresTime);
                        currentUpgradeLevel[workspaceIndex] = upgradeLevel;
                        workspace.level_workspace = upgradeLevel;
                        Debug.Log($"Workspace {workspaceIndex + 1} upgraded to level {upgradeLevel} with new progress time: {newProgresTime} seconds.");
                        bookMenu.CloseBook();
                    }
                    else
                    {
                        ShowNotif();
                        return;
                    }
                }
                else if (workspaceIndex == 3)
                {
                    if (money.CanAfford(upgradePrice4[upgradeLevel - 1]))
                    {
                        money.ReduceMoney(upgradePrice4[upgradeLevel - 1]);
                        int newProgresTime = Mathf.RoundToInt(timePerLevel[upgradeLevel - 1]);
                        workspace.UpdateProgresTime(newProgresTime);
                        currentUpgradeLevel[workspaceIndex] = upgradeLevel;
                        workspace.level_workspace = upgradeLevel;
                        Debug.Log($"Workspace {workspaceIndex + 1} upgraded to level {upgradeLevel} with new progress time: {newProgresTime} seconds.");
                        bookMenu.CloseBook();
                    }
                    else
                    {
                        ShowNotif();
                        return;
                    }
                }
                else if (workspaceIndex == 4)
                {
                    if (money.CanAfford(upgradePrice5[upgradeLevel - 1]))
                    {
                        money.ReduceMoney(upgradePrice5[upgradeLevel - 1]);
                        int newProgresTime = Mathf.RoundToInt(timePerLevel[upgradeLevel - 1]);
                        workspace.UpdateProgresTime(newProgresTime);
                        currentUpgradeLevel[workspaceIndex] = upgradeLevel;
                        workspace.level_workspace = upgradeLevel;
                        Debug.Log($"Workspace {workspaceIndex + 1} upgraded to level {upgradeLevel} with new progress time: {newProgresTime} seconds.");
                        bookMenu.CloseBook();
                    }
                    else
                    {
                        ShowNotif();
                        return;
                    }
                }
            }
            else
            {
                Debug.LogWarning("Max upgrade level reached.");
            }
        }
        else
        {
            Debug.LogWarning("Invalid workspace index.");
        }
    }

    private string PriceCount(int price)
    {
        if (price < 1000)
        {
            return ((int)price).ToString() + "rb";
        }
        else
        {
            return ((float)price / 1000f).ToString("0.##") + "Jt";
        }
    }

    public void ShowNotif()
    {
        StartCoroutine(ShowNotifDelay());
    }

    IEnumerator ShowNotifDelay()
    {
        panelNotif.SetActive(true);
        yield return new WaitForSeconds(1f);
        animNotif.SetTrigger("IsEnd");
        yield return new WaitForSeconds(0.5f);
        panelNotif.SetActive(false);
    }
}