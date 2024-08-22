using System;
using System.Collections;
using System.ComponentModel;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class NPCBehav : MonoBehaviour
{
    public Transform[] waypoints;
    [SerializeField] int index = 0;
    public float speed;
    public float waitTime;
    private Vector3 spawnPoint;
    [SerializeField] private bool isWaiting = false;
    private Animator anim;
    private bool isReversing;
    [SerializeField] FollowMouse followMouse;
    public event Action<NPCBehav> OnNPCReturned;
    [SerializeField] bool isEventRightTriggered = false;
    [SerializeField] bool isEventWrongTriggered = false;
    [SerializeField] bool isEventWorkspaceTriggered = false;
    [SerializeField] bool isEventDrawingTriggered = false;
    private float elapsedTime = 0f;
    private bool isWaitTimeActive = false;
    public Workspace currentWorkspace;
    [SerializeField] Drawing drawing;
    Tutorial tutorial;
    [Header("UI Related")]
    [SerializeField] private GameObject buttonMotif;
    [SerializeField] private Camera cam;
    [SerializeField] private Canvas canvasWorldSpace;

    void Start()
    {
        tutorial = FindAnyObjectByType<Tutorial>();
        anim = GetComponent<Animator>();
        spawnPoint = transform.position;
        buttonMotif = GetComponentInChildren<FollowMouse>().gameObject;
        followMouse = buttonMotif.GetComponent<FollowMouse>();
        cam = FindAnyObjectByType<Camera>();
        canvasWorldSpace = GameObject.Find("Canvas World Space").GetComponent<Canvas>();
        buttonMotif.transform.SetParent(canvasWorldSpace.transform);
        if (buttonMotif.TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
        {
            faceCamera.Camera = cam;
        }
        

        if (followMouse != null)
        {
            followMouse.OnMouseDroppedRight += HandleDroppedRight;
            followMouse.OnMouseDroppedWrong += HandleDroppedWrong;
        }
        buttonMotif.SetActive(false);

    }

    private void HandleDroppedRight()
    {
        isEventRightTriggered = true;
    }
    private void HandleDroppedWrong()
    {
        isEventWrongTriggered = true;
    }
    private void HandleWorkspaceCompleted()
    {
        isEventWorkspaceTriggered = true;
    }
    private void HandleDrawingCompleted()
    {
        isEventDrawingTriggered = true;
    }

    private void Update()
    {

        if (isWaiting)
        {
            anim.SetBool("IsWalking", false);
            if (isWaitTimeActive)
            {
                elapsedTime += Time.deltaTime;
                if (isEventRightTriggered)
                {
                    drawing = FindAnyObjectByType<Drawing>();

                    if (currentWorkspace != null)
                    {
                        currentWorkspace.OnWorkspaceCompleted += HandleWorkspaceCompleted;
                    }
                    
                    if (drawing != null)
                    {
                        drawing.OnDrawingCompleted += HandleDrawingCompleted;
                    }
                    Debug.Log("Tunggu workspace selesai");
                    if (isEventWorkspaceTriggered || isEventDrawingTriggered)
                    {
                        StopWaiting();
                    }

                    // isEventTriggered = false;
                    // StopWaiting();
                }
                else if(isEventWrongTriggered)
                {
                    StopWaiting();
                }
                else if (elapsedTime >= 10f && !tutorial.isStartTutor)
                {
                    Debug.Log("Waktu tunggu habis, melanjutkan perjalanan...");
                    StopWaiting();
                }
            }
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
            MoveToNextWaypoint();
        }
    }
    private void MoveToNextWaypoint()
    {
        if (!isReversing)
        {
            if (index < waypoints.Length - 1)
            {
                index++;
            }
            else
            {
                StartWaiting();
            }
        }
        else
        {
            if (index > 0)
            {
                index--;
            }
            else
            {
                OnNPCReturned?.Invoke(this);
                Destroy(gameObject);
            }
        }

    }

    private void StartWaiting()
    {
        isWaiting = true;
        elapsedTime = 0f;
        buttonMotif.SetActive(true);
        
        
        isWaitTimeActive = true;
    }

    private void StopWaiting()
    {
        isWaiting = false;
        isWaitTimeActive = false;
        if(buttonMotif != null)
        {
            buttonMotif.SetActive(false);
        }
        isReversing = !isReversing;
    }

    // private IEnumerator WaitAtWaypoint()
    // {
    //     isWaiting = true;

    //     yield return new WaitForSeconds(1f);
    //     buttonMotif.SetActive(true);

    //     Debug.Log("NPC sedang menunggu dengan tenang selama 5 detik...");

    //     yield return new WaitForSeconds(5f);

    //     Debug.Log("NPC mulai marah! Menunggu selama 5 detik terakhir...");

    //     yield return new WaitForSeconds(5f);

    //     Debug.Log("NPC selesai marah dan akan kembali ke titik spawn.");

    //     isReversing = !isReversing;

    //     isWaiting = false;
    // }
}
