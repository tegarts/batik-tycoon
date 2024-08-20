using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drawing : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Vector2 defaultPos;
    private Vector3 defalutPosForLineRenderer;
    private bool isDragging;
    public bool isFinish;
    DrawingManager drawingManager;
    private LineRenderer lineRenderer;
    private int pointIndex = 0;
    public int scoreCanting;
    [SerializeField] bool[] checkpoints;
    bool isSetDefaultPos = false;
    CameraRotation cameraRotation;
    Money money;

    [Header("UI Related")]
    [SerializeField] private Canvas canvas;
    [SerializeField] GameObject panelAfterCanting;
    [SerializeField] GameObject circleTinta;
    [SerializeField] GameObject jalurTinta;
    private RectTransform rectTransform;
    [SerializeField] RectTransform parentRectTransform;
    [SerializeField] Image filledImage;


    private void OnEnable()
    {
        gameObject.GetComponent<Image>().enabled = true;
        gameObject.GetComponent<LineRenderer>().enabled = true;
        jalurTinta.SetActive(true);
        filledImage.fillAmount = 0;
        isFinish = false;
    }
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultPos = rectTransform.anchoredPosition;
        canvas = GetComponentInParent<Canvas>();
        drawingManager = FindAnyObjectByType<DrawingManager>();

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        cameraRotation = FindAnyObjectByType<CameraRotation>();

        panelAfterCanting.SetActive(false);
        money = FindObjectOfType<Money>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isFinish)
        {
            if(!isSetDefaultPos)
            {
                isSetDefaultPos = true;
                defalutPosForLineRenderer = rectTransform.position;
                lineRenderer.SetPosition(0, defalutPosForLineRenderer);
            }
            isDragging = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector2 movePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out movePos);

            Rect parentRect = parentRectTransform.rect;

            // Clamp the position within the boundaries
            movePos.x = Mathf.Clamp(movePos.x, parentRect.xMin, parentRect.xMax);
            movePos.y = Mathf.Clamp(movePos.y, parentRect.yMin, parentRect.yMax);

            rectTransform.anchoredPosition = movePos;
            AddPointLine();
        }
        else
        {
            rectTransform.anchoredPosition = defaultPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Boundary"))
        {
            Debug.Log("diluar gan");
            scoreCanting -= 5;
        }
        else if (other.gameObject.CompareTag("target"))
        {
            isFinish = true;
            isDragging = false;
            jalurTinta.SetActive(false);
            pointIndex = 0;
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, defalutPosForLineRenderer);
            gameObject.GetComponent<Image>().enabled = false;
            gameObject.GetComponent<LineRenderer>().enabled = false;
            StartCoroutine(ImageFilled(1.5f));

            if (scoreCanting < 0)
            {
                scoreCanting = 0;
            }
            Debug.Log("Score Canting: " + scoreCanting);
            money.AddMoney(scoreCanting * 6000);
            scoreCanting = 0;
        }

        if (other.gameObject.CompareTag("cp1"))
        {
            if (checkpoints[0] == false)
            {
                checkpoints[0] = true;
                scoreCanting += 10;
            }
        }
        else if (other.gameObject.CompareTag("cp2"))
        {
            if (checkpoints[1] == false)
            {
                checkpoints[1] = true;
                scoreCanting += 10;
            }
        }
        else if (other.gameObject.CompareTag("cp3"))
        {
            if (checkpoints[2] == false)
            {
                checkpoints[2] = true;
                scoreCanting += 10;
            }
        }
        else if (other.gameObject.CompareTag("cp4"))
        {
            if (checkpoints[3] == false)
            {
                checkpoints[3] = true;
                scoreCanting += 10;
            }
        }
        else if (other.gameObject.CompareTag("cp5"))
        {
            if (checkpoints[4] == false)
            {
                checkpoints[4] = true;
                scoreCanting += 10;
            }
        }
        else if (other.gameObject.CompareTag("cp6"))
        {
            if (checkpoints[5] == false)
            {
                checkpoints[5] = true;
                scoreCanting += 10;
            }
        }
        else if (other.gameObject.CompareTag("cp7"))
        {
            if (checkpoints[6] == false)
            {
                checkpoints[6] = true;
                scoreCanting += 10;
            }
        }
        else if (other.gameObject.CompareTag("cp8"))
        {
            if (checkpoints[7] == false)
            {
                checkpoints[7] = true;
                scoreCanting += 10;
            }
        }
        else if (other.gameObject.CompareTag("cp9"))
        {
            if (checkpoints[8] == false)
            {
                checkpoints[8] = true;
                scoreCanting += 10;
            }
        }
        else if (other.gameObject.CompareTag("cp10"))
        {
            if (checkpoints[9] == false)
            {
                checkpoints[9] = true;
                scoreCanting += 10;
            }
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

    IEnumerator ImageFilled(float duration)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float elapsed = Time.time - startTime;
            float percentageCompleted = elapsed / duration;

            filledImage.fillAmount = percentageCompleted;
            yield return null;
        }
        filledImage.fillAmount = 1;

        panelAfterCanting.SetActive(true);
    }

    public void ButtonExitFromPanel()
    {
        panelAfterCanting.SetActive(false);
        drawingManager.CanvasController(false);
        cameraRotation.RotateToA(0.5f);
    }

}
