using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
using Unity.VisualScripting;

public class Tutorial : MonoBehaviour, IDataPersistence
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public bool isStartTutor;
    public bool isOpenUpgrade;
    public bool isAlreadyTutor;
    public GameObject panelTutorial;
    [SerializeField] GameObject panelSkipConfirmation;
    public bool[] isStepDone;
    public bool isNpcIn;
    bool isTyping;

    [Header("References")]
    DrawingManager drawingManager;
    [SerializeField] Workspace workspace;
    WorkspaceManager workspaceManager;
    BookMenu bookMenu;
    DayManager dayManager;
    StartDay startDay;
    public int index;
    [Header("UI References")]
    [SerializeField] GameObject PlayerArea;
    [SerializeField] GameObject playerChildArea;
    [SerializeField] GameObject WorkerArea;
    [SerializeField] GameObject workerChildArea;
    [SerializeField] Animator animPanelTutor;
    [SerializeField] Animator animPopUp;
    [SerializeField] GameObject buttonStartDay;
    [SerializeField] GameObject imagePortrait;
    [SerializeField] Sprite[] spritePortrait;

    [SerializeField] GameObject highlightButtonBook;
    [SerializeField] GameObject highlightButtonStartHUD;
    [SerializeField] GameObject highlightButtonUnlock;
    [SerializeField] GameObject highlightButtonWS;
    [SerializeField] GameObject highlightButtonStartDay;
    [Header("Audio")]
    AudioSetter audioSetter;
    private void Awake()
    {
        audioSetter = GameObject.FindWithTag("Audio").GetComponent<AudioSetter>();
    }

    public void LoadData(GameData data)
    {
        isAlreadyTutor = data.isAlreadyTutor;
    }

    public void SaveData(ref GameData data)
    {
        data.isAlreadyTutor = isAlreadyTutor;
    }

    private void Start()
    {
        highlightButtonBook.SetActive(false);
        highlightButtonUnlock.SetActive(false);
        highlightButtonWS.SetActive(false);
        highlightButtonStartDay.SetActive(false);
        // indicatorUpgrade.SetActive(false);
        // indicatorMoney.SetActive(false);

        drawingManager = FindAnyObjectByType<DrawingManager>();
        workspaceManager = FindAnyObjectByType<WorkspaceManager>();
        bookMenu = FindAnyObjectByType<BookMenu>();
        dayManager = FindAnyObjectByType<DayManager>();
        startDay = FindAnyObjectByType<StartDay>();

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
                    if (isTyping)
                    {
                        StopAllCoroutines();
                        textComponent.text = lines[index];
                        isTyping = false;
                    }
                    else
                    {
                        NextLine();
                    }
                    // if (textComponent.text == lines[index])
                    // {
                    //     NextLine();
                    // }
                    // else
                    // {
                    //     StopAllCoroutines();
                    //     textComponent.text = lines[index];
                    //     isInputNotAllowed = false;
                    // }
                }
            }
        }

        if (isStartTutor)
        {
            if (drawingManager.canvasCanting.activeSelf && !isStepDone[0])
            {
                index = 5;
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

        if (isStartTutor)
        {
            if (index == 9)
            {
                if (bookMenu.bookPanel.activeSelf && !bookMenu.unlockPanel.activeSelf)
                {
                    Debug.Log("SANTOEHUNATOEHUSNAEOU");
                    highlightButtonBook.SetActive(false);
                    highlightButtonUnlock.SetActive(true);
                }
                if (bookMenu.unlockPanel.activeSelf)
                {
                    Debug.Log("ZZZZZZZZZZZZZZZZZZz");
                    highlightButtonUnlock.SetActive(false);
                    highlightButtonWS.SetActive(true);
                }
            }
        }
        else if(isAlreadyTutor && dayManager.day == 0)
        {
            if(!startDay.startDayPanel.activeSelf)
            {
                highlightButtonStartHUD.SetActive(true);
            }
            else
            {
                highlightButtonStartDay.SetActive(true);
                highlightButtonStartHUD.SetActive(false);
            }
        }
        else
        {
            highlightButtonWS.SetActive(false);
        }
    }

    void StartDialogue()
    {
        //index = 0;
        StartCoroutine(TypeLine());
    }


    IEnumerator TypeLine()
    {
        isTyping = true;
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;
    }

    void NextLine()
    {

        if (index < 2)
        {
            if (index == 0)
            {
                StartCoroutine(ChangeSprite(2));
            }
            else if (index == 1)
            {
                StartCoroutine(ChangeSprite(1));
            }

            buttonStartDay.SetActive(false);
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else if (index == 2)
        {
            StartCoroutine(ChangeSprite(3));
            Debug.Log("NPC Pembeli Masuk");
            isNpcIn = true;
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else if (index == 3)
        {
            StartCoroutine(ChangeSprite(2));
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());

        }
        else if (index == 4)
        {
            StartCoroutine(ChangeSprite(5));
            index++;
            PlayerArea.GetComponent<Image>().enabled = true;
            playerChildArea.GetComponent<Image>().enabled = true;
            CloseTutorial();
        }
        else if (index <= 6)
        {
            if (index == 5)
            {
                StartCoroutine(ChangeSprite(4));
            }
            else if (index == 6)
            {
                StartCoroutine(ChangeSprite(1));
            }
            index++;
            CloseTutorial();
        }
        else if (index == 7)
        {
            StartCoroutine(ChangeSprite(2));
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else if (index == 8)
        {
            StartCoroutine(ChangeSprite(4));
            index++;
            CloseTutorial();
            highlightButtonBook.SetActive(true);

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

    IEnumerator ChangeSprite(int dex)
    {
        animPanelTutor.SetTrigger("IsPortrait");
        yield return new WaitForSeconds(0.25f);
        imagePortrait.GetComponent<Image>().sprite = spritePortrait[dex];
    }

    public void SkipOpenConfirmation()
    {
        audioSetter.PlaySFX(audioSetter.OpenPanel);
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
        audioSetter.PlaySFX(audioSetter.ClosePanel);
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
