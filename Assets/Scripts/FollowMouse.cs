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
    public GameObject imageMotif;
    [SerializeField] Sprite[] spriteMotif;
    [SerializeField] Workspace[] workspaceAutomation;
    public Collider[] workspaceArea;
    public Collider playerArea;
    public GameObject instantiatedImage;
    private RectTransform imageRectTransform;
    public bool isDragging;
    [SerializeField] string[] motifNames;
    [SerializeField] string namaMotif;
    public bool onProgress;
    private int randomIndex;
    public delegate void MouseDroppedRight();
    public delegate void MouseDroppedWrong();
    public event MouseDroppedRight OnMouseDroppedRightAuto;
    public event MouseDroppedRight OnMouseDroppedRightManual;
    public event MouseDroppedWrong OnMouseDroppedWrong;
    [Header("References")]
    DrawingManager drawingManager;
    CameraRotation cameraRotation;
    DayManager dayManager;
    Tutorial tutorial;
    [SerializeField] NPCBehav nPCBehavParent;
    AudioSetter audioSetter;


    private void Awake()
    {
        if (gameObject.transform.parent.GetComponent<NPCBehav>() != null)
        {
            nPCBehavParent = gameObject.transform.parent.GetComponent<NPCBehav>();
        }

        audioSetter = GameObject.FindWithTag("Audio").GetComponent<AudioSetter>();
    }
    void Start()
    {
        mainCamera = FindAnyObjectByType<Camera>();
        worldSpaceCanvas = GameObject.Find("Canvas World Space").GetComponent<Canvas>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(InstantiateImage);
        drawingManager = FindAnyObjectByType<DrawingManager>();
        cameraRotation = FindAnyObjectByType<CameraRotation>();
        dayManager = FindAnyObjectByType<DayManager>();
        tutorial = FindAnyObjectByType<Tutorial>();

        for (int i = 0; i < workspaceAutomation.Length; i++)
        {
            string workspaceName = "Workspace Pegawai " + (i + 1);
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
            if (tutorial.isStartTutor)
            {
                namaMotif = "kawung";
            }
            else
            {
                randomIndex = Random.Range(0, motifNames.Length - 3);
                namaMotif = motifNames[randomIndex];
            }
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
        else if (namaMotif == "truntum")
        {
            imagePrefab = imagePrefabs[2];
            imageMotif.GetComponent<Image>().sprite = spriteMotif[2];
        }
        else if (namaMotif == "parang")
        {
            imagePrefab = imagePrefabs[3];
            imageMotif.GetComponent<Image>().sprite = spriteMotif[3];
        }
        else if (namaMotif == "simbut" || namaMotif == "simbu" || namaMotif == "simb" || namaMotif == "sim")
        {
            imagePrefab = imagePrefabs[4];
            imageMotif.GetComponent<Image>().sprite = spriteMotif[4];
        }
    }

    void InstantiateImage()
    {
        instantiatedImage = Instantiate(imagePrefab, worldSpaceCanvas.transform);
        imageRectTransform = instantiatedImage.GetComponent<RectTransform>();
        audioSetter.PlaySFX(audioSetter.OpenPanel);
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
                if (tutorial.isStartTutor && tutorial.isStepDone[1] == false)
                {
                    Debug.Log("tutor manual dulu");
                }
                else if (workspaceAutomation[0].isOnProgress == false)
                {
                    nPCBehavParent.currentWorkspace = workspaceAutomation[0];
                    Destroy(instantiatedImage);
                    isDragging = false;
                    if (!workspaceAutomation[0].isStartAuto)
                    {
                        workspaceAutomation[0].isStartAuto = true;
                    }
                    OnMouseDroppedRightAuto?.Invoke();
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
                Destroy(instantiatedImage);
                isDragging = false;
                Debug.Log("Salah alat");
                OnMouseDroppedWrong?.Invoke();
                Destroy(gameObject);
            }
            else if (playerArea.bounds.Contains(imageRectTransform.position))
            {
                if (tutorial.isStartTutor && tutorial.isStepDone[1] == true)
                {
                    Debug.Log("tutor auto jangan manual");
                }
                else
                {
                    Destroy(instantiatedImage);
                    isDragging = false;
                    cameraRotation.RotateToC(0.1f, 1, 0.6f);
                    // drawingManager.CanvasController(true);
                    drawingManager.MatchMotifKawung();
                    OnMouseDroppedRightManual?.Invoke();
                    Destroy(gameObject);
                }

            }
        }
        else if (namaMotif == "megamendung")
        {
            if (workspaceArea[1] != null && workspaceArea[1].bounds.Contains(imageRectTransform.position))
            {
                if (workspaceAutomation[1].isOnProgress == false)
                {
                    nPCBehavParent.currentWorkspace = workspaceAutomation[1];
                    Destroy(instantiatedImage);
                    isDragging = false;
                    if (!workspaceAutomation[1].isStartAuto)
                    {
                        workspaceAutomation[1].isStartAuto = true;
                    }
                    OnMouseDroppedRightAuto?.Invoke();
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Proses belom selese");
                }

            }
            else if (workspaceArea[0] != null && workspaceArea[0].bounds.Contains(imageRectTransform.position) ||
            workspaceArea[2] != null && workspaceArea[2].bounds.Contains(imageRectTransform.position) ||
            workspaceArea[3] != null && workspaceArea[3].bounds.Contains(imageRectTransform.position) ||
            workspaceArea[4] != null && workspaceArea[4].bounds.Contains(imageRectTransform.position))
            {
                Destroy(instantiatedImage);
                isDragging = false;
                Debug.Log("Salah alat");
                OnMouseDroppedWrong?.Invoke();
                Destroy(gameObject);
            }
            else if (playerArea.bounds.Contains(imageRectTransform.position))
            {
                Destroy(instantiatedImage);
                isDragging = false;
                cameraRotation.RotateToC(0.1f, 1, 0.6f);
                drawingManager.MatchMotifMega();
                OnMouseDroppedRightManual?.Invoke();
                Destroy(gameObject);
            }
        }
        else if (namaMotif == "truntum")
        {
            if (workspaceArea[2] != null && workspaceArea[2].bounds.Contains(imageRectTransform.position))
            {
                if (workspaceAutomation[2].isOnProgress == false)
                {
                    nPCBehavParent.currentWorkspace = workspaceAutomation[2];
                    Destroy(instantiatedImage);
                    isDragging = false;
                    if (!workspaceAutomation[2].isStartAuto)
                    {
                        workspaceAutomation[2].isStartAuto = true;
                    }
                    OnMouseDroppedRightAuto?.Invoke();
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Proses belom selese");
                }

            }
            else if (workspaceArea[0] != null && workspaceArea[0].bounds.Contains(imageRectTransform.position) ||
            workspaceArea[1] != null && workspaceArea[1].bounds.Contains(imageRectTransform.position) ||
            workspaceArea[3] != null && workspaceArea[3].bounds.Contains(imageRectTransform.position) ||
            workspaceArea[4] != null && workspaceArea[4].bounds.Contains(imageRectTransform.position))
            {
                Destroy(instantiatedImage);
                isDragging = false;
                Debug.Log("Salah alat");
                OnMouseDroppedWrong?.Invoke();
                Destroy(gameObject);
            }
            else if (playerArea.bounds.Contains(imageRectTransform.position))
            {
                Destroy(instantiatedImage);
                isDragging = false;
                cameraRotation.RotateToC(0.1f, 1, 0.6f);
                drawingManager.MatchMotifTruntum();
                OnMouseDroppedRightManual?.Invoke();
                Destroy(gameObject);
            }
        }
        else if (namaMotif == "parang")
        {
            if (workspaceArea[3] != null && workspaceArea[3].bounds.Contains(imageRectTransform.position))
            {
                if (workspaceAutomation[3].isOnProgress == false)
                {
                    nPCBehavParent.currentWorkspace = workspaceAutomation[3];
                    Destroy(instantiatedImage);
                    isDragging = false;
                    if (!workspaceAutomation[3].isStartAuto)
                    {
                        workspaceAutomation[3].isStartAuto = true;
                    }
                    OnMouseDroppedRightAuto?.Invoke();
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Proses belom selese");
                }

            }
            else if (workspaceArea[0] != null && workspaceArea[0].bounds.Contains(imageRectTransform.position) ||
            workspaceArea[1] != null && workspaceArea[1].bounds.Contains(imageRectTransform.position) ||
            workspaceArea[2] != null && workspaceArea[2].bounds.Contains(imageRectTransform.position) ||
            workspaceArea[4] != null && workspaceArea[4].bounds.Contains(imageRectTransform.position))
            {
                Destroy(instantiatedImage);
                isDragging = false;
                Debug.Log("Salah alat");
                OnMouseDroppedWrong?.Invoke();
                Destroy(gameObject);
            }
            else if (playerArea.bounds.Contains(imageRectTransform.position))
            {
                Destroy(instantiatedImage);
                isDragging = false;
                cameraRotation.RotateToC(0.1f, 1, 0.6f);
                drawingManager.MatchMotifParang();
                OnMouseDroppedRightManual?.Invoke();
                Destroy(gameObject);
            }
        }
        else if (namaMotif == "simbut")
        {
            if (workspaceArea[4] != null && workspaceArea[4].bounds.Contains(imageRectTransform.position))
            {
                if (workspaceAutomation[4].isOnProgress == false)
                {
                    nPCBehavParent.currentWorkspace = workspaceAutomation[4];
                    Destroy(instantiatedImage);
                    isDragging = false;
                    if (!workspaceAutomation[4].isStartAuto)
                    {
                        workspaceAutomation[4].isStartAuto = true;
                    }
                    OnMouseDroppedRightAuto?.Invoke();
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Proses belom selese");
                }

            }
            else if (workspaceArea[0] != null && workspaceArea[0].bounds.Contains(imageRectTransform.position) ||
            workspaceArea[1] != null && workspaceArea[1].bounds.Contains(imageRectTransform.position) ||
            workspaceArea[2] != null && workspaceArea[2].bounds.Contains(imageRectTransform.position) ||
            workspaceArea[3] != null && workspaceArea[3].bounds.Contains(imageRectTransform.position))
            {
                Destroy(instantiatedImage);
                isDragging = false;
                Debug.Log("Salah alat");
                OnMouseDroppedWrong?.Invoke();
                Destroy(gameObject);
            }
            else if (playerArea.bounds.Contains(imageRectTransform.position))
            {
                Destroy(instantiatedImage);
                isDragging = false;
                cameraRotation.RotateToC(0.1f, 1, 0.6f);
                drawingManager.MatchMotifSimbut();
                OnMouseDroppedRightManual?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
