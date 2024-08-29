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
    [SerializeField] GameObject playerChildArea;
    [SerializeField] GameObject WorkerArea;
    [SerializeField] GameObject workerChildArea;
    [SerializeField] Animator animPanelTutor;
    [SerializeField] Animator animPopUp;
    [SerializeField] GameObject buttonStartDay;

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
            playerChildArea.GetComponent<Image>().enabled = false;
            WorkerArea.GetComponent<Image>().enabled = false;
            workerChildArea.GetComponent<Image>().enabled = false;
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
                playerChildArea.GetComponent<Image>().enabled = false;
                WorkerArea.GetComponent<Image>().enabled = true;
                workerChildArea.GetComponent<Image>().enabled = true;
                
            }
            else if (workspace.isDone && !isStepDone[2])
            {
                panelTutorial.SetActive(true);
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
                isStepDone[2] = true;
                PlayerArea.GetComponent<Image>().enabled = true;
                playerChildArea.GetComponent<Image>().enabled = true;
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
            buttonStartDay.SetActive(false);
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
            playerChildArea.GetComponent<Image>().enabled = true;
            CloseTutorial();
        }
        else if (index <= 6)
        {
            index++;
            CloseTutorial();
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
            CloseTutorial();
            isStepDone[4] = true;
        }
        else if (index == 9)
        {
            CloseTutorial();
            buttonStartDay.SetActive(true);
            isAlreadyTutor = true;
            isStartTutor = false;
        }
        
    }

    public void SkipOpenConfirmation()
    {
        panelSkipConfirmation.SetActive(true);
    }

    public void SkipCloseConfirmation()
    {
        buttonStartDay.SetActive(true);
        StartCoroutine(ClosePopUp());
    }

    public void SkipTutorial()
    {
        SkipCloseConfirmation();
        CloseTutorial();
        PlayerArea.GetComponent<Image>().enabled = true;
        playerChildArea.GetComponent<Image>().enabled = true;
        WorkerArea.GetComponent<Image>().enabled = true;
        workerChildArea.GetComponent<Image>().enabled = true;
        isStartTutor = false;
        isAlreadyTutor = true;
    }

    private void CloseTutorial()
    {
        StartCoroutine(CloseTutorialAnim());
    }

    IEnumerator CloseTutorialAnim()
    {
        animPanelTutor.SetTrigger("IsEnd");
        yield return new WaitForSeconds(0.25f);
        panelTutorial.SetActive(false);
    }

    IEnumerator ClosePopUp()
    {
        animPopUp.SetTrigger("IsEnd");
        yield return new WaitForSeconds(0.25f);
        panelSkipConfirmation.SetActive(false);
    }
}
