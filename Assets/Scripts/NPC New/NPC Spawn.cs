using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCSpawn : MonoBehaviour
{
    public GameObject npcPrefab; // Prefab NPC yang akan di-spawn

    public Transform[] waypoints;
    public float waitTime = 10f; // Waktu berhenti di waypoint
    public int npcCount = 5;
    [SerializeField] int totalNPC;
    public float spawnInterval = 3f;
    [Header("References")]
    DayManager dayManager;
    private bool isSpawning;

    private List<NPCBehav> activeNPCs = new List<NPCBehav>();

    void Start()
    {
        // StartCoroutine(SpawnNPCsInBatches());
        dayManager = FindAnyObjectByType<DayManager>();
    }

    private void Update()
    {
        if (dayManager.dayIsStarted)
        {
            if (!isSpawning)
            {
                if(totalNPC > 0)
                {
                    isSpawning = true;
                    StartCoroutine(SpawnNPCsInBatches());
                }

                totalNPC = 10 + ((dayManager.day - 1) / 2) * 5;
            }
            else if(isSpawning == true && totalNPC <= 0)
            {
                isSpawning = false;
                dayManager.dayIsStarted = false;
            }
        }
        else if(!dayManager.dayIsStarted)
        {
            StopCoroutine(SpawnNPCsInBatches());    
        }

    }

    IEnumerator SpawnNPCsInBatches()
    {
        while (true)
        {
            // Spawn 5 NPC
            for (int i = 0; i < npcCount; i++)
            {
                SpawnNPC();
                yield return new WaitForSeconds(spawnInterval);
            }

            yield return StartCoroutine(WaitForAllNPCsToReturn());
        }
    }

    void SpawnNPC()
    {
        GameObject npcObject = Instantiate(npcPrefab, transform.position, Quaternion.identity);
        NPCBehav npcBehav = npcObject.GetComponent<NPCBehav>();

        npcBehav.waypoints = waypoints;
        npcBehav.waitTime = waitTime;

        activeNPCs.Add(npcBehav);
        npcBehav.OnNPCReturned += HandleNPCReturned;
        totalNPC--;
    }

    void HandleNPCReturned(NPCBehav npc)
    {
        activeNPCs.Remove(npc);
    }

    IEnumerator WaitForAllNPCsToReturn()
    {
        while (activeNPCs.Count > 0)
        {
            yield return null;
        }
    }
}
