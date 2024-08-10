using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBuyingManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonMotif;

    public void SetupButton(Canvas canvas, Camera camera)
    {
        buttonMotif.transform.SetParent(canvas.transform);
        if(buttonMotif.TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
        {
            faceCamera.Camera = camera;
        }
    }
}
