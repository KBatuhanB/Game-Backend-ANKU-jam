using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlScrool : MonoBehaviour
{
    public float zoomSpeed = 5f;
    public float minOrthoSize = 3f;
    public float maxOrthoSize = 10f;
    public float zoomLerpSpeed = 10f;

    private Camera cam;
    private float defaultOrthoSize;
    private Vector3 defaultPosition;
    private bool isZoomed = false;

    void Start()
    {
        cam = GetComponent<Camera>();
        defaultOrthoSize = cam.orthographicSize;
        defaultPosition = cam.transform.position;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > 0.01f)
        {
            // Zoom in
            if (scroll > 0f)
            {
                isZoomed = true;
                // Mouse'un world pozisyonunu bul
                Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
                // Kamerayı mouse'a doğru yaklaştır
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, minOrthoSize, Time.deltaTime * zoomSpeed);
                cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(mouseWorldPos.x, mouseWorldPos.y, defaultPosition.z), Time.deltaTime * zoomSpeed);
            }
            // Zoom out
            else if (scroll < 0f)
            {
                isZoomed = false;
            }
        }

        // Eğer zoom-out yapıldıysa veya scroll bırakıldıysa kamerayı eski haline döndür
        if (!isZoomed)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, defaultOrthoSize, Time.deltaTime * zoomLerpSpeed);
            cam.transform.position = Vector3.Lerp(cam.transform.position, defaultPosition, Time.deltaTime * zoomLerpSpeed);
        }
    }
}
