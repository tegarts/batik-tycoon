using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public float spawnInterval = 5f;
    TimeManager timeManager;
    AdvertisingManager advertisingManager;

    private Coroutine spawnCoroutine;
    private int lastHour;
    private int lastMinute;
    private int lastSpawnTimeInMinutes;

    private void Start()
    {
        timeManager = FindAnyObjectByType<TimeManager>();
        advertisingManager = FindAnyObjectByType<AdvertisingManager>();
        lastHour = 8;
        lastMinute = 0;
        lastSpawnTimeInMinutes = 8 * 60;
    }

    private void Update()
    {
        if (timeManager.isStartDay)
        {
            int currentTimeInMinutes = timeManager.hour * 60 + timeManager.minute;
            int elapsedTimeInMinutes = currentTimeInMinutes - lastSpawnTimeInMinutes;

            if (advertisingManager.isKoran)
            {
                if (elapsedTimeInMinutes >= 90)
                {
                    Instantiate(npcPrefab, transform.position, transform.rotation);
                    lastSpawnTimeInMinutes = currentTimeInMinutes;
                }
            }
            else if (elapsedTimeInMinutes >= 90)
            {
                if (lastHour != timeManager.hour)
                {
                    Instantiate(npcPrefab, transform.position, transform.rotation);
                    lastSpawnTimeInMinutes = currentTimeInMinutes;
                }
            }
            else if (advertisingManager.isBaliho)
            {
                if (elapsedTimeInMinutes >= 60)
                {
                    Instantiate(npcPrefab, transform.position, transform.rotation);
                    lastSpawnTimeInMinutes = currentTimeInMinutes;
                }
            }
            else if (advertisingManager.isPesbuk)
            {
                if (elapsedTimeInMinutes >= 40)
                {
                    Instantiate(npcPrefab, transform.position, transform.rotation);
                    lastSpawnTimeInMinutes = currentTimeInMinutes;
                }
            }
            else if (advertisingManager.isYutup)
            {
                if (elapsedTimeInMinutes >= 40)
                {
                    Instantiate(npcPrefab, transform.position, transform.rotation);
                    lastSpawnTimeInMinutes = currentTimeInMinutes;
                }
            }
            else
            {
                    Instantiate(npcPrefab, transform.position, transform.rotation);
                    lastHour = timeManager.hour;
                
            }
        }
        else
        {
            lastHour = 8;
            lastSpawnTimeInMinutes = 8 * 60;
        }

    }
}
