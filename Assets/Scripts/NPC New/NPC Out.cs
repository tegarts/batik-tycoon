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

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval && npcCount > 0)
        {
            SpawnNPC();
            spawnTimer = 0f;
            npcCount--;
        }
    }

    void SpawnNPC()
    {
        GameObject newNPC = Instantiate(npcPrefab, transform.position, Quaternion.identity);

        NPCWarga patrolScript = newNPC.GetComponent<NPCWarga>();
        if (patrolScript != null)
        {
            patrolScript.waypoints = waypoints;
        }
    }
}
