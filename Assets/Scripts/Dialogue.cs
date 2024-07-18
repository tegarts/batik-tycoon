using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    PlayerMovement playerMovement;
    public string[] lines;
    public bool isLine1;
    public float textSpeed;
    UIManager uiManager;

    [SerializeField] private int index;
    public GameObject glowTools1;

    private void Awake() {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        uiManager = FindAnyObjectByType<UIManager>();
    }
    private void Start() 
    {
        glowTools1.SetActive(false);
        if(!uiManager.isAlreadyTutor)
        {
            playerMovement.canMove = false;
        }
        else
        {
            playerMovement.canMove = true;
        }
        textComponent.text = string.Empty;
        StartDialogue();
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0))
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
        if(index < 2 && !isLine1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
            Debug.Log("test 1");
        }
        else if(index == 2 && !isLine1)
        {
            index++;
            playerMovement.canMove = true;
            gameObject.SetActive(false);
            glowTools1.SetActive(true);
            Debug.Log("test 2");
        }
        else if(index == 3 && isLine1)
        {
            index++;
            gameObject.SetActive(false);
        }
        else if(index == 4 && isLine1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
    }
}
