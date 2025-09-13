using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isGameStarted = false;
    public PlayerController playerController;
    public Button[] placeObjectButtons;
    public Button playButton;
    public GameObject confirmPanel; // Inspector'dan ata
    public GameObject objectListPanel; // Inspector'dan ata
    public CameraFollow cameraFollow; // Inspector'dan ata
    public int totalRescuedAxolotls = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            int currentLevel = SceneManager.GetActiveScene().buildIndex;

            // Eğer ilk sahnedeysek (örneğin Level 1 ise), kayıtları sıfırla
            if (currentLevel == 2)
            {
                ResetAllLevelData();
            }
            else
            {
                totalRescuedAxolotls = PlayerPrefs.GetInt("TotalRescuedAxolotls", 0);
            }
        }
        else
        {
            Destroy(gameObject);
        }

        if (playerController != null)
            playerController.enabled = false; // SADECE script'i disable et, objeyi kapatma!
    }
    
    public void ResetAllLevelData()
    {
        totalRescuedAxolotls = 0;

        // Tüm kayıtlı level bilgilerini sil
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string key = $"Level_{i}_RescuedAxolotls";
            PlayerPrefs.DeleteKey(key);
        }

        PlayerPrefs.SetInt("TotalRescuedAxolotls", 0);
        PlayerPrefs.Save();
    }

    public void AddRescuedAxolotls(int amount)
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        string key = $"Level_{currentLevel}_RescuedAxolotls";

        // Daha önce bu levelde kaç aksolotl kurtarıldıysa onu çıkar
        int previousAmount = PlayerPrefs.GetInt(key, 0);
        totalRescuedAxolotls -= previousAmount;

        // Yeni değeri hem toplam sayıya hem PlayerPrefs'e ekle
        totalRescuedAxolotls += amount;
        PlayerPrefs.SetInt(key, amount); // Bu levelde kaç tane kurtarıldı
        PlayerPrefs.SetInt("TotalRescuedAxolotls", totalRescuedAxolotls); // Toplam güncelle
        PlayerPrefs.Save();

        Debug.Log($"[Level {currentLevel}] Kurtarılan aksolotlar güncellendi. Yeni toplam: {totalRescuedAxolotls}");
    }

    public void StartGame()
    {
        var panels = FindObjectsOfType<AssignTimePanel>(true);
        bool hasUnconfirmed = false;

        foreach (var panel in panels)
        {
            if (panel.gameObject.activeSelf && panel.inputPanel.activeSelf)
            {
                hasUnconfirmed = true;
                break;
            }
        }

        if (hasUnconfirmed)
        {
            if (confirmPanel != null)
                confirmPanel.SetActive(true);
            return;
        }

        foreach (var panel in panels)
        {
            float time = 0;
            if (float.TryParse(panel.inputField.text, out time) && time > 0)
            {
                panel.StartTimerPanel(time);
            }
            else
            {
                panel.inputField.text = "0";
                panel.OnConfirm();
                panel.StartTimerPanel(0);
            }
        }

        isGameStarted = true;

        if (playerController != null)
            playerController.enabled = true; // Sadece script'i aç

        foreach (var btn in placeObjectButtons)
        {
            btn.interactable = false;
        }

        if (playButton != null)
            playButton.gameObject.SetActive(false);

        if (objectListPanel != null)
            objectListPanel.SetActive(false);

        if (Timer.Instance != null)
            Timer.Instance.StartTotalTimer();

        if (cameraFollow != null)
        {
            cameraFollow.ResetCameraSize();
            cameraFollow.ActivateFollowAndResize();
        }

        CameraControlScrool scrollScript = Camera.main.GetComponent<CameraControlScrool>();
        if (scrollScript != null)
            scrollScript.enabled = false;
    }

    public void ConfirmUnconfirmedPanels()
    {
        var panels = FindObjectsOfType<AssignTimePanel>(true);
        foreach (var panel in panels)
        {
            if (panel.gameObject.activeSelf && panel.inputPanel.activeSelf)
            {
                panel.inputField.text = "0";
                panel.OnConfirm();
                panel.StartTimerPanel(0);
            }
        }

        if (confirmPanel != null)
            confirmPanel.SetActive(false);

        StartGame();
    }

    public void CancelStartGame()
    {
        if (confirmPanel != null)
            confirmPanel.SetActive(false);
    }
}
