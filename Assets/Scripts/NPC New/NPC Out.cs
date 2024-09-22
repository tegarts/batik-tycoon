using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCOut : MonoBehaviour
{
    public GameObject npcPrefab;
    public Transform[] waypoints;
    public int npcCount = 3;
    public float spawnInterval = 2f;

    private float spawnTimer = 0f;
    [SerializeField] List<GameObject> activeNPC;
    DayManager dayManager;

    private void Start()
    {
        dayManager = FindAnyObjectByType<DayManager>();
    }

    void Update()
    {
        if (dayManager.dayIsStarted)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval && npcCount > 0)
            {
                SpawnNPC();
                spawnTimer = 0f;
                npcCount--;
            }
        }
        else
        {
            for(int i = 0; i < activeNPC.Count; i++)
            {
                Destroy(activeNPC[i]);
            }
        }

    }

    void SpawnNPC()
    {
        GameObject newNPC = Instantiate(npcPrefab, transform.position, Quaternion.identity);
        activeNPC.Add(newNPC);
        NPCWarga patrolScript = newNPC.GetComponent<NPCWarga>();
        if (patrolScript != null)
        {
            patrolScript.waypoints = waypoints;
        }
    }
}
