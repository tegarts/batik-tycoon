using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILineRenderer : MonoBehaviour
{
    // public RectTransform movingObject; // The UI element being dragged
    // private LineRenderer lineRenderer;
    // public Camera uiCamera;  // The camera used by the Canvas

    // void Start()
    // {
    //     lineRenderer = GetComponent<LineRenderer>();
    //     lineRenderer.positionCount = 1;
    //     lineRenderer.SetPosition(0, uiCamera.ScreenToWorldPoint(movingObject.position));
    // }

    // void Update()
    // {
    //     Vector3 currentWorldPos = uiCamera.ScreenToWorldPoint(movingObject.position);
    //     currentWorldPos.z = 0; // Align with the camera

    //     // Add the current position to the line
    //     lineRenderer.positionCount += 1;
    //     lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentWorldPos);
    // }

    // private void AddPointLine()
    // {
    //     pointIndex++;
    //     lineRenderer.positionCount = pointIndex + 1;
    //     lineRenderer.SetPosition(pointIndex, transform.position);
    // }
}
