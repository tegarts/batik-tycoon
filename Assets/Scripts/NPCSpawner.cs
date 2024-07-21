using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public float spawnInterval = 5f;
    TimeManager timeManager;

    private Coroutine spawnCoroutine;
    private int lastHour;

    private void Start()
    {
        timeManager = FindAnyObjectByType<TimeManager>();
        lastHour = 8;
    }

    private void Update() 
    {
        if(timeManager.isStartDay)
        {
            if(lastHour != timeManager.hour)
        {
            Instantiate(npcPrefab, transform.position, transform.rotation);
            lastHour = timeManager.hour;
        }
        }
        
    }
}
