using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek obje (Player)
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float targetOrthoSize = 5f; // Inspector'dan atanacak yeni boyut

    private Camera cam;
    private float initialOrthoSize;
    private bool canFollow = false;

    void Start()
    {
        cam = GetComponent<Camera>();
        initialOrthoSize = cam.orthographicSize; // Başlangıç boyutunu kaydet
    }

    void LateUpdate()
    {
        if (canFollow && target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            float ppu = 100f; // Sprite ayarındaki Pixels Per Unit ile aynı olmalı!
            smoothedPosition.x = Mathf.Round(smoothedPosition.x * ppu) / ppu;
            smoothedPosition.y = Mathf.Round(smoothedPosition.y * ppu) / ppu;

            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        }
    }

    // GameManager'dan çağrılacak
    public void ActivateFollowAndResize()
    {
        canFollow = true;
        if (cam != null)
            cam.orthographicSize = targetOrthoSize; // Yeni boyuta geç
    }

    // StartGame çalışmadan önce kameranın boyutu değişmesin
    public void ResetCameraSize()
    {
        if (cam != null)
            cam.orthographicSize = initialOrthoSize;
    }
}