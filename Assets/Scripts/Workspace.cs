using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Workspace : MonoBehaviour, IDataPersistence
{
    [Header("Workspace Number")]
    [SerializeField] int workspaceNumber;

    [Header("Other")]
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
    public int level_workspace; // TODO - baca dari data player
    [SerializeField] GameObject vfx_workspace;
    Tutorial tutorial;
    [SerializeField] GameObject[] workers;

    public void LoadData(GameData data)
    {
        if(workspaceNumber == 1)
        {
            level_workspace = data.level_workspace[0];
        }
        else if(workspaceNumber == 2)
        {
            level_workspace = data.level_workspace[1];
        }
        else if(workspaceNumber == 3)
        {
            level_workspace = data.level_workspace[2];
        }
        else if(workspaceNumber == 4)
        {
            level_workspace = data.level_workspace[3];
        }
        else if(workspaceNumber == 5)
        {
            level_workspace = data.level_workspace[4];
        }
    }

    public void SaveData(ref GameData data)
    {
        if(workspaceNumber == 1)
        {
            data.level_workspace[0] = level_workspace;
        }
        else if(workspaceNumber == 2)
        {
            data.level_workspace[1] = level_workspace;
        }
        else if(workspaceNumber == 3)
        {
            data.level_workspace[2] = level_workspace;
        }
        else if(workspaceNumber == 4)
        {
            data.level_workspace[3] = level_workspace;
        }
        else if(workspaceNumber == 5)
        {
            data.level_workspace[4] = level_workspace;
        }
    }

    private void Start()
    {
        if (progresImage != null)
        {
            progresImage.SetActive(false);
        }

        if(level_workspace == 0)
        {
            level_workspace = 1;
        }

        for(int i = 0; i < workers.Length; i++)
        {
            workers[i].SetActive(false);
        }

        workers[0].SetActive(true);

        money = FindObjectOfType<Money>();
        if (level_workspace == 1)
        {
            progresTime = 14;
        }
        else if (level_workspace == 2)
        {
            progresTime = 12;
        }
        else if (level_workspace == 3)
        {
            progresTime = 10;
        }
        else if (level_workspace == 4)
        {
            progresTime = 8;
        }
        else if (level_workspace == 5)
        {
            progresTime = 6;
        }

        vfx_workspace.SetActive(false);

        tutorial = FindObjectOfType<Tutorial>();
        
    }

    private void Update()
    {
        if (isStartAuto)
        {
            isStartAuto = false;
            StartCoroutine(StartAutomation());
        }

    }

    IEnumerator StartAutomation()
    {
        isOnProgress = true;
        float waitTime;
        if(tutorial.isStartTutor)
        {
            waitTime = 6;
        }
        else
        {
            waitTime = progresTime;
        }
        float elapsedTime = 0f;
        progresImage.GetComponent<Image>().fillAmount = 1;
        progresImage.SetActive(true);

        vfx_workspace.SetActive(true);

        while (elapsedTime < waitTime)
        {
            float enableNPC = waitTime / 5;
            float enableNPC1 = enableNPC * 2;
            float enableNPC2 = enableNPC * 3;
            float enableNPC3 = enableNPC * 4;

            if(elapsedTime < enableNPC)
            {
                workers[0].SetActive(false);
                workers[1].SetActive(true);
            }
            else if(elapsedTime < enableNPC1) // 3+
            {
                workers[1].SetActive(false);
                workers[2].SetActive(true);
            }
            else if(elapsedTime < enableNPC2) // 6+
            {
                workers[2].SetActive(false);
                workers[3].SetActive(true);
            }
            else if(elapsedTime < enableNPC3) // 9+
            {
                workers[3].SetActive(false);
                workers[4].SetActive(true);
            }
            else if(elapsedTime >= enableNPC3 && elapsedTime <= waitTime) // 12
            {
                workers[4].SetActive(false);
                workers[5].SetActive(true);
            }
            else
            {
                workers[5].SetActive(false);
                workers[0].SetActive(true);
            }

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

        workers[5].SetActive(false);
        workers[0].SetActive(true);

        vfx_workspace.SetActive(false);
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
