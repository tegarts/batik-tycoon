using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    public GameObject[] machine1;
    public GameObject[] machine2;
    public GameObject[] machine3;
    public GameObject[] machine4;
    public GameObject[] machine5;
    public Machine[] machines;

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
        if(machines[0].level == 3)
        {
            machine1[0].SetActive(true);
        }
        else if(machines[0].level == 4)
        {
            machine1[0].SetActive(true);
            machine1[1].SetActive(true);
        }
        else if(machines[0].level == 5)
        {
            machine1[0].SetActive(true);
            machine1[1].SetActive(true);
            machine1[2].SetActive(true);
        }

        // Mesin Mencanting
        if(machines[1].level == 3)
        {
            machine2[0].SetActive(true);
        }
        else if(machines[1].level == 4)
        {
            machine2[0].SetActive(true);
            machine2[1].SetActive(true);
        }
        else if(machines[1].level == 5)
        {
            machine2[0].SetActive(true);
            machine2[1].SetActive(true);
            machine2[2].SetActive(true);
        }

        // Mesin Mewarnai
        if(machines[2].level == 3)
        {
            machine3[0].SetActive(true);
        }
        else if(machines[2].level == 4)
        {
            machine3[0].SetActive(true);
            machine3[1].SetActive(true);
        }
        else if(machines[2].level == 5)
        {
            machine3[0].SetActive(true);
            machine3[1].SetActive(true);
            machine3[2].SetActive(true);
        }

        // Mesin Menjemur
        if(machines[3].level == 3)
        {
            machine4[0].SetActive(true);
        }
        else if(machines[3].level == 4)
        {
            machine4[0].SetActive(true);
            machine4[1].SetActive(true);
        }
        else if(machines[3].level == 5)
        {
            machine4[0].SetActive(true);
            machine4[1].SetActive(true);
            machine4[2].SetActive(true);
        }

        // Mesin Menglodor
        if(machines[4].level == 3)
        {
            machine5[0].SetActive(true);
        }
        else if(machines[4].level == 4)
        {
            machine5[0].SetActive(true);
            machine5[1].SetActive(true);
        }
        else if(machines[4].level == 5)
        {
            machine5[0].SetActive(true);
            machine5[1].SetActive(true);
            machine5[2].SetActive(true);
        }
    }

}
