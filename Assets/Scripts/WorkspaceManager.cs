using System.Collections;
using System.Collections.Generic;
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
        EnableWorkspace();
    }

    private void EnableWorkspace()
    {
        // for (int i = 0; i < workspaces.Length; i++)
        // {
        //     workspaces[i].SetActive(false);
        // }
        for (int i = 0; i < motifUnlocked + 1; i++)
        {
            workspaces[i].SetActive(true);
            workspaces[i].GetComponent<Workspace>().SetupProgresBar(canvasWorldSpace, cam);
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
                Debug.Log("set button uninteracable atau semacamnya,karna player udah unlock motif ini");
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
                Debug.Log("set button uninteracable atau semacamnya,karna player udah unlock motif ini");
            }

        }
        if (level == 4)
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
                Debug.Log("set button uninteracable atau semacamnya,karna player udah unlock motif ini");
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
                Debug.Log("set button uninteracable atau semacamnya,karna player udah unlock motif ini");
            }

        }

    }
}
