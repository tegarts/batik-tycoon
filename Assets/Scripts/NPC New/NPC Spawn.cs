using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;

public class NPCSpawn : MonoBehaviour
{
    public GameObject[] npcPrefabs; // Prefab NPC yang akan di-spawn
    [SerializeField] GameObject npcPrefab;
    public Transform[] waypoints;
    public Transform[] waypoints1;
    public Transform[] waypoints2;
    public Transform[] waypoints3;
    public Transform[] waypoints4;
    public Transform[] waypoints5;
    private List<Transform[]> waypointOptions = new List<Transform[]>();
    private Transform[] lastWaypoints;
    public float waitTime = 10f; // Waktu berhenti di waypoint
    public int npcCount = 5;
    [SerializeField] int npcDayOne;
    [SerializeField] int totalNPC;
    public int initializeNPC;
    private bool isInitialized;
    public float spawnInterval = 3f;

    [Header("UI Related")]
    [SerializeField] TMP_Text npcCountText;

    [Header("References")]
    DayManager dayManager;
    BookMenu bookMenu;
    private bool isSpawning;
    [SerializeField] Tutorial tutorial;

    private List<NPCBehav> activeNPCs = new List<NPCBehav>();
    AudioSetter audioSetter;

    private void Awake()
    {
        audioSetter = GameObject.FindWithTag("Audio").GetComponent<AudioSetter>();
    }

    void Start()
    {
        bookMenu = FindAnyObjectByType<BookMenu>();
        tutorial = FindAnyObjectByType<Tutorial>();
        // StartCoroutine(SpawnNPCsInBatches());
        dayManager = FindAnyObjectByType<DayManager>();
        waypointOptions.Add(waypoints1);
        waypointOptions.Add(waypoints2);
        waypointOptions.Add(waypoints3);
        waypointOptions.Add(waypoints4);
        waypointOptions.Add(waypoints5);
    }

    private void Update()
    {
        if (tutorial.isStartTutor && tutorial.isNpcIn)
        {
            Debug.Log("test");
            SpawnNPCForTutor();
            tutorial.isNpcIn = false;
        }

        if (dayManager.dayIsStarted)
        {
            if (!isSpawning)
            {
                totalNPC = npcDayOne + ((dayManager.day - 1) / 2) * 5;
                isSpawning = true;
                if (!isInitialized)
                {
                    initializeNPC = totalNPC;
                    isInitialized = true;
                }
                StartCoroutine(SpawnNPCsInBatches());
            }

        }
        else if (!dayManager.dayIsStarted)
        {
            StopCoroutine(SpawnNPCsInBatches());
        }



    }

    IEnumerator SpawnNPCsInBatches()
    {
        // while (totalNPC > 0)
        // {
        //     int currentBatchCount = Mathf.Min(npcCount, totalNPC);
        //     for (int i = 0; i < currentBatchCount; i++)
        //     {
        //         totalNPC--;
        //         SpawnNPC();
        //         yield return new WaitForSeconds(spawnInterval);
        //     }
        //     // totalNPC -= currentBatchCount;
        //     yield return StartCoroutine(WaitForAllNPCsToReturn());
        // }
        // isSpawning = false;
        // dayManager.dayIsStarted = false;
        // isInitialized = false;
        // bookMenu.OpenBook();

        while(totalNPC > 0)
        {
            int currentBatchCount = Mathf.Min(npcCount, totalNPC);
            List<Transform[]> availableWaypoints = new List<Transform[]>(waypointOptions);

            for(int i = 0; i < currentBatchCount; i++)
            {
                if(availableWaypoints.Count == 0)
                {
                    availableWaypoints = new List<Transform[]>(waypointOptions);
                }

                totalNPC--;
                SpawnNPC(availableWaypoints);
                yield return new WaitForSeconds(spawnInterval);
            }
            yield return StartCoroutine(WaitForAllNPCsToReturn());
        }

        isSpawning = false;
        dayManager.dayIsStarted = false;
        isInitialized = false;
        bookMenu.OpenBook();
    }

    void SpawnNPC(List<Transform[]> availableWaypoints)
    {
        audioSetter.PlaySFX(audioSetter.bellEntrance);
        int randomIndex = Random.Range(0, npcPrefabs.Length);
        npcPrefab = npcPrefabs[randomIndex];
        GameObject npcObject = Instantiate(npcPrefab, transform.position, Quaternion.Euler(0, 180, 0));
        NPCBehav npcBehav = npcObject.GetComponent<NPCBehav>();

        // List<Transform[]> availableWaypoints = new List<Transform[]>(waypointOptions);
        // if (lastWaypoints != null)
        // {
        //     availableWaypoints.Remove(lastWaypoints);
        // }

        int waypointSetIndex = Random.Range(0, availableWaypoints.Count);
        Transform[] selectedWaypoints = availableWaypoints[waypointSetIndex];

        availableWaypoints.RemoveAt(waypointSetIndex);

        // lastWaypoints = selectedWaypoints;
        npcBehav.waypoints = selectedWaypoints;
        npcBehav.waitTime = waitTime;

        activeNPCs.Add(npcBehav);
        npcBehav.OnNPCReturned += HandleNPCReturned;

        npcCountText.text = totalNPC.ToString();

        // if (!tutorial.isStartTutor)
        // {
        //     totalNPC--;
        // }

    }

    void SpawnNPCForTutor()
    {
        int randomIndex = Random.Range(0, npcPrefabs.Length);
        npcPrefab = npcPrefabs[randomIndex];
        GameObject npcObject = Instantiate(npcPrefab, transform.position, Quaternion.Euler(0, 180, 0));
        NPCBehav npcBehav = npcObject.GetComponent<NPCBehav>();

        npcBehav.waypoints = waypoints1;
        npcBehav.waitTime = waitTime;

        activeNPCs.Add(npcBehav);
        npcBehav.OnNPCReturned += HandleNPCReturned;

        npcCountText.text = totalNPC.ToString();
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
