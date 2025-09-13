using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private Transform cam;       // Reference to the main camera's transform
    private Vector3 lastCamPos;  // Store the camera's position in the previous frame

    [SerializeField]
    private Vector2 parallaxMultiplier; // X and Y parallax speed multipliers

    private float textureUnitSizeX;

    void Start()
    {
        cam = Camera.main.transform;
        lastCamPos = cam.position;

        if (TryGetComponent(out SpriteRenderer sprite))
        {
            textureUnitSizeX = sprite.bounds.size.x;
        }
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = cam.position - lastCamPos;
        transform.position += new Vector3(deltaMovement.x * parallaxMultiplier.x, deltaMovement.y * parallaxMultiplier.y);
        lastCamPos = cam.position;

        // Optional: infinite scrolling background
        if (Mathf.Abs(cam.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPositionX = (cam.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cam.position.x + offsetPositionX, transform.position.y);
        }
    }
}