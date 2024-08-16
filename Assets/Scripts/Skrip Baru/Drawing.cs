using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drawing : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Vector2 defaultPos;
    private Vector3 defalutPosForLineRenderer;
    private bool isDragging;
    private bool wasMouseReleased;
    private bool isOutside;
    [SerializeField] private Canvas canvas;
    DrawingManager drawingManager;
    private LineRenderer lineRenderer;
    private int pointIndex = 0;

    private void OnEnable() 
    {
        wasMouseReleased = true;    
    }
    private void Start()
    {
        wasMouseReleased = true;
        rectTransform = GetComponent<RectTransform>();
        defaultPos = rectTransform.anchoredPosition;
        defalutPosForLineRenderer = rectTransform.position;
        canvas = GetComponentInParent<Canvas>();
        drawingManager = FindAnyObjectByType<DrawingManager>();

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, defalutPosForLineRenderer);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        wasMouseReleased = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging && !isOutside && wasMouseReleased)
        {
            Vector2 movePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out movePos);

                rectTransform.anchoredPosition = movePos;
                AddPointLine();
        }
        else
        {
            rectTransform.anchoredPosition = defaultPos;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Boundary"))
        {
            isOutside = true;
            wasMouseReleased = false;
            pointIndex = 0;
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, defalutPosForLineRenderer);
            isOutside = false;
        }

        else if(other.gameObject.CompareTag("target"))
        {
            isOutside = true;
            wasMouseReleased = false;
            pointIndex = 0;
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, defalutPosForLineRenderer);
            isOutside = false;
            rectTransform.anchoredPosition = defaultPos;
            drawingManager.CanvasController(false);
        }
    }

    private void AddPointLine()
    {
        pointIndex++;
        lineRenderer.positionCount = pointIndex + 1;
        Vector3 currentPos = rectTransform.position;
         currentPos.z = rectTransform.position.z - 0.1f;

        lineRenderer.SetPosition(pointIndex, currentPos);
    }

}
