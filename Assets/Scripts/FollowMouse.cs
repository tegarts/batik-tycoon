using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowMouse : MonoBehaviour
{
    public Camera mainCamera;
    public Canvas worldSpaceCanvas;
    public GameObject[] imagePrefabs;
    [SerializeField] GameObject imagePrefab;
    [SerializeField] GameObject imageMotif;
    [SerializeField] Sprite[] spriteMotif;
    [SerializeField] Workspace[] workspaceAutomation;
    public Collider[] workspaceArea;
    public Collider playerArea;
    private GameObject instantiatedImage;
    private RectTransform imageRectTransform;
    private bool isDragging;
    [SerializeField] string[] motifNames;
    [SerializeField] string namaMotif;
    private int randomIndex;
    [Header("References")]
    DrawingManager drawingManager;
    CameraRotation cameraRotation;
    DayManager dayManager;



    void Start()
    {
        mainCamera = FindAnyObjectByType<Camera>();
        worldSpaceCanvas = GameObject.Find("Canvas World Space").GetComponent<Canvas>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(InstantiateImage);
        drawingManager = FindAnyObjectByType<DrawingManager>();
        cameraRotation = FindAnyObjectByType<CameraRotation>();
        dayManager = FindAnyObjectByType<DayManager>();

        for (int i = 0; i < workspaceAutomation.Length; i++)
        {
            string workspaceName = "Workspace Motif " + (i + 1);
            Workspace workspaceComponent = GameObject.Find(workspaceName)?.GetComponent<Workspace>();
            if (workspaceComponent != null)
            {
                workspaceAutomation[i] = workspaceComponent;
            }
        }

        for (int i = 0; i < workspaceArea.Length; i++)
        {
            string workspaceAreaName = "area" + (i + 1);
            Collider workspaceAreaComponent = GameObject.FindWithTag(workspaceAreaName)?.GetComponent<Collider>();
            if (workspaceAreaComponent != null)
            {
                workspaceArea[i] = workspaceAreaComponent;
            }
        }

        playerArea = GameObject.FindWithTag("areaplayer").GetComponent<Collider>();

        RandomMotif();
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

    private void RandomMotif()
    {
        if (dayManager.day < 3)
        {
            randomIndex = Random.Range(0, motifNames.Length - 3);
            namaMotif = motifNames[randomIndex];
        }
        else if (dayManager.day < 5)
        {
            randomIndex = Random.Range(0, motifNames.Length - 2);
            namaMotif = motifNames[randomIndex];
        }
        else if (dayManager.day < 7)
        {
            randomIndex = Random.Range(0, motifNames.Length - 1);
            namaMotif = motifNames[randomIndex];
        }
        else if (dayManager.day >= 7)
        {
            randomIndex = Random.Range(0, motifNames.Length);
            namaMotif = motifNames[randomIndex];
        }

        if (namaMotif == "kawung")
        {
            imagePrefab = imagePrefabs[0];
            imageMotif.GetComponent<Image>().sprite = spriteMotif[0];
        }
        else if (namaMotif == "megamendung")
        {
            imagePrefab = imagePrefabs[1];
            imageMotif.GetComponent<Image>().sprite = spriteMotif[1];
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
                if (workspaceAutomation[0].isOnProgress == false)
                {
                    Destroy(instantiatedImage);
                    isDragging = false;
                    if (!workspaceAutomation[0].isStartAuto)
                    {
                        workspaceAutomation[0].isStartAuto = true;
                    }
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Proses belom selese");
                }

            }
            else if (workspaceArea[1] != null && workspaceArea[1].bounds.Contains(imageRectTransform.position) ||
            workspaceArea[2] != null && workspaceArea[2].bounds.Contains(imageRectTransform.position) ||
            workspaceArea[3] != null && workspaceArea[3].bounds.Contains(imageRectTransform.position) ||
            workspaceArea[4] != null && workspaceArea[4].bounds.Contains(imageRectTransform.position))
            {
                // TODO - Tambah kondisi pembeli marah / gak jadi beli
                Destroy(instantiatedImage);
                isDragging = false;
                Debug.Log("Salah alat");
                Destroy(gameObject);
            }
            else if (playerArea.bounds.Contains(imageRectTransform.position))
            {
                Destroy(instantiatedImage);
                isDragging = false;
                cameraRotation.RotateToB(0.5f);
                drawingManager.CanvasController(true);
                drawingManager.MatchMotifKawung();
                Destroy(gameObject);
            }
        }
        else if (namaMotif == "megamendung")
        {
            if (workspaceArea[1] != null && workspaceArea[1].bounds.Contains(imageRectTransform.position))
            {
                Destroy(instantiatedImage);
                isDragging = false;
                if (!workspaceAutomation[1].isStartAuto)
                {
                    workspaceAutomation[1].isStartAuto = true;
                }
                Destroy(gameObject);
            }
            else if (workspaceArea[0] != null && workspaceArea[0].bounds.Contains(imageRectTransform.position) ||
            workspaceArea[2] != null && workspaceArea[2].bounds.Contains(imageRectTransform.position) ||
            workspaceArea[3] != null && workspaceArea[3].bounds.Contains(imageRectTransform.position) ||
            workspaceArea[4] != null && workspaceArea[4].bounds.Contains(imageRectTransform.position))
            {
                // TODO - Tambah kondisi pembeli marah / gak jadi beli
                Destroy(instantiatedImage);
                isDragging = false;
                Debug.Log("Salah alat");
                Destroy(gameObject);
            }
            else if (playerArea.bounds.Contains(imageRectTransform.position))
            {
                Destroy(instantiatedImage);
                isDragging = false;
                drawingManager.CanvasController(true);
                drawingManager.MatchMotifMega();
                Destroy(gameObject);
            }
        }

    }
}
