using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBuyingManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonMotif;
    [SerializeField] private Camera cam;
    [SerializeField] private Canvas canvasWorldSpace;

    // public void SetupButton(Canvas canvas, Camera camera)
    // {
    //     buttonMotif.transform.SetParent(canvas.transform);
    //     if(buttonMotif.TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
    //     {
    //         faceCamera.Camera = camera;
    //     }
    // }

    private void Start() 
    {
        buttonMotif = GetComponentInChildren<FollowMouse>().gameObject;
        cam = FindAnyObjectByType<Camera>();
        canvasWorldSpace = GameObject.Find("Canvas World Space").GetComponent<Canvas>();
        buttonMotif.transform.SetParent(canvasWorldSpace.transform);
        if(buttonMotif.TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
        {
            faceCamera.Camera = cam;
        }
    }
}
