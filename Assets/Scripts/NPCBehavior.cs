using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
     public GameObject[] waypoints = new GameObject[9];
    public float speed = 2;
    [SerializeField] int index = 0;
    public bool isLoop = true;
    public float waitTimeAtWaypoint = 2f;
    private bool isWaiting = false;
    Animator anim;
    private int[] possibleStopWaypoints = {2, 5, 6};
    public int[] stopWaypoints;
    BuyerManager buyerManager;

    private void SetRandomStopWaypoints()
    {
        float randomValue = Random.value * 100f;

        int numStopWaypoints;
        if (randomValue < 75f)
        {
            numStopWaypoints = 1;
        }
        else if (randomValue < 90f)
        {
            numStopWaypoints = 2;
        }
        else
        {
            numStopWaypoints = 3;
        }

        stopWaypoints = new int[numStopWaypoints];
        List<int> possibleStops = new List<int>(possibleStopWaypoints);
        for (int i = 0; i < numStopWaypoints; i++)
        {
            int randomIndex = Random.Range(0, possibleStops.Count);
            stopWaypoints[i] = possibleStops[randomIndex];
            possibleStops.RemoveAt(randomIndex);
        }
    }

    private void Start()
    {
        buyerManager = FindAnyObjectByType<BuyerManager>();

        anim = GetComponent<Animator>();
        waypoints[0] = GameObject.Find("WP1");
        waypoints[1] = GameObject.Find("WP2");
        waypoints[2] = GameObject.Find("WP3");
        waypoints[3] = GameObject.Find("WP4");
        waypoints[4] = GameObject.Find("WP5");
        waypoints[5] = GameObject.Find("WP6");
        waypoints[6] = GameObject.Find("WP7");
        waypoints[7] = GameObject.Find("WP8");
        waypoints[8] = GameObject.Find("WP9");
        SetRandomStopWaypoints();
    }

    private void Update()
    {
       
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
        if (distance <= 0.05f) 
        {
            if (ShouldStopAtWaypoint(index))
            {

                StartCoroutine(WaitAtWaypoint());
                if(index == 2)
                {
                    buyerManager.NPCBuying1();
                }
                else if(index == 5)
                {
                    buyerManager.NPCBuying2();
                }
                else if(index == 6)
                {
                    buyerManager.NPCBuying3();
                }
                else
                {
                    Debug.LogError("Index tidak sesuai");
                }
            }
            else
            {
                MoveToNextWaypoint();
            }
        }
    }

    private bool ShouldStopAtWaypoint(int waypointIndex)
    {
 
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

        if (index < waypoints.Length - 1)
        {
            index++;
        }
        else
        {
            anim.SetBool("IsWalking", false);
            Destroy(gameObject);
            if (isLoop)
            {
                index = 0;
            }
        }
    }

    private IEnumerator WaitAtWaypoint()
    {
        isWaiting = true; 

        yield return new WaitForSeconds(waitTimeAtWaypoint);

        MoveToNextWaypoint();

        isWaiting = false; 
    }
}
