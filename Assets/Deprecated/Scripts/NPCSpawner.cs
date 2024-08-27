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

    private int lastSpawnTimeInMinutes;
    AudioManager audioManager;

    private void Start()
    {
        timeManager = FindAnyObjectByType<TimeManager>();
        advertisingManager = FindAnyObjectByType<AdvertisingManager>();
        audioManager = FindAnyObjectByType<AudioManager>();
        // lastHour = 8;
        // lastMinute = 0;
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
                    audioManager.PlaySFX(audioManager.peopleIn);
                    lastSpawnTimeInMinutes = currentTimeInMinutes;
                }
            }
            else if (advertisingManager.isPoster)
            {
                if (elapsedTimeInMinutes >= 60)
                {
                    Instantiate(npcPrefab, transform.position, transform.rotation);
                    audioManager.PlaySFX(audioManager.peopleIn);
                    lastSpawnTimeInMinutes = currentTimeInMinutes;
                }
            }
            else if (advertisingManager.isBaliho)
            {
                if (elapsedTimeInMinutes >= 60)
                {
                    Instantiate(npcPrefab, transform.position, transform.rotation);
                    audioManager.PlaySFX(audioManager.peopleIn);
                    lastSpawnTimeInMinutes = currentTimeInMinutes;
                }
            }
            else if (advertisingManager.isPesbuk)
            {
                if (elapsedTimeInMinutes >= 40)
                {
                    Instantiate(npcPrefab, transform.position, transform.rotation);
                    audioManager.PlaySFX(audioManager.peopleIn);
                    lastSpawnTimeInMinutes = currentTimeInMinutes;
                }
            }
            else if (advertisingManager.isYutup)
            {
                if (elapsedTimeInMinutes >= 40)
                {
                    Instantiate(npcPrefab, transform.position, transform.rotation);
                    audioManager.PlaySFX(audioManager.peopleIn);
                    lastSpawnTimeInMinutes = currentTimeInMinutes;
                }
            }
            else
            {
                if(elapsedTimeInMinutes >= 120)
                {   
                    Instantiate(npcPrefab, transform.position, transform.rotation);
                    audioManager.PlaySFX(audioManager.peopleIn);
                    lastSpawnTimeInMinutes = currentTimeInMinutes;
                }
                    
            }
        }
        else
        {
            // lastHour = 8;
            lastSpawnTimeInMinutes = 8 * 60;
        }

    }
}
