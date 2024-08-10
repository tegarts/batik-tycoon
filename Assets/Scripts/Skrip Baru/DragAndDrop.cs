using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject objectPrefab;
    private bool isDragging;
    private Vector3 mousePosition;
    private GameObject currentObject;

    private void Update() {
          if (isDragging)
          {
            mousePosition = Input.mousePosition;
            currentObject.GetComponent<RectTransform>().position = mousePosition;
          }
    }
     public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnButtonClick()
    {
        gameObject.GetComponent<Button>().interactable = false;

            currentObject = Instantiate(objectPrefab, transform);
            isDragging = true;
    }
}
