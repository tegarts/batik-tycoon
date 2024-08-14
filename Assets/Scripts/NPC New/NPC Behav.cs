using System;
using System.Collections;
using UnityEngine;

public class NPCBehav : MonoBehaviour
{
    public Transform[] waypoints; 
    public float speed;
    public float waitTime;
    private Vector3 spawnPoint;
    private bool isWaiting = false;
    private Animator anim;

    public event Action<NPCBehav> OnNPCReturned;

    void Start()
    {
        anim = GetComponent<Animator>();
        spawnPoint = transform.position;
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
                Vector3 destination = targetWaypoint.position;
                destination.y = transform.position.y; // Jaga ketinggian tetap sama

                Vector3 direction = (destination - transform.position).normalized;
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

            while (Vector3.Distance(transform.position, spawnPoint) > 0.1f)
            {
                Vector3 direction = (spawnPoint - transform.position).normalized;
                transform.position = Vector3.MoveTowards(transform.position, spawnPoint, speed * Time.deltaTime);

                if (direction != Vector3.zero)
                {
                    Quaternion rotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
                }

                anim.SetBool("IsWalking", true);
                yield return null;
            }

            anim.SetBool("IsWalking", false);

            OnNPCReturned?.Invoke(this);

            Destroy(gameObject);
        }
    }
}
