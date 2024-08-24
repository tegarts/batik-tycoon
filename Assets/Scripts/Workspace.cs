using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Workspace : MonoBehaviour
{
    public int progresTime;
    public bool isStartAuto;
    public bool isOnProgress;
    public bool isDone;
    public GameObject AssignArea;
    public GameObject progresImage;
    Money money;
    public delegate void WorkspaceEvent();
    public event WorkspaceEvent OnWorkspaceCompleted;
    public int level_workspace = 1;

    private void Start()
    {
        if(progresImage != null)
        {
            progresImage.SetActive(false);
        }

        money = FindObjectOfType<Money>();
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
        progresImage.GetComponent<Image>().fillAmount = 1;
        progresImage.SetActive(true);

        while (elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;
            progresImage.GetComponent<Image>().fillAmount = Mathf.Clamp01(1 - (elapsedTime / waitTime));
            yield return null;
        }
        Debug.Log("done bang");
        progresImage.SetActive(false);
        OnWorkspaceCompleted?.Invoke();
        isDone = true;
        isOnProgress = false;
        money.AddMoney(400000);
    }

    public void SetupAssignArea(Canvas canvas, Camera camera)
    {
        AssignArea.transform.SetParent(canvas.transform);
        if (AssignArea.TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
        {
            faceCamera.Camera = camera;
        }
    }

    public void UpdateProgresTime(int newProgresTime)
    {
        progresTime = newProgresTime;
    }
}
