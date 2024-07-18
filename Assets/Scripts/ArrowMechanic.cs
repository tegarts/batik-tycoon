using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ArrowMechanic : MonoBehaviour
{
    public Sprite[] arrowSprites;
    public Image[] sequenceImages;
    public KeyCode[] arrowKeys;

    [SerializeField] private Sprite[] currentSequence;
    public int currentButtonIndex;

    public int sequenceTotal;

    public bool isStartArrow = false;

    public GameObject arrowGameObject;

    private PlayerMovement playerMovement;

    AudioManager audioManager;
    [SerializeField] bool[] toolsDone;
    [SerializeField] bool[] isInsideTools;
    [SerializeField] GameObject popUpPressF;
    Animator anim;
    RewardManager rewardManager;
    public Dialogue dialogue;
    public GameObject[] glowTools;
    private bool isOnProgres;

    private void Awake()
    {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        arrowGameObject.SetActive(false);
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        popUpPressF.SetActive(false);
        anim = GetComponent<Animator>();
        rewardManager = FindAnyObjectByType<RewardManager>();

        for (int i = 0; i < glowTools.Length; i++)
        {
            glowTools[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (isInsideTools[0] == true && toolsDone[0] == false && !isStartArrow)
        {
            popUpPressF.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (glowTools[0].activeSelf && dialogue.isStartTutor)
                {
                    dialogue.gameObject.SetActive(true);
                }
                anim.SetBool("IsWalking", false);
                popUpPressF.SetActive(false);
                GenerateRandomSequence();
                isStartArrow = true;
            }
        }
        else if (isInsideTools[1] == true && toolsDone[0] == true && toolsDone[1] == false && !isStartArrow)
        {
            popUpPressF.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                anim.SetBool("IsWalking", false);
                popUpPressF.SetActive(false);
                GenerateRandomSequence();
                isStartArrow = true;
            }
        }
        else if (isInsideTools[2] == true && toolsDone[1] == true && toolsDone[2] == false && !isStartArrow)
        {
            popUpPressF.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                anim.SetBool("IsWalking", false);
                popUpPressF.SetActive(false);
                GenerateRandomSequence();
                isStartArrow = true;
            }
        }
        else if (isInsideTools[3] == true && toolsDone[2] == true && toolsDone[3] == false && !isStartArrow)
        {
            popUpPressF.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                anim.SetBool("IsWalking", false);
                popUpPressF.SetActive(false);
                GenerateRandomSequence();
                isStartArrow = true;
            }
        }
        else if (isInsideTools[4] == true && toolsDone[3] == true && toolsDone[4] == false && !isStartArrow)
        {
            popUpPressF.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                anim.SetBool("IsWalking", false);
                popUpPressF.SetActive(false);
                GenerateRandomSequence();
                isStartArrow = true;
            }
        }

        if (isStartArrow)
        {
            CheckButtonInput();
        }
    }

    public void GenerateRandomSequence()
    {
        playerMovement.canMove = false;
        arrowGameObject.SetActive(true);
        currentSequence = new Sprite[sequenceTotal];
        ShuffleArray(arrowSprites, currentSequence);

        UpdateSequenceImages();

    }

    void UpdateSequenceImages()
    {
        for (int i = 0; i < sequenceImages.Length; i++)
        {
            sequenceImages[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < sequenceTotal; i++)
        {
            sequenceImages[i].gameObject.SetActive(true);

            if (i < currentSequence.Length)
            {
                sequenceImages[i].sprite = currentSequence[i];
                sequenceImages[i].enabled = true;
            }
            else
            {
                sequenceImages[i].enabled = false;
            }
        }
    }

    public void CheckButtonInput()
    {
        if (Input.GetKeyDown(currentSequence[currentButtonIndex].name))
        {
            sequenceImages[currentButtonIndex].color = Color.green;
            audioManager.PlaySFX(audioManager.correctArrow);
            currentButtonIndex++;

            if (currentButtonIndex >= currentSequence.Length)
            {
                Debug.Log("Sequence Correct!");
                audioManager.PlaySFX(audioManager.successArrow);
                for (int i = 0; i < sequenceImages.Length; i++)
                {
                    sequenceImages[i].color = Color.white;
                }
                currentButtonIndex = 0;
                isStartArrow = false;
                arrowGameObject.SetActive(false);
                playerMovement.canMove = true;

                for (int i = 0; i < isInsideTools.Length; i++)
                {
                    if (isInsideTools[i] == true)
                    {
                        toolsDone[i] = true;
                    }
                }

                if (toolsDone[0] == true && toolsDone[1] == true && toolsDone[2] == true && toolsDone[3] == true && toolsDone[4] == true)
                {
                    rewardManager.GiveRewardManual();

                    for (int i = 0; i < toolsDone.Length; i++)
                    {
                        toolsDone[i] = false;
                    }
                }
                // if (isInsideTools[0] == true)
                // {
                //     toolsDone[0] = true;
                // }

                if (!glowTools[0].activeSelf && !glowTools[1].activeSelf && !glowTools[2].activeSelf && !glowTools[3].activeSelf && !glowTools[4].activeSelf && !isOnProgres)
                {
                    isOnProgres = true;
                    glowTools[1].SetActive(true);
                    return;
                }

                if (glowTools[0].activeSelf)
                {
                    glowTools[0].SetActive(false);
                    dialogue.gameObject.SetActive(true);
                    playerMovement.canMove = false;
                }
                else if (glowTools[1].activeSelf)
                {
                    glowTools[2].SetActive(true);
                    glowTools[1].SetActive(false);
                }
                else if (glowTools[2].activeSelf)
                {
                    glowTools[3].SetActive(true);
                    glowTools[2].SetActive(false);
                }
                else if (glowTools[3].activeSelf)
                {
                    glowTools[4].SetActive(true);
                    glowTools[3].SetActive(false);
                }
                else if (glowTools[4].activeSelf && dialogue.isStartTutor)
                {
                    glowTools[4].SetActive(false);
                    playerMovement.canMove = false;
                    dialogue.gameObject.SetActive(true);
                    isOnProgres = false;
                }
                else if (glowTools[4].activeSelf && !dialogue.isStartTutor)
                {
                    glowTools[4].SetActive(false);
                    isOnProgres = false;
                }


            }
        }
        else
        {
            foreach (KeyCode key in arrowKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    sequenceImages[currentButtonIndex].color = Color.red;
                    Debug.Log("Wrong button pressed. Restarting sequence.");

                    if (glowTools[0].activeSelf && dialogue.isStartTutor)
                    {
                        dialogue.index--;
                    }

                    audioManager.PlaySFX(audioManager.wrongArrow);
                    for (int i = 0; i < sequenceImages.Length; i++)
                    {
                        sequenceImages[i].color = Color.white;
                    }
                    arrowGameObject.SetActive(false);
                    currentButtonIndex = 0;
                    isStartArrow = false;
                    playerMovement.canMove = true;
                    break;
                }
            }
        }
    }

    void ShuffleArray(Sprite[] sourceArray, Sprite[] targetArray)
    {
        for (int i = 0; i < targetArray.Length; i++)
        {
            int randomIndex = Random.Range(0, sourceArray.Length);
            targetArray[i] = sourceArray[randomIndex];
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "tools1")
        {
            isInsideTools[0] = true;
        }
        else if (other.gameObject.tag == "tools2")
        {
            isInsideTools[1] = true;
        }
        else if (other.gameObject.tag == "tools3")
        {
            isInsideTools[2] = true;
        }
        else if (other.gameObject.tag == "tools4")
        {
            isInsideTools[3] = true;
        }
        else if (other.gameObject.tag == "tools5")
        {
            isInsideTools[4] = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "tools1")
        {
            isInsideTools[0] = false;
            popUpPressF.SetActive(false);
        }
        else if (other.gameObject.tag == "tools2")
        {
            isInsideTools[1] = false;
            popUpPressF.SetActive(false);
        }
        else if (other.gameObject.tag == "tools3")
        {
            isInsideTools[2] = false;
            popUpPressF.SetActive(false);
        }
        else if (other.gameObject.tag == "tools4")
        {
            isInsideTools[3] = false;
            popUpPressF.SetActive(false);
        }
        else if (other.gameObject.tag == "tools5")
        {
            isInsideTools[4] = false;
            popUpPressF.SetActive(false);
        }
    }
}
