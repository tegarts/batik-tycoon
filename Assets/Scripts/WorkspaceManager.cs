using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorkspaceManager : MonoBehaviour, IDataPersistence
{
    [Header("User Interface")]
    [SerializeField] private Camera cam;
    [SerializeField] private Canvas canvasWorldSpace;

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
                    money.ReduceMoney(workspacePrice[0]);
                    motifUnlocked = 1;
                    EnableWorkspace();
                }
                else
                {
                    Debug.Log("Uang kurang");
                }
            }
            else
            {
                Debug.Log("Set button uninteracable atau semacamnya, karena player sudah unlock motif ini");
            }
        }
        else if (level == 3)
        {
            if (motifUnlocked == 1)
            {
                if (money.CanAfford(workspacePrice[1]))
                {
                    money.ReduceMoney(workspacePrice[1]);
                    motifUnlocked = 2;
                    EnableWorkspace();
                }
                else
                {
                    Debug.Log("Uang kurang");
                }
            }
            else
            {
                Debug.Log("Set button uninteracable atau semacamnya, karena player sudah unlock motif ini");
            }
        }
        else if (level == 4)
        {
            if (motifUnlocked == 2)
            {
                if (money.CanAfford(workspacePrice[2]))
                {
                    money.ReduceMoney(workspacePrice[2]);
                    motifUnlocked = 3;
                    EnableWorkspace();
                }
                else
                {
                    Debug.Log("Uang kurang");
                }
            }
            else
            {
                Debug.Log("Set button uninteracable atau semacamnya, karena player sudah unlock motif ini");
            }
        }
        if (level == 5)
        {
            if (motifUnlocked == 3)
            {
                if (money.CanAfford(workspacePrice[3]))
                {
                    money.ReduceMoney(workspacePrice[3]);
                    motifUnlocked = 4;
                    EnableWorkspace();
                }
                else
                {
                    Debug.Log("Uang kurang");
                }
            }
            else
            {
                Debug.Log("Set button uninteracable atau semacamnya, karena player sudah unlock motif ini");
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
                    }
                    else
                    {
                        Debug.LogWarning("Not enough money to upgrade workspace.");
                        return;
                    }
                }
                else if(workspaceIndex == 1)
                {
                    if (money.CanAfford(upgradePrice2[upgradeLevel - 1]))
                    {
                        money.ReduceMoney(upgradePrice2[upgradeLevel - 1]);
                        int newProgresTime = Mathf.RoundToInt(timePerLevel[upgradeLevel - 1]);
                        workspace.UpdateProgresTime(newProgresTime);
                        currentUpgradeLevel[workspaceIndex] = upgradeLevel;
                        workspace.level_workspace = upgradeLevel;
                        Debug.Log($"Workspace {workspaceIndex + 1} upgraded to level {upgradeLevel} with new progress time: {newProgresTime} seconds.");
                    }
                    else
                    {
                        Debug.LogWarning("Not enough money to upgrade workspace.");
                        return;
                    }
                }
                else if(workspaceIndex == 1)
                {
                    if (money.CanAfford(upgradePrice2[upgradeLevel - 1]))
                    {
                        money.ReduceMoney(upgradePrice2[upgradeLevel - 1]);
                        int newProgresTime = Mathf.RoundToInt(timePerLevel[upgradeLevel - 1]);
                        workspace.UpdateProgresTime(newProgresTime);
                        currentUpgradeLevel[workspaceIndex] = upgradeLevel;
                        workspace.level_workspace = upgradeLevel;
                        Debug.Log($"Workspace {workspaceIndex + 1} upgraded to level {upgradeLevel} with new progress time: {newProgresTime} seconds.");
                    }
                    else
                    {
                        Debug.LogWarning("Not enough money to upgrade workspace.");
                        return;
                    }
                }
                else if(workspaceIndex == 2)
                {
                    if (money.CanAfford(upgradePrice3[upgradeLevel - 1]))
                    {
                        money.ReduceMoney(upgradePrice3[upgradeLevel - 1]);
                        int newProgresTime = Mathf.RoundToInt(timePerLevel[upgradeLevel - 1]);
                        workspace.UpdateProgresTime(newProgresTime);
                        currentUpgradeLevel[workspaceIndex] = upgradeLevel;
                        workspace.level_workspace = upgradeLevel;
                        Debug.Log($"Workspace {workspaceIndex + 1} upgraded to level {upgradeLevel} with new progress time: {newProgresTime} seconds.");
                    }
                    else
                    {
                        Debug.LogWarning("Not enough money to upgrade workspace.");
                        return;
                    }
                }
                else if(workspaceIndex == 3)
                {
                    if (money.CanAfford(upgradePrice4[upgradeLevel - 1]))
                    {
                        money.ReduceMoney(upgradePrice4[upgradeLevel - 1]);
                        int newProgresTime = Mathf.RoundToInt(timePerLevel[upgradeLevel - 1]);
                        workspace.UpdateProgresTime(newProgresTime);
                        currentUpgradeLevel[workspaceIndex] = upgradeLevel;
                        workspace.level_workspace = upgradeLevel;
                        Debug.Log($"Workspace {workspaceIndex + 1} upgraded to level {upgradeLevel} with new progress time: {newProgresTime} seconds.");
                    }
                    else
                    {
                        Debug.LogWarning("Not enough money to upgrade workspace.");
                        return;
                    }
                }
                else if(workspaceIndex == 4)
                {
                    if (money.CanAfford(upgradePrice5[upgradeLevel - 1]))
                    {
                        money.ReduceMoney(upgradePrice5[upgradeLevel - 1]);
                        int newProgresTime = Mathf.RoundToInt(timePerLevel[upgradeLevel - 1]);
                        workspace.UpdateProgresTime(newProgresTime);
                        currentUpgradeLevel[workspaceIndex] = upgradeLevel;
                        workspace.level_workspace = upgradeLevel;
                        Debug.Log($"Workspace {workspaceIndex + 1} upgraded to level {upgradeLevel} with new progress time: {newProgresTime} seconds.");
                    }
                    else
                    {
                        Debug.LogWarning("Not enough money to upgrade workspace.");
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
}