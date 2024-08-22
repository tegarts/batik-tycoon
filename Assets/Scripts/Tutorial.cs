using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

public class Tutorial : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public bool isStartTutor;
    public bool isOpenUpgrade;
    public bool isAlreadyTutor; // TODO - tambahin idatapersistence buat simpen ini
    public GameObject indicatorUpgrade;
    [SerializeField] GameObject indicatorMoney;
    public GameObject panelTutorial;
    [SerializeField] GameObject panelSkipConfirmation;
    public bool[] isStepDone;
    public bool isNpcIn;

    [Header("References")]
    DrawingManager drawingManager;
    [SerializeField] Workspace workspace;
    WorkspaceManager workspaceManager;
    public int index;
    [Header("UI References")]
    [SerializeField] GameObject PlayerArea;
    [SerializeField] GameObject WorkerArea;

    private void Start()
    {
        // indicatorUpgrade.SetActive(false);
        // indicatorMoney.SetActive(false);

        drawingManager = FindAnyObjectByType<DrawingManager>();
        workspaceManager = FindAnyObjectByType<WorkspaceManager>();

        panelSkipConfirmation.SetActive(false);

        if (!isAlreadyTutor)
        {
            isStartTutor = true;
            panelTutorial.SetActive(true);
            PlayerArea.GetComponent<Image>().enabled = false;
            WorkerArea.GetComponent<Image>().enabled = false;;
        }
        else
        {

        }
        textComponent.text = string.Empty;
        StartDialogue();
    }

    private void Update()
    {
        if (panelTutorial.activeSelf)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                if (!panelSkipConfirmation.activeSelf)
                {
                    if (textComponent.text == lines[index])
                    {
                        NextLine();
                    }
                    else
                    {
                        StopAllCoroutines();
                        textComponent.text = lines[index];
                    }
                }
            }
        }

        if (isStartTutor)
        {
            if (drawingManager.canvasCanting.activeSelf && !isStepDone[0])
            {
                panelTutorial.SetActive(true);
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
                isStepDone[0] = true;
            }
            else if (!drawingManager.canvasCanting.activeSelf && isStepDone[0] && !isStepDone[1])
            {
                panelTutorial.SetActive(true);
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
                isStepDone[1] = true;
                isNpcIn = true;
                PlayerArea.GetComponent<Image>().enabled = false;
                WorkerArea.GetComponent<Image>().enabled = true;
                
            }
            else if (workspace.isDone && !isStepDone[2])
            {
                panelTutorial.SetActive(true);
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
                isStepDone[2] = true;
                PlayerArea.GetComponent<Image>().enabled = true;
            }

            if (workspaceManager.motifUnlocked == 1 && !isStepDone[3])
            {
                panelTutorial.SetActive(true);
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
                isStepDone[3] = true;
            }
        }
    }

    void StartDialogue()
    {
        //index = 0;
        StartCoroutine(TypeLine());
    }


    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < 2)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else if (index == 2)
        {
            Debug.Log("NPC Pembeli Masuk");
            isNpcIn = true;
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else if (index == 3)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
            
        }
        else if(index == 4)
        {
            index++;
            PlayerArea.GetComponent<Image>().enabled = true;
            panelTutorial.SetActive(false);
        }
        else if (index <= 6)
        {
            index++;
            panelTutorial.SetActive(false);
        }
        else if (index == 7)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else if (index == 8)
        {
            index++;
            panelTutorial.SetActive(false);
            isStepDone[4] = true;
        }
        else if (index == 9)
        {
            panelTutorial.SetActive(false);
            isAlreadyTutor = true;
            isStartTutor = false;
        }
        // else if (index == 5)
        // {
        //     index++;
        //     panelTutorial.SetActive(false);
        // }
        // else if(index == 6)
        // {
        //     index++;
        //     panelTutorial.SetActive(false);
        // }
        // else if(index == 6)
        // {
        //     index++;
        //     textComponent.text = string.Empty;
        //     indicatorMoney.SetActive(false);
        //     indicatorUpgrade.SetActive(true);
        //     isOpenUpgrade = true;
        //     StartCoroutine(TypeLine());
        // }
        // else if(index == 7)
        // {
        //     index++;
        //     gameObject.SetActive(false);
        // }
        // else if(index == 8)
        // {
        //     index++;
        //     textComponent.text = string.Empty;
        //     StartCoroutine(TypeLine());

        // }
        // else if(index == 9)
        // {
        //     gameObject.SetActive(false);
        //     isStartTutor = false;
        // }
    }

    public void SkipOpenConfirmation()
    {
        panelSkipConfirmation.SetActive(true);
    }

    public void SkipCloseConfirmation()
    {
        panelSkipConfirmation.SetActive(false);
    }

    public void SkipTutorial()
    {
        panelTutorial.SetActive(false);
        panelSkipConfirmation.SetActive(false);
        isStartTutor = false;
        isAlreadyTutor = true;
    }
}
