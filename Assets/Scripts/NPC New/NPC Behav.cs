using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
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
    [SerializeField] GameObject instantiatedImage;
    public event Action<NPCBehav> OnNPCReturned;
    [SerializeField] bool isEventRightAutoTriggered = false;
    [SerializeField] bool isEventRightManualTriggered = false;
    [SerializeField] bool isEventWrongTriggered = false;
    [SerializeField] bool isEventWorkspaceTriggered = false;
    [SerializeField] bool isEventDrawingTriggered = false;
    private int scoreCanting;
    [SerializeField] bool isAddAngryReaction, isAddFlatReaction, isAddHappyReaction;
    private float elapsedTime = 0f;
    private float timePassed = 0f;
    private bool isTimePassed;
    private bool isWaitTimeActive = false;
    public Workspace currentWorkspace;
    [SerializeField] Drawing drawing;
    Tutorial tutorial;
    [Header("UI Related")]
    [SerializeField] private GameObject buttonMotif;
    [SerializeField] private GameObject reactionBubble;
    [SerializeField] Sprite[] reactions;
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
        reactionBubble.transform.SetParent(canvasWorldSpace.transform);
        if (reactionBubble.TryGetComponent<FaceCamera>(out FaceCamera faceCam))
        {
            faceCam.Camera = cam;
        }


        if (followMouse != null)
        {
            followMouse.OnMouseDroppedRightAuto += HandleDroppedRightAuto;
            followMouse.OnMouseDroppedRightManual += HandleDroppedRightManual;
            followMouse.OnMouseDroppedWrong += HandleDroppedWrong;
        }
        buttonMotif.SetActive(false);
        reactionBubble.SetActive(false);

    }

    private void HandleDroppedRightAuto()
    {
        isEventRightAutoTriggered = true;
    }
    private void HandleDroppedRightManual()
    {
        isEventRightManualTriggered = true;
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

    private void OnScoreGained(int score)
    {
        scoreCanting = score;
    }

    private void Update()
    {
        if (isWaiting)
        {
            anim.SetBool("IsWalking", false);
            if (isWaitTimeActive)
            {
                elapsedTime += Time.deltaTime;

                if (followMouse.instantiatedImage != null)
                {
                    instantiatedImage = followMouse.instantiatedImage;
                }

                if (isEventRightAutoTriggered)
                {
                    if (isTimePassed == false)
                    {
                        timePassed = elapsedTime;
                        isTimePassed = true;
                    }

                    if (currentWorkspace != null)
                    {
                        currentWorkspace.OnWorkspaceCompleted += HandleWorkspaceCompleted;
                    }

                    // if (drawing != null)
                    // {
                    //     drawing.OnDrawingCompleted += HandleDrawingCompleted;
                    //     drawing.OnDrawingScore += OnScoreGained;
                    // }
                    if (isEventWorkspaceTriggered)
                    {
                        
                        if (timePassed >= 10f)
                        {
                            
                            reactionBubble.GetComponent<Image>().sprite = reactions[1];
                            reactionBubble.SetActive(true);
                            if (!isAddFlatReaction)
                            {
                                Daily.instance.IncreaseProgress(15);
                                Daily.instance.flatReaction++;
                                anim.SetBool("IsFlat", true);
                            StartCoroutine(DisplayReaction("IsFlat"));
                                isAddFlatReaction = true;
                            }
                        }
                        else
                        {
                         
                            reactionBubble.GetComponent<Image>().sprite = reactions[0];
                            reactionBubble.SetActive(true);
                            if (!isAddHappyReaction)
                            {
                                Daily.instance.IncreaseProgress(20);
                                Daily.instance.happyReaction++;
                                   anim.SetBool("IsHappy", true);
                            StartCoroutine(DisplayReaction("IsHappy"));
                                isAddHappyReaction = true;
                            }
                        }
                    }
                    // else if (isEventDrawingTriggered)
                    // {
                    //     if (scoreCanting >= 80)
                    //     {
                    //         reactionBubble.GetComponent<Image>().sprite = reactions[0];
                    //         reactionBubble.SetActive(true);
                    //         if (!isAddHappyReaction)
                    //         {
                    //             Daily.instance.IncreaseProgress(25);
                    //             Daily.instance.happyReaction++;
                    //             anim.SetBool("IsHappy", true);
                    //             StartCoroutine(DisplayReaction("IsHappy"));
                    //             isAddHappyReaction = true;
                    //         }
                    //     }
                    //     else if (scoreCanting >= 60)
                    //     {
                    //         reactionBubble.GetComponent<Image>().sprite = reactions[1];
                    //         reactionBubble.SetActive(true);
                    //         if (!isAddFlatReaction)
                    //         {
                    //             Daily.instance.IncreaseProgress(20);
                    //             Daily.instance.flatReaction++;
                    //             anim.SetBool("IsFlat", true);
                    //             StartCoroutine(DisplayReaction("IsFlat"));
                    //             isAddFlatReaction = true;
                    //         }
                    //     }
                    //     else
                    //     {
                    //         reactionBubble.GetComponent<Image>().sprite = reactions[2];
                    //         reactionBubble.SetActive(true);
                    //         if (!isAddAngryReaction)
                    //         {
                    //             Daily.instance.angryReaction++;
                    //             anim.SetBool("IsAngry", true);
                    //             StartCoroutine(DisplayReaction("IsAngry"));
                    //             isAddAngryReaction = true;
                    //         }
                    //     }
                    // }

                    // isEventTriggered = false;
                    // StopWaiting();
                }
                else if(isEventRightManualTriggered)
                {
                    drawing = FindAnyObjectByType<Drawing>();
                    if (drawing != null)
                    {
                        drawing.OnDrawingCompleted += HandleDrawingCompleted;
                        drawing.OnDrawingScore += OnScoreGained;
                    }

                    if (isEventDrawingTriggered)
                    {
                        if (scoreCanting >= 80)
                        {
                            reactionBubble.GetComponent<Image>().sprite = reactions[0];
                            reactionBubble.SetActive(true);
                            if (!isAddHappyReaction)
                            {
                                Daily.instance.IncreaseProgress(25);
                                Daily.instance.happyReaction++;
                                anim.SetBool("IsHappy", true);
                                StartCoroutine(DisplayReaction("IsHappy"));
                                isAddHappyReaction = true;
                            }
                        }
                        else if (scoreCanting >= 60)
                        {
                            reactionBubble.GetComponent<Image>().sprite = reactions[1];
                            reactionBubble.SetActive(true);
                            if (!isAddFlatReaction)
                            {
                                Daily.instance.IncreaseProgress(20);
                                Daily.instance.flatReaction++;
                                anim.SetBool("IsFlat", true);
                                StartCoroutine(DisplayReaction("IsFlat"));
                                isAddFlatReaction = true;
                            }
                        }
                        else
                        {
                            reactionBubble.GetComponent<Image>().sprite = reactions[2];
                            reactionBubble.SetActive(true);
                            if (!isAddAngryReaction)
                            {
                                Daily.instance.angryReaction++;
                                anim.SetBool("IsAngry", true);
                                StartCoroutine(DisplayReaction("IsAngry"));
                                isAddAngryReaction = true;
                            }
                        }
                    }
                    
                }
                else if (isEventWrongTriggered)
                {
                   
                    reactionBubble.GetComponent<Image>().sprite = reactions[2];
                    reactionBubble.SetActive(true);
                    if (!isAddAngryReaction)
                    {
                        Daily.instance.angryReaction++;
                         anim.SetBool("IsAngry", true);
                    StartCoroutine(DisplayReaction("IsAngry"));
                        isAddAngryReaction = true;
                    }
                }
                else if (elapsedTime >= 15f && !tutorial.isStartTutor)
                {
                    UnityEngine.Debug.Log("Waktu tunggu habis, melanjutkan perjalanan...");
                    reactionBubble.GetComponent<Image>().sprite = reactions[2];
                    reactionBubble.SetActive(true);
                    if (!isAddAngryReaction)
                    {
                        Daily.instance.angryReaction++;
                        anim.SetBool("IsAngry", true);
                    StartCoroutine(DisplayReaction("IsAngry"));
                        isAddAngryReaction = true;
                    }
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

    IEnumerator DisplayReaction(string animBool)
    {
        Animator animator = anim;
        // AnimatorStateInfo animState = animator.GetCurrentAnimatorStateInfo(0);
        // UnityEngine.Debug.Log(animator.GetCurrentAnimatorStateInfo(0));
        // yield return new WaitUntil(() => animState.normalizedTime >= 1f && !animator.IsInTransition(0));
        yield return new WaitForSeconds(1.6f);
        animator.SetBool(animBool, false);

        StopWaiting();
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
        if (buttonMotif != null)
        {
            buttonMotif.SetActive(false);
        }
        if (instantiatedImage != null)
        {
            Destroy(instantiatedImage);
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
