using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWorkerManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Canvas canvasWorldSpace;
    [SerializeField] private GameObject worker1;

    private void Start() 
    {
        worker1.GetComponent<WorkerAutomation>().SetupProgresBar(canvasWorldSpace, cam);
    }
}
