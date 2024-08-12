using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWorkerManager : MonoBehaviour
{
    // TODO - Ini kemungkinan pindah ke UIManager
    [SerializeField] private Camera cam;
    [SerializeField] private Canvas canvasWorldSpace;
    [SerializeField] private GameObject[] workerKawung;
    [SerializeField] private GameObject[] workerMega;

    private void Start() 
    {
        workerKawung[0].GetComponent<WorkerAutomation>().SetupProgresBar(canvasWorldSpace, cam);
        workerKawung[0].GetComponent<WorkerAutomation>().SetupAssignArea(canvasWorldSpace, cam);

        workerKawung[1].GetComponent<WorkerAutomation>().SetupProgresBar(canvasWorldSpace, cam);
        workerKawung[1].GetComponent<WorkerAutomation>().SetupAssignArea(canvasWorldSpace, cam);

        workerMega[0].GetComponent<WorkerAutomation>().SetupProgresBar(canvasWorldSpace, cam);
        workerMega[0].GetComponent<WorkerAutomation>().SetupAssignArea(canvasWorldSpace, cam);

        workerMega[1].GetComponent<WorkerAutomation>().SetupProgresBar(canvasWorldSpace, cam);
        workerMega[1].GetComponent<WorkerAutomation>().SetupAssignArea(canvasWorldSpace, cam);
    }
}
