using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameControl : MonoBehaviour
{
    float distance = 3.4f;
    private LineRenderer lineRenderer;
    private int pointIndex = 0;
    private Vector3 defaultPos;
    private bool isResetting;
    [SerializeField] private bool wasMouseReleased;

    private void OnEnable() {
        wasMouseReleased = true;
    }

    private void Start()
    {
        defaultPos = transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        wasMouseReleased = true;
    }
    private void OnMouseDrag()
    {
        if (!isResetting && wasMouseReleased)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objectPosition;

            AddPointLine();
        }


    }

    private void OnMouseUp() 
    {
        wasMouseReleased = true;    
    }

    private void OnTriggerExit(Collider other) 
    {
        StartCoroutine(ResetPosition());
    }

    IEnumerator ResetPosition()
    {
        isResetting = true;
        wasMouseReleased = false;
        transform.position = defaultPos;
        pointIndex = 0;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, defaultPos);
        yield return new WaitForSeconds(0.5f);
        isResetting = false;
    }

    private void AddPointLine()
    {
        pointIndex++;
        lineRenderer.positionCount = pointIndex + 1;
        lineRenderer.SetPosition(pointIndex, transform.position);
    }
}
