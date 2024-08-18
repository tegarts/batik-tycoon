using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingManager : MonoBehaviour
{
    public GameObject canvasCanting;

    private void Start() 
    {
        canvasCanting.SetActive(false);    
    }

    public void CanvasController(bool condition)
    {
        canvasCanting.SetActive(condition);
    }

    private void Update() {
        if(canvasCanting.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                CanvasController(false);
            }
        }
    }
}
