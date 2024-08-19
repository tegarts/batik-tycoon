using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Workspace: MonoBehaviour
{
    public int progresTime;
    public bool isStartAuto;
    public bool isOnProgress;
    public bool isDone;
    public Slider progresBar;
    public GameObject AssignArea;
    private void Start()
    {
        if(progresBar == null)
        {
            Debug.LogError("Progres Bar di Workernya kosong coi");
        }
        else
        {
            progresBar.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        
            if(isStartAuto)
            {
                isStartAuto = false;
                StartCoroutine(StartAutomation());
            }
           
    }    

    IEnumerator StartAutomation()
    {
        isOnProgress = true;
        float waitTime = progresTime;
        float elapsedTime = 0f;

        progresBar.value = 0f;
        progresBar.gameObject.SetActive(true);

        while(elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;
            progresBar.value = Mathf.Clamp01(elapsedTime / waitTime);
            yield return null;
        }
        Debug.Log("done bang");
        progresBar.gameObject.SetActive(false);
        isDone = true;
        isOnProgress = false;
    }

    public void SetupProgresBar(Canvas canvas, Camera camera)
    {
        progresBar.transform.SetParent(canvas.transform);
        if(progresBar.TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
        {
            faceCamera.Camera = camera;
        }
    }

    public void SetupAssignArea(Canvas canvas, Camera camera)
    {
        AssignArea.transform.SetParent(canvas.transform);
        if(AssignArea.TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
        {
            faceCamera.Camera = camera;
        }
    }

}
