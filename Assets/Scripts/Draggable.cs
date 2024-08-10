using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
   private RectTransform rectTransform;
    private Canvas canvas;
    public Vector2 targetPosition = new Vector2(200, 100);
    public Vector2 nextPosition = new Vector2(200, 100);
    public GameObject dragablePrefab;
    public float tolerance = 20f;

    private void Awake() 
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();    
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        InstantiateNewDraggable();
        if(IsWithinTargetRange(rectTransform.anchoredPosition))
        {
            Debug.Log("Perfect");
        }
        else
        {
            Debug.Log("Miss");
        }
    }

    private bool IsWithinTargetRange(Vector2 position)
    {
        float distance = Vector2.Distance(position, targetPosition);
        return distance <= tolerance;
    }

    private void InstantiateNewDraggable()
    {
        GameObject newDraggable = Instantiate(dragablePrefab, canvas.transform);
        newDraggable.GetComponent<RectTransform>().anchoredPosition = nextPosition;
    }
}
