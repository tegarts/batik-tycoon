using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowMouse : MonoBehaviour
{
    public Camera mainCamera;
    public Canvas worldSpaceCanvas;
    public GameObject imagePrefab;
    public Collider[] kawungWorkerArea;
    public Collider[] megaWorkerArea;
    public Collider playerArea;
    private GameObject instantiatedImage;
    private RectTransform imageRectTransform;
    private bool isDragging;
    [SerializeField] WorkerAutomation[] kawungWorkerAutomation;
    [SerializeField] WorkerAutomation[] megaWorkerAutomation;
    PlayerManager playerManager;
    [SerializeField] private string namaMotif;


    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(InstantiateImage);
        playerManager = FindAnyObjectByType<PlayerManager>();
    }

    void Update()
    {
        if (instantiatedImage != null)
        {
            FollowTheMouse();
        }

        if (isDragging && Input.GetMouseButtonDown(0))
        {
            CheckAndDestroy();
        }
    }

    void InstantiateImage()
    {
        instantiatedImage = Instantiate(imagePrefab, worldSpaceCanvas.transform);
        imageRectTransform = instantiatedImage.GetComponent<RectTransform>();
    }

    void FollowTheMouse()
    {
        Vector3 mousePosition = Input.mousePosition;

        Vector3 worldPosition;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(worldSpaceCanvas.GetComponent<RectTransform>(), mousePosition, mainCamera, out worldPosition))
        {
            imageRectTransform.position = worldPosition;
            isDragging = true;
        }
    }

    void CheckAndDestroy()
    {
        if (namaMotif == "kawung")
        {
            if (kawungWorkerArea[0].bounds.Contains(imageRectTransform.position))
            {
                Destroy(instantiatedImage);
                isDragging = false;
                if (!kawungWorkerAutomation[0].isStartAuto)
                {
                    kawungWorkerAutomation[0].isStartAuto = true;
                }
            }
            else if (kawungWorkerArea[1].bounds.Contains(imageRectTransform.position))
            {
                Destroy(instantiatedImage);
                isDragging = false;
                if (!kawungWorkerAutomation[1].isStartAuto)
                {
                    kawungWorkerAutomation[1].isStartAuto = true;
                }
            }
            else if (megaWorkerArea[0].bounds.Contains(imageRectTransform.position))
            {
                Destroy(instantiatedImage);
                isDragging = false;
                Debug.Log("Salah alat");
            }
            else if (megaWorkerArea[1].bounds.Contains(imageRectTransform.position))
            {
                Destroy(instantiatedImage);
                isDragging = false;
                Debug.Log("Salah alat");
            }
            else if (playerArea.bounds.Contains(imageRectTransform.position))
            {
                Destroy(instantiatedImage);
                isDragging = false;
                playerManager.CanvasController(true);
            }
        }

    }
}
