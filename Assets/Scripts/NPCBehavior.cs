using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
     public GameObject[] waypoints;
    public float speed = 2;
    [SerializeField] int index = 0;
    public bool isLoop = true;

    // Menambahkan variabel untuk waypoint tertentu di mana NPC akan berhenti
    public int[] stopWaypoints;
    public float waitTimeAtWaypoint = 2f; // Waktu tunggu di waypoint tertentu
    private bool isWaiting = false; // Untuk memeriksa apakah NPC sedang menunggu
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Jika NPC sedang menunggu, tidak melakukan gerakan
        if (isWaiting)
        {
            anim.SetBool("IsWalking", false);
            return;
        }
        anim.SetBool("IsWalking", true);
        Vector3 destination = waypoints[index].transform.position;
        Vector3 currentPosition = transform.position;

        destination.y = currentPosition.y;

        Vector3 direction = destination - currentPosition;
        Vector3 newPos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        transform.position = newPos;

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
        }
        

        float distance = Vector3.Distance(transform.position, destination);
        if (distance <= 0.05f) // Memastikan jarak cukup dekat untuk beralih waypoint
        {
            if (ShouldStopAtWaypoint(index))
            {
                StartCoroutine(WaitAtWaypoint()); // Mulai coroutine untuk menunggu di waypoint tertentu
            }
            else
            {
                MoveToNextWaypoint();
            }
        }
    }

    private bool ShouldStopAtWaypoint(int waypointIndex)
    {
        // Memeriksa apakah indeks waypoint saat ini ada di dalam array stopWaypoints
        foreach (int stopIndex in stopWaypoints)
        {
            if (waypointIndex == stopIndex)
            {
                return true;
            }
        }
        return false;
    }

    private void MoveToNextWaypoint()
    {
        // Pindah ke waypoint berikutnya
        if (index < waypoints.Length - 1)
        {
            index++;
        }
        else
        {
            anim.SetBool("IsWalking", false);
            if (isLoop)
            {
                index = 0;
            }
        }
    }

    private IEnumerator WaitAtWaypoint()
    {
        isWaiting = true; // Tandai NPC sedang menunggu

        // Tunggu beberapa detik
        yield return new WaitForSeconds(waitTimeAtWaypoint);

        MoveToNextWaypoint(); // Pindah ke waypoint berikutnya setelah menunggu

        isWaiting = false; // Tandai NPC tidak sedang menunggu lagi
    }
}
