using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera Camera;

    private void Start() {
        Camera = FindAnyObjectByType<Camera>();
        transform.forward = Camera.transform.forward;
    }
    private void Update()
    {
        //transform.LookAt(Camera.transform, Vector3.up);
        transform.forward = Camera.transform.forward;
    }
}
