using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWarga : MonoBehaviour
{
    public Transform[] waypoints; // Array Waypoints yang ditentukan
    public float speed = 5f; // Kecepatan pergerakan NPC
    public float waitTime = 10f; // Waktu berhenti di waypoint
    private Vector3 spawnPoint;
    private bool isWaiting = false; // Menandai apakah NPC sedang menunggu
    //private Animator anim;

    public event Action<NPCWarga> OnNPCReturned;

    void Start()
    {
        //anim = GetComponent<Animator>(); // Mendapatkan komponen Animator
        spawnPoint = transform.position; // Menyimpan posisi awal (titik spawn)
        StartCoroutine(MoveToWaypoints());
    }

    IEnumerator MoveToWaypoints()
    {
        while (true)
        {
            // Pilih waypoint acak
            Transform targetWaypoint = waypoints[UnityEngine.Random.Range(0, waypoints.Length)];
            isWaiting = false;

            while (Vector3.Distance(transform.position, targetWaypoint.position) > 0.1f)
            {
                // Vector3 direction = (targetWaypoint.position - transform.position).normalized;
                Vector3 direction = targetWaypoint.transform.position;

                Quaternion rotation = Quaternion.LookRotation(direction);
                direction.y = targetWaypoint.transform.position.y;

                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

                transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);
                //anim.SetBool("IsWalking", true);
                yield return null;
            }
            //anim.SetBool("IsWalking", false);
            isWaiting = true;
            yield return new WaitForSeconds(waitTime);

            // Setelah menunggu, kembali ke titik spawn
            while (Vector3.Distance(transform.position, spawnPoint) > 0.1f)
            {
                Vector3 direction = (spawnPoint - transform.position).normalized;

                Quaternion rotation = Quaternion.LookRotation(direction);
                direction.y = spawnPoint.y;

                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

                transform.position = Vector3.MoveTowards(transform.position, spawnPoint, speed * Time.deltaTime);
                //anim.SetBool("IsWalking", true);
                yield return null;
            }

            //anim.SetBool("IsWalking", false);

            // Trigger event bahwa NPC telah kembali
            OnNPCReturned?.Invoke(this);

            // Hapus NPC setelah kembali ke titik spawn
            Destroy(gameObject);
        }
    }
}
