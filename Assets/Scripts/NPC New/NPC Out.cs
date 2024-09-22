using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCOut : MonoBehaviour
{
    public GameObject npcPrefab;     // Prefab NPC yang akan di-spawn
    public Transform[] waypoints;    // Waypoints yang akan diberikan ke NPC
    public int npcCount = 3;         // Jumlah NPC yang akan di-spawn
    public float spawnInterval = 2f; // Jeda waktu antar spawn NPC

    private float spawnTimer = 0f;

    void Update()
    {
        // Timer untuk spawn NPC
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval && npcCount > 0)
        {
            SpawnNPC();
            spawnTimer = 0f; // Reset timer
            npcCount--;      // Kurangi jumlah NPC yang harus di-spawn
        }
    }

    void SpawnNPC()
    {
        // Spawn NPC pada posisi spawner
        GameObject newNPC = Instantiate(npcPrefab, transform.position, Quaternion.identity);

        // Berikan NPC referensi waypoint
        NPCWarga patrolScript = newNPC.GetComponent<NPCWarga>();
        if (patrolScript != null)
        {
            patrolScript.waypoints = waypoints;
        }
    }
}
