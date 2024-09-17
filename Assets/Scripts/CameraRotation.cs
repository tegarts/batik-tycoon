using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private Quaternion rotationA = Quaternion.Euler(44.81f, -47f, 0f);
    private Quaternion rotationB = Quaternion.Euler(0f, -180f, 0f);
    private Quaternion rotationC = Quaternion.Euler(40f, -60.75f, 0f);

    private Coroutine rotationCoroutine;
    DrawingManager drawingManager;

    private void Start() {
        drawingManager = FindObjectOfType<DrawingManager>();
        transform.rotation = rotationA;
    }

    public void RotateToA(float duration)
    {
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
        }
        rotationCoroutine = StartCoroutine(RotateCamera(rotationA, duration, null));
    }

    public void RotateToB(float duration)
    {
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
        }
        rotationCoroutine = StartCoroutine(RotateCamera(rotationB, duration, null));
    }

    public void RotateToC(float duration, float projectionSize, float projectionDuration)
    {
        if(rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
        }
        rotationCoroutine = StartCoroutine(RotateCamera(rotationC, duration, () =>
        {
            StartCoroutine(ProjectionSize(projectionSize, projectionDuration, () =>
            {
                Debug.Log("SSSSSSSSSSSSS");
                Camera.main.orthographicSize = 11;
                RotateToB(0f);
                drawingManager.CanvasController(true);
            }));
            }));        
    }

    private IEnumerator RotateCamera(Quaternion targetRotation, float duration, System.Action onComplete)
    {
        Quaternion startRotation = transform.rotation;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;

        onComplete?.Invoke();
    }

    private IEnumerator ProjectionSize(float size, float duration, System.Action onComplete)
    {
        float startSize = Camera.main.orthographicSize;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Camera.main.orthographicSize = Mathf.Lerp(startSize, size, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Camera.main.orthographicSize = size;

        onComplete?.Invoke();
    }

}
