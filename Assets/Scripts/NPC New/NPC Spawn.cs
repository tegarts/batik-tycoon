using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCSpawn : MonoBehaviour
{
    public GameObject npcPrefab; // Prefab NPC yang akan di-spawn
    public Transform[] waypoints;
    public float waitTime = 10f; // Waktu berhenti di waypoint
    public int npcCount = 5;
    public float spawnInterval = 3f;

    private List<NPCBehav> activeNPCs = new List<NPCBehav>();

    void Start()
    {
        StartCoroutine(SpawnNPCsInBatches());
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
