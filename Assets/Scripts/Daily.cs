using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Daily : MonoBehaviour
{
    public static Daily instance { get; private set; }
    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;
    [SerializeField] float progress = 0;
    float maxProgress;

    [Header("Reaction")]
    public int happyReaction;
    public int flatReaction;
    public int angryReaction;
    [Header("References")]
    NPCSpawn nPCSpawn;
    DayManager dayManager;

    private void Start() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        nPCSpawn = FindAnyObjectByType<NPCSpawn>();
        dayManager = FindAnyObjectByType<DayManager>();

        progressBar.value = progress;
        progressBar.maxValue = maxProgress;
    }

    public void IncreaseProgress(float value)
    {
        progress += value;
        progressBar.value = progress;
    }

    private void Update() 
    {
        if(dayManager.dayIsStarted)
        {
            progressBar.maxValue = nPCSpawn.initializeNPC * 25;
        }
        else
        {
            // TODO - BUat kondisi nampilin daily report dulu nilai progres hariannya
            progress = 0;
        }    
    }
    
}
