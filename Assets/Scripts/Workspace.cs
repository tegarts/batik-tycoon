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
    public GameObject upgradeButton;
    public GameObject progresImage;
    Money money;
    public delegate void WorkspaceEvent();
    public event WorkspaceEvent OnWorkspaceCompleted;
    public int level_workspace; // TODO - baca dari data player
    [SerializeField] GameObject vfx_workspace;
    Tutorial tutorial;
    [SerializeField] GameObject[] workers;
    [SerializeField] GameObject[] vfxAngin;
    private bool[] isAnginEnabled;
    DayManager dayManager;
    bool isStartDay;
    AudioSetter audioSetter;

    private void Awake()
    {
        audioSetter = GameObject.FindWithTag("Audio").GetComponent<AudioSetter>();
        dayManager = FindObjectOfType<DayManager>();
    }

    public void LoadData(GameData data)
    {
        if (workspaceNumber == 1)
        {
            if (data.level_workspace[0] == 0)
            {
                data.level_workspace[0] = 1;
            }

            level_workspace = data.level_workspace[0];
        }
        else if (workspaceNumber == 2)
        {
            if (data.level_workspace[1] == 0)
            {
                data.level_workspace[1] = 1;
            }

            level_workspace = data.level_workspace[1];
        }
        else if (workspaceNumber == 3)
        {
            if (data.level_workspace[2] == 0)
            {
                data.level_workspace[2] = 1;
            }

            level_workspace = data.level_workspace[2];
        }
        else if (workspaceNumber == 4)
        {
            if (data.level_workspace[3] == 0)
            {
                data.level_workspace[3] = 1;
            }

            level_workspace = data.level_workspace[3];
        }
        else if (workspaceNumber == 5)
        {
            if (data.level_workspace[4] == 0)
            {
                data.level_workspace[4] = 1;
            }

            level_workspace = data.level_workspace[4];
        }
    }

    public void SaveData(ref GameData data)
    {
        if (workspaceNumber == 1)
        {
            data.level_workspace[0] = level_workspace;
        }
        else if (workspaceNumber == 2)
        {
            data.level_workspace[1] = level_workspace;
        }
        else if (workspaceNumber == 3)
        {
            data.level_workspace[2] = level_workspace;
        }
        else if (workspaceNumber == 4)
        {
            data.level_workspace[3] = level_workspace;
        }
        else if (workspaceNumber == 5)
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


        for (int i = 0; i < workers.Length; i++)
        {
            workers[i].SetActive(false);
            vfxAngin[i].SetActive(false);
        }

        isAnginEnabled = new bool[vfxAngin.Length];

        workers[0].SetActive(true);

        money = FindObjectOfType<Money>();
        if (level_workspace == 1)
        {
            progresTime = 12;
        }
        else if (level_workspace == 2)
        {
            progresTime = 10;
        }
        else if (level_workspace == 3)
        {
            progresTime = 8;
        }
        else if (level_workspace == 4)
        {
            progresTime = 6;
        }
        else if (level_workspace == 5)
        {
            progresTime = 4;
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

        if (!dayManager.dayIsStarted)
        {
            if (!tutorial.isStartTutor)
            {
                workers[0].SetActive(false);
                isStartDay = false;
                AssignArea.SetActive(false);
                upgradeButton.SetActive(true);
            }
            else
            {
                upgradeButton.SetActive(false);
            }

        }
        else
        {
            if (!isStartDay)
            {
                workers[0].SetActive(true);
                upgradeButton.SetActive(false);
                AssignArea.SetActive(true);
                isStartDay = true;
            }
        }

    }

    IEnumerator StartAutomation()
    {
        isOnProgress = true;
        float waitTime;
        if (tutorial.isStartTutor)
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

            if (elapsedTime < enableNPC)
            {
                workers[0].SetActive(false);
                if (!isAnginEnabled[0])
                {
                    StartCoroutine(EnableWind(0));
                    audioSetter.PlaySFX(audioSetter.angin);
                    isAnginEnabled[0] = true;
                    audioSetter.PlaySFX(audioSetter.desain);
                }
                workers[1].SetActive(true);

            }
            else if (elapsedTime < enableNPC1) // 3+
            {
                workers[1].SetActive(false);
                if (!isAnginEnabled[1])
                {
                    audioSetter.StopSFX();
                    StartCoroutine(EnableWind(1));
                    audioSetter.PlaySFX(audioSetter.angin);
                    isAnginEnabled[1] = true;
                    audioSetter.PlaySFX(audioSetter.canting);
                }
                workers[2].SetActive(true);

            }
            else if (elapsedTime < enableNPC2) // 6+
            {
                workers[2].SetActive(false);
                if (!isAnginEnabled[2])
                {
                    audioSetter.StopSFX();
                    StartCoroutine(EnableWind(2));
                    audioSetter.PlaySFX(audioSetter.angin);
                    isAnginEnabled[2] = true;
                    audioSetter.PlaySFX(audioSetter.mewarnai);
                }
                workers[3].SetActive(true);

            }
            else if (elapsedTime < enableNPC3) // 9+
            {
                workers[3].SetActive(false);
                if (!isAnginEnabled[3])
                {
                    audioSetter.StopSFX();
                    StartCoroutine(EnableWind(3));
                    audioSetter.PlaySFX(audioSetter.angin);
                    isAnginEnabled[3] = true;
                    audioSetter.PlaySFX(audioSetter.menjemur);
                }
                workers[4].SetActive(true);

            }
            else if (elapsedTime >= enableNPC3 && elapsedTime <= waitTime) // 12
            {
                workers[4].SetActive(false);
                if (!isAnginEnabled[4])
                {
                    audioSetter.StopSFX();
                    StartCoroutine(EnableWind(4));
                    audioSetter.PlaySFX(audioSetter.angin);
                    isAnginEnabled[4] = true;
                    audioSetter.PlaySFX(audioSetter.lorod);
                }
                workers[5].SetActive(true);

            }
            else
            {
                workers[5].SetActive(false);
                if (!isAnginEnabled[5])
                {
                    audioSetter.StopSFX();
                    StartCoroutine(EnableWind(5));
                    isAnginEnabled[5] = true;
                }
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
        audioSetter.StopSFX();
        if (!isAnginEnabled[5])
        {
            StartCoroutine(EnableWind(5));
            audioSetter.PlaySFX(audioSetter.angin);
            isAnginEnabled[5] = true;
        }

        for (int i = 0; i < isAnginEnabled.Length; i++)
        {
            isAnginEnabled[i] = false;
        }

        vfx_workspace.SetActive(false);
    }

    IEnumerator EnableWind(int windIndex)
    {
        vfxAngin[windIndex].SetActive(true);
        yield return new WaitForSeconds(0.25f);
        vfxAngin[windIndex].SetActive(false);
    }

    public void SetupAssignArea(Canvas canvas, Camera camera)
    {
        AssignArea.transform.SetParent(canvas.transform);
        if (AssignArea.TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
        {
            faceCamera.Camera = camera;
        }
    }

    public void SetupUpgradeButton(Canvas canvas, Camera camera)
    {
        upgradeButton.transform.SetParent(canvas.transform);
        if (TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
        {
            faceCamera.Camera = camera;
        }
    }

    public void UpdateProgresTime(int newProgresTime)
    {
        progresTime = newProgresTime;
    }
}
