using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWarga : MonoBehaviour
{
    public Transform[] waypoints;   // Array waypoint (bisa lebih dari 5)
    public float speed = 2f;        // Kecepatan NPC
    private int currentWaypointIndex = 0;  // Indeks waypoint saat ini
    private Animator animator;      // Animator untuk animasi

    void Start()
    {
        animator = GetComponent<Animator>();  // Dapatkan komponen Animator
    }

    void Update()
    {
        Patrol();
        HandleAnimation(); // Handle animasi sesuai dengan status NPC
    }

    void Patrol()
    {
        // Jika waypoint kurang dari 2, hentikan proses
        if (waypoints.Length < 2) return;

        // Dapatkan posisi waypoint saat ini
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Pindahkan NPC menuju waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // Rotasi NPC agar menghadap arah gerakan
        Vector3 direction = targetWaypoint.position - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }

        // Jika NPC sudah mencapai waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // Pindah ke waypoint berikutnya
            currentWaypointIndex++;

            // Jika sudah mencapai waypoint terakhir, kembali ke waypoint pertama
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;  // Kembali ke waypoint pertama
            }
        }
    }


    // Handle animasi berjalan
    void HandleAnimation()
    {
        // Aktifkan animasi berjalan jika NPC bergerak
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) > 0.1f)
        {
            animator.SetBool("IsWalking", true); // Set animasi berjalan
        }
        else
        {
            animator.SetBool("IsWalking", false); // Set animasi idle
        }
    }

    // Debugging untuk melihat jalur waypoint di Scene
    private void OnDrawGizmos()
    {
        if (waypoints.Length > 0)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < waypoints.Length; i++)
            {
                Gizmos.DrawSphere(waypoints[i].position, 0.2f);
                if (i + 1 < waypoints.Length)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                }
            }
        }
    }
}
