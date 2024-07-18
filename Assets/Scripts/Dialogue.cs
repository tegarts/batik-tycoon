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

    private int index;

    private void Awake() {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        uiManager = FindAnyObjectByType<UIManager>();
    }
    private void Start() 
    {
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

    void StartDialogue()
    {
        index = 0;
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
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            playerMovement.canMove = true;
            gameObject.SetActive(false);
        }
    }
}
