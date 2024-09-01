using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    AudioSetter audioSetter;
    DayManager dayManager;
    Money money;
    Tutorial tutorial;
    WorkspaceManager workspaceManager;
    [SerializeField] Workspace[] workspaces;
    Daily daily;
    private void Awake()
    {
        audioSetter = GameObject.FindWithTag("Audio").GetComponent<AudioSetter>();
        money = FindObjectOfType<Money>();
        tutorial = FindObjectOfType<Tutorial>();
        workspaceManager = FindObjectOfType<WorkspaceManager>();
        daily = FindObjectOfType<Daily>();
    }

    private void Start() 
    {
        dayManager = FindAnyObjectByType<DayManager>();
    }
    public void GameOverShow()
    {
        audioSetter.PlaySFX(audioSetter.OpenPanel);
        Time.timeScale = 1;
        dayManager.day = 0;
        money.moneyValue = 0;
        tutorial.isAlreadyTutor = false;
        workspaceManager.motifUnlocked = 0;
        daily.totalStars = 0;
        for(int i = 0; i < workspaces.Length; i++)
        {
            workspaces[i].level_workspace = 0;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
