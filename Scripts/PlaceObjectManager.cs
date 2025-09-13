using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlaceObjectManager : MonoBehaviour
{
    [Header("Buton Ayarları")]
    public Button myButton; // Inspector'dan ata (her buton için farklı)
    public int maxCount = 1; // Inspector'dan ata (her buton için farklı)
    private int currentCount;

    [Header("Prefab Ayarları")]
    public GameObject prefabToPlace; // Inspector'dan ata (her buton için farklı)

    private GameObject currentGhostObject;

    [Header("Ses Ayarları")]
    public AudioClip placeSound; // Inspector'dan ata
    private AudioSource audioSource;

    void Start()
    {
        currentCount = maxCount;
        if (myButton != null)
            myButton.interactable = currentCount > 0;

        // AudioSource ekle veya bul
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void OnButtonClick()
    {
        if (currentCount <= 0) return;

        // Hayalet obje oluştur
        if (currentGhostObject != null)
            Destroy(currentGhostObject);

        if (prefabToPlace != null)
        {
            currentGhostObject = Instantiate(prefabToPlace);
            SetGhostMode(currentGhostObject, true);
        }

        // Counter azalt ve butonu kontrol et
        currentCount--;
        if (myButton != null && currentCount <= 0)
            myButton.interactable = false;
    }

    void Update()
    {
        if (currentGhostObject != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            currentGhostObject.transform.position = mousePosition;

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                PlaceObject();
            }
        }
    }

    private void PlaceObject()
    {
        SetGhostMode(currentGhostObject, false);
        ShowAssignTimePanel(currentGhostObject);

        // SES ÇAL
        if (placeSound != null && audioSource != null)
            audioSource.PlayOneShot(placeSound);

        currentGhostObject = null;
    }

    private void ShowAssignTimePanel(GameObject obj)
    {
        AssignTimePanel panel = obj.GetComponentInChildren<AssignTimePanel>(true);
        if (panel != null)
            panel.Open(obj);
    }

    private void SetGhostMode(GameObject obj, bool isGhost)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color color = sr.color;
            color.a = isGhost ? 0.5f : 1f;
            sr.color = color;
        }

        Collider2D col = obj.GetComponent<Collider2D>();
        if (col != null)
            col.enabled = !isGhost;
    }
}

