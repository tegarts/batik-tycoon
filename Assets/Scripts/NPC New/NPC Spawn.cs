using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class NPCSpawn : MonoBehaviour
{
    public GameObject[] npcPrefabs; // Prefab NPC yang akan di-spawn
    [SerializeField] GameObject npcPrefab;
    public Transform[] waypoints;
    public Transform[] waypoints1;
    public Transform[] waypoints2;
    public Transform[] waypoints3;
    public Transform[] waypoints4;
    private List<Transform[]> waypointOptions = new List<Transform[]>();
    private Transform[] lastWaypoints;
    public float waitTime = 10f; // Waktu berhenti di waypoint
    public int npcCount = 5;
    [SerializeField] int totalNPC;
    public float spawnInterval = 3f;

    [Header("UI Related")]
    [SerializeField] TMP_Text npcCountText;

    [Header("References")]
    DayManager dayManager;
    private bool isSpawning;

    private List<NPCBehav> activeNPCs = new List<NPCBehav>();

    void Start()
    {
        // StartCoroutine(SpawnNPCsInBatches());
        dayManager = FindAnyObjectByType<DayManager>();
        waypointOptions.Add(waypoints1);
        waypointOptions.Add(waypoints2);
        waypointOptions.Add(waypoints3);
        waypointOptions.Add(waypoints4);
    }

    private void Update()
    {
        if (dayManager.dayIsStarted)
        {
            if (!isSpawning)
            {
                totalNPC = 10 + ((dayManager.day - 1) / 2) * 5;
                isSpawning = true;
                StartCoroutine(SpawnNPCsInBatches());
            }
            else if (isSpawning == true && totalNPC <= 0)
            {
                isSpawning = false;
                dayManager.dayIsStarted = false;
            }

            npcCountText.text = totalNPC + " Pembeli";
        }
        else if (!dayManager.dayIsStarted)
        {
            StopCoroutine(SpawnNPCsInBatches());
        }

    }

    IEnumerator SpawnNPCsInBatches()
    {
        while (totalNPC > 0)
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
        int randomIndex = Random.Range(0, npcPrefabs.Length);
        npcPrefab = npcPrefabs[randomIndex];
        GameObject npcObject = Instantiate(npcPrefab, transform.position, Quaternion.identity);
        NPCBehav npcBehav = npcObject.GetComponent<NPCBehav>();

         List<Transform[]> availableWaypoints = new List<Transform[]>(waypointOptions);
        if (lastWaypoints != null)
        {
            availableWaypoints.Remove(lastWaypoints);
        }

         int waypointSetIndex = Random.Range(0, availableWaypoints.Count);
        Transform[] selectedWaypoints = availableWaypoints[waypointSetIndex];

        lastWaypoints = selectedWaypoints; 
        npcBehav.waypoints = selectedWaypoints;
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
