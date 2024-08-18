using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowMouse : MonoBehaviour
{
    public Camera mainCamera;
    public Canvas worldSpaceCanvas;
    public GameObject imagePrefab;
    public Collider[] workspaceArea;
    public Collider playerArea;
    private GameObject instantiatedImage;
    private RectTransform imageRectTransform;
    private bool isDragging;
    [SerializeField] Workspace[] workspaceAutomation;
    DrawingManager drawingManager;
    [SerializeField] private string namaMotif;
    CameraRotation cameraRotation;


    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(InstantiateImage);
        drawingManager = FindAnyObjectByType<DrawingManager>();
        cameraRotation = FindAnyObjectByType<CameraRotation>();
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
            if (workspaceArea[0].bounds.Contains(imageRectTransform.position))
            {
                Debug.Log("test");
                Destroy(instantiatedImage);
                isDragging = false;
                if (!workspaceAutomation[0].isStartAuto)
                {
                    workspaceAutomation[0].isStartAuto = true;
                }
            }
            else if (workspaceArea[1].bounds.Contains(imageRectTransform.position) || workspaceArea[2].bounds.Contains(imageRectTransform.position) || workspaceArea[3].bounds.Contains(imageRectTransform.position) || workspaceArea[4].bounds.Contains(imageRectTransform.position))
            {
                // TODO - Tambah kondisi pembeli marah / gak jadi beli
                Destroy(instantiatedImage);
                isDragging = false;
                Debug.Log("Salah alat");
            }
            else if (playerArea.bounds.Contains(imageRectTransform.position))
            {
                Destroy(instantiatedImage);
                isDragging = false;
                cameraRotation.RotateToB(0.5f);
                drawingManager.CanvasController(true);
            }
        }
        else if (namaMotif == "megamendung")
        {
            if (workspaceArea[1].bounds.Contains(imageRectTransform.position))
            {
                Destroy(instantiatedImage);
                isDragging = false;
                if (!workspaceAutomation[0].isStartAuto)
                {
                    workspaceAutomation[0].isStartAuto = true;
                }
            }
            else if (workspaceArea[0].bounds.Contains(imageRectTransform.position) || workspaceArea[2].bounds.Contains(imageRectTransform.position) || workspaceArea[3].bounds.Contains(imageRectTransform.position) || workspaceArea[4].bounds.Contains(imageRectTransform.position))
            {
                // TODO - Tambah kondisi pembeli marah / gak jadi beli
                Destroy(instantiatedImage);
                isDragging = false;
                Debug.Log("Salah alat");
            }
            else if (playerArea.bounds.Contains(imageRectTransform.position))
            {
                Destroy(instantiatedImage);
                isDragging = false;
                drawingManager.CanvasController(true);
            }
        }

    }
}
