using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    PlayerMovement playerMovement;
    public string[] lines;
    public float textSpeed;
    UIManager uiManager;
    public bool isStartTutor;
    public GameObject indicatorUpgrade;
    [SerializeField] GameObject indicatorMoney;

    public int index;
    ArrowMechanic arrowMechanic;

    private void Awake() {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        uiManager = FindAnyObjectByType<UIManager>();
        arrowMechanic = FindAnyObjectByType<ArrowMechanic>();
    }
    private void Start() 
    {
        indicatorUpgrade.SetActive(false);
        indicatorMoney.SetActive(false);
        if(!uiManager.isAlreadyTutor)
        {
            playerMovement.canMove = false;
            isStartTutor = true;
        }
        else
        {
            playerMovement.canMove = true;
        }
        textComponent.text = string.Empty;
        StartDialogue();
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if(textComponent.text == lines[index])
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

    private void OnEnable() 
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void StartDialogue()
    {
        //index = 0;
        StartCoroutine(TypeLine());
    }


    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < 2)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else if(index == 2)
        {
            index++;
            playerMovement.canMove = true;
            gameObject.SetActive(false);
            arrowMechanic.glowTools[0].SetActive(true);
        }
        else if(index == 3)
        {
            index++;
            gameObject.SetActive(false);
            playerMovement.canMove = false;
        }
        else if(index == 4)
        {
            index++;
            playerMovement.canMove = true;
            gameObject.SetActive(false);
            arrowMechanic.glowTools[1].SetActive(true);
        }
        else if(index == 5)
        {
            index++;
            textComponent.text = string.Empty;
            indicatorMoney.SetActive(true);
            StartCoroutine(TypeLine());

        }
        else if(index == 6)
        {
            index++;
            textComponent.text = string.Empty;
            indicatorMoney.SetActive(false);
            indicatorUpgrade.SetActive(true);
            StartCoroutine(TypeLine());
        }
        else if(index == 7)
        {
            index++;
            gameObject.SetActive(false);
        }
        else if(index == 8)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
            
        }
        else if(index == 9)
        {
            gameObject.SetActive(false);
            playerMovement.canMove = true;
            isStartTutor = false;
        }
    }
}
