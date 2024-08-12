using System;
using System.Collections;
using UnityEngine;

public class NPCBehav : MonoBehaviour
{
    public Transform[] waypoints; // Array Waypoints yang ditentukan
    public float speed = 5f; // Kecepatan pergerakan NPC
    public float waitTime = 10f; // Waktu berhenti di waypoint
    private Vector3 spawnPoint;
    private bool isWaiting = false; // Menandai apakah NPC sedang menunggu
    private Animator anim;

    public event Action<NPCBehav> OnNPCReturned; // Event untuk melaporkan kembali ke spawner

    void Start()
    {
        anim = GetComponent<Animator>(); // Mendapatkan komponen Animator
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

            Vector2 transformPosition = new Vector2(transform.position.x, transform.position.z);
            Vector2 targetPosition = new Vector2(targetWaypoint.position.x, targetWaypoint.position.z);


            while (Vector3.Distance(transformPosition, targetPosition) > 0.1f)
            {
                Vector3 destination = targetWaypoint.transform.position;

                destination.y = transform.position.y;
                Vector3 direction = (destination - transform.position);
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

                if (direction != Vector3.zero)
                {
                    Quaternion rotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
                }
                anim.SetBool("IsWalking", true);
                yield return null;
            }

            anim.SetBool("IsWalking", false);
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
                anim.SetBool("IsWalking", true);
                yield return null;
            }

            anim.SetBool("IsWalking", false);

            // Trigger event bahwa NPC telah kembali
            OnNPCReturned?.Invoke(this);

            // Hapus NPC setelah kembali ke titik spawn
            Destroy(gameObject);
        }
    }
}
