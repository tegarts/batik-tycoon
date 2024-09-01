using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private Quaternion rotationA = Quaternion.Euler(44.81f, -47f, 0f);
    private Quaternion rotationB = Quaternion.Euler(0f, -180f, 0f);
    private Coroutine rotationCoroutine;

    private void Start() {
        transform.rotation = rotationA;
    }

    public void RotateToA(float duration)
    {
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
        }
        rotationCoroutine = StartCoroutine(RotateCamera(rotationA, duration));
    }

    public void RotateToB(float duration)
    {
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
        }
        rotationCoroutine = StartCoroutine(RotateCamera(rotationB, duration));
    }

    private IEnumerator RotateCamera(Quaternion targetRotation, float duration)
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
    }
}
