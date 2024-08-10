using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WorkerAutomation : MonoBehaviour
{
    public int progresTime;
    private bool isStartAuto;
    public Slider progresBar;
    private void Start()
    {
        if(progresBar == null)
        {
            Debug.LogError("Progres Bar di Workernya kosong coi");
        }
        else
        {
            // progresBar.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(!isStartAuto)
            {
                isStartAuto = true;
                StartCoroutine(StartAutomation());
            }
        }   
    }    

    IEnumerator StartAutomation()
    {
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
    }

    public void SetupProgresBar(Canvas canvas, Camera camera)
    {
        progresBar.transform.SetParent(canvas.transform);
        if(progresBar.TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
        {
            faceCamera.Camera = camera;
        }
    }

}
