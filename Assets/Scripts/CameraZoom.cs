using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 10f;           // Kecepatan zoom
    public float smoothSpeed = 5f;          // Kecepatan perubahan zoom agar halus
    public float[] zoomLevels = { 7f, 9f, 10.5f, 11.5f }; // Level zoom in/out
    public Vector3 defaultPosition = new Vector3(0f, 0f, -10f); // Posisi kamera default

    private Camera cam;
    private int currentZoomIndex;           // Indeks zoom level saat ini
    private float targetZoom;               // Target zoom yang diinginkan
    private Vector3 targetPosition;         // Posisi kamera selama zoom
    private Vector3 originalPosition;       // Posisi awal kamera sebelum zoom

    void Start()
    {
        cam = Camera.main;
        currentZoomIndex = 1;               // Mulai dari zoom level ke-2 (9f)
        targetZoom = zoomLevels[currentZoomIndex];
        cam.orthographicSize = targetZoom;

        originalPosition = cam.transform.position; // Simpan posisi awal kamera
        targetPosition = originalPosition;
    }

    void Update()
    {
        HandleZoom();
        SmoothZoom();
        ReturnToDefaultPosition();
    }

    void HandleZoom()
    {
        // Input dari scroll wheel mouse
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0)
        {
            // Zoom in hanya jika tidak di level 7f
            if (scrollInput > 0 && currentZoomIndex > 0)
            {
                currentZoomIndex--;
            }
            // Zoom out hanya jika tidak di level 11.5f
            else if (scrollInput < 0 && currentZoomIndex < zoomLevels.Length - 1)
            {
                currentZoomIndex++;
            }

            // Set target zoom berdasarkan zoom level yang dipilih
            targetZoom = zoomLevels[currentZoomIndex];

            // Hitung posisi kursor dalam koordinat dunia (2D)
            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mouseWorldPos - cam.transform.position;

            // Tentukan posisi target kamera dengan pergerakan ke arah kursor
            targetPosition = cam.transform.position + direction * scrollInput * zoomSpeed;
            targetPosition.z = -10f; // Pastikan kamera tetap berada di sumbu Z yang sama untuk 2D
        }
        // Jika sudah pada level 7f, kunci zoom in
        if (currentZoomIndex == 0)
        {
            targetPosition = cam.transform.position; // Kunci posisi saat ini
        }
    }

    void SmoothZoom()
    {
        // Lerp untuk perubahan zoom yang lebih halus
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * smoothSpeed);

        // Lerp posisi kamera ke posisi target selama zoom
        cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, Time.deltaTime * smoothSpeed);
    }

    void ReturnToDefaultPosition()
    {
        // Kembalikan ke posisi awal hanya jika level zoom berada pada 10.5f atau 11.5f
        if (Input.GetAxis("Mouse ScrollWheel") == 0 && (currentZoomIndex >= 4))
        {
            targetPosition = originalPosition; // Set target ke posisi awal
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, Time.deltaTime * smoothSpeed);
        }
    }
}

