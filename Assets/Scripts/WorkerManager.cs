using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : MonoBehaviour, IDataPersistence
{
    public GameObject[] machine1;
    public GameObject[] machine2;
    public GameObject[] machine3;
    public GameObject[] machine4;
    public GameObject[] machine5;
    private int[] levelMachines = new int[5];
    
    public void LoadData(GameData data)
    {
        levelMachines[0] = data.machineLevel1;
        levelMachines[1] = data.machineLevel2;
        levelMachines[2] = data.machineLevel3;
        levelMachines[3] = data.machineLevel4;
        levelMachines[4] = data.machineLevel5;
    }

    public void SaveData(ref GameData data)
    {
        
    }

    private void Start() 
    {
        for(int i = 0; i < machine1.Length; i++)
        {
            machine1[i].SetActive(false);
            machine2[i].SetActive(false);
            machine3[i].SetActive(false);
            machine4[i].SetActive(false);
            machine5[i].SetActive(false);
        }

        EnableMachines();
    }

    private void Update() {
        
    }


    public void EnableMachines()
    {
        // Mesin Mendesain
        if(levelMachines[0] == 3)
        {
            machine1[0].SetActive(true);
        }
        else if(levelMachines[0] == 4)
        {
            machine1[0].SetActive(true);
            machine1[1].SetActive(true);
        }
        else if(levelMachines[0] == 5)
        {
            machine1[0].SetActive(true);
            machine1[1].SetActive(true);
            machine1[2].SetActive(true);
        }

        // Mesin Mencanting
        if(levelMachines[1] == 3)
        {
            machine2[0].SetActive(true);
        }
        else if(levelMachines[0] == 4)
        {
            machine2[0].SetActive(true);
            machine2[1].SetActive(true);
        }
        else if(levelMachines[0] == 5)
        {
            machine2[0].SetActive(true);
            machine2[1].SetActive(true);
            machine2[2].SetActive(true);
        }

        // Mesin Mewarnai
        if(levelMachines[2] == 3)
        {
            machine3[0].SetActive(true);
        }
        else if(levelMachines[2] == 4)
        {
            machine3[0].SetActive(true);
            machine3[1].SetActive(true);
        }
        else if(levelMachines[2] == 5)
        {
            machine3[0].SetActive(true);
            machine3[1].SetActive(true);
            machine3[2].SetActive(true);
        }

        // Mesin Menjemur
        if(levelMachines[3] == 3)
        {
            machine4[0].SetActive(true);
        }
        else if(levelMachines[3] == 4)
        {
            machine4[0].SetActive(true);
            machine4[1].SetActive(true);
        }
        else if(levelMachines[3] == 5)
        {
            machine4[0].SetActive(true);
            machine4[1].SetActive(true);
            machine4[2].SetActive(true);
        }

        // Mesin Menglodor
        if(levelMachines[4] == 3)
        {
            machine5[0].SetActive(true);
        }
        else if(levelMachines[4] == 4)
        {
            machine5[0].SetActive(true);
            machine5[1].SetActive(true);
        }
        else if(levelMachines[4] == 5)
        {
            machine5[0].SetActive(true);
            machine5[1].SetActive(true);
            machine5[2].SetActive(true);
        }
    }

}
