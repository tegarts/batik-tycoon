using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CantingMechanic : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    MiniGameManager miniGameManager;
    public GameObject objectPrefab;
    [SerializeField] private GameObject currentObject;
    public bool isDragging = false;
    [SerializeField] private int patternAmount;
    private int currentPattern;
    private Vector3 mousePosition;
    private bool isInside;

    private void Start() 
    {
        miniGameManager = FindAnyObjectByType<MiniGameManager>();
    }

    private void Update()
    {
        if (isDragging && currentObject != null)
        {
            mousePosition = Input.mousePosition;
            currentObject.GetComponent<RectTransform>().position = mousePosition;

            if (mousePosition.x >= 160 && mousePosition.x <= 800 && mousePosition.y >= 290 && mousePosition.y <= 930)
            {
                isInside = true;
                Color color = currentObject.GetComponent<Image>().color;
                color.a = 1f;
                currentObject.GetComponent<Image>().color = color;
            }
            else
            {
                isInside = false;
                Color color = currentObject.GetComponent<Image>().color;
                color.a = 0.5f;
                currentObject.GetComponent<Image>().color = color;
            }
        }


    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (currentObject != null)
        {
            if (isInside)
            {
                isDragging = false;
                miniGameManager.motif1_1[currentPattern] = currentObject;
                patternAmount--;
                currentPattern++;

                if (!isDragging && patternAmount > 0)
                {
                    mousePosition = Input.mousePosition;
                    currentObject = Instantiate(objectPrefab, transform);
                    currentObject.GetComponent<RectTransform>().position = mousePosition;
                    isDragging = true;
                }

                if(patternAmount <= 0)
                {
                    miniGameManager.level1isDone = true;
                }
            }
        }
    }

    public void OnButtonClick()
    {
        gameObject.GetComponent<Button>().interactable = false;

        if (currentObject == null)
        {
            Vector3 mousePosition = Input.mousePosition;
            currentObject = Instantiate(objectPrefab, transform);
            currentObject.GetComponent<RectTransform>().position = mousePosition;
            isDragging = true;
        }
    }

}
