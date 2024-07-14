using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float smoothing = 5f;
    [SerializeField] Vector3 offset = new Vector3(0f, 15f, -22f);
    [SerializeField] GameObject player; // TODO - nanti ganti biar assign pake script biar gak perlu assign manual

    private void FixedUpdate() 
    {
        Vector3 targetCamPos = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
