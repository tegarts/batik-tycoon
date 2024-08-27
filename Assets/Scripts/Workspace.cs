using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Workspace : MonoBehaviour
{
    public int progresTime;
    [SerializeField] int batikPrice;
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
        if(level_workspace == 1)
        {
            progresTime = 15;
        }
        else if(level_workspace == 2)
        {
            progresTime = 13;
        }
        else if(level_workspace == 3)
        {
            progresTime = 11;
        }
        else if(level_workspace == 4)
        {
            progresTime = 9;
        }
        else if(level_workspace == 5)
        {
            progresTime = 7;
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
        money.AddMoney(batikPrice);
        Daily.instance.dailyIncome += batikPrice;
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
