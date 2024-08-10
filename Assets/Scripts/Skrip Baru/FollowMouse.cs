using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowMouse : MonoBehaviour
{
    public Camera mainCamera;           // The camera in the scene
    public Canvas worldSpaceCanvas;     // The canvas in world space
    public GameObject imagePrefab;      // The image prefab that will follow the mouse
    public Collider specificArea; 
    private GameObject instantiatedImage; // Reference to the instantiated image
    private RectTransform imageRectTransform;
    private bool isDragging;
    

    void Start()
    {
        // Assign the button's onClick event
        Button button = GetComponent<Button>();
        button.onClick.AddListener(InstantiateImage);
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
        // Instantiate the image as a child of the canvas
        instantiatedImage = Instantiate(imagePrefab, worldSpaceCanvas.transform);
        imageRectTransform = instantiatedImage.GetComponent<RectTransform>();
    }

    void FollowTheMouse()
    {
        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Convert screen space mouse position to world space
        Vector3 worldPosition;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(worldSpaceCanvas.GetComponent<RectTransform>(), mousePosition, mainCamera, out worldPosition))
        {
            
                // Set the position of the instantiated image
                imageRectTransform.position = worldPosition;
                isDragging = true;
            
            
        }
    }

    void CheckAndDestroy()
    {
        // Check if the instantiated image is within the specific area
        if (specificArea.bounds.Contains(imageRectTransform.position))
        {
            // Destroy the instantiated image
            Destroy(instantiatedImage);
            isDragging = false;
        }
    }
}
