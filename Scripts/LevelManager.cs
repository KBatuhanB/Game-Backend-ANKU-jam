using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject uiPanel;
    public TextMeshProUGUI axolotlText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI totalAxolotlText; // Inspector'a atamayı unutma
    public Button restartButton;
    public Button nextLevelButton;
    public GameObject player;

    private AxolotlManager axolotlManager;

    void Start()
    {
        uiPanel.SetActive(false);

        axolotlManager = FindObjectOfType<AxolotlManager>();

        restartButton.onClick.AddListener(RestartLevel);
        nextLevelButton.onClick.AddListener(LoadNextLevel);
    }

    public void ShowPanel()
    {
        uiPanel.SetActive(true);
        
        player.SetActive(false);

        if (axolotlManager != null)
        {
            axolotlText.text = $"Kurtarılan: {axolotlManager.rescuedAxolotls} / {axolotlManager.totalAxolotlsInLevel}";
            nextLevelButton.interactable = axolotlManager.rescuedAxolotls > 0;
        }

        if (GameManager.Instance != null && Timer.Instance != null)
        {
            totalAxolotlText.text = $"Toplam Kurtarılan: {GameManager.Instance.totalRescuedAxolotls}";
            timerText.text = $"Kalan Süre: {Mathf.CeilToInt(Timer.Instance.totalTime)}";
        }
        else
        {
            timerText.text = "Kalan Süre: ?";
        }
        
        
    }

    void RestartLevel()
    {
        axolotlManager.RestartLevel();
    }

    void LoadNextLevel()
    {
        if (axolotlManager.rescuedAxolotls > 0)
        {
            axolotlManager.LoadNextScene();
        }
    }
}