using UnityEngine;
using UnityEngine.SceneManagement;

public class AxolotlManager : MonoBehaviour
{
    public int totalAxolotlsInLevel;
    public int rescuedAxolotls = 0;
    private bool levelCompleted = false;
    
    private LevelManager levelManager;

    void Start()
    {
        totalAxolotlsInLevel = GameObject.FindGameObjectsWithTag("Axolotl").Length;
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void RescueAxolotl()
    {
        rescuedAxolotls++;
    
        // Her aksolot kurtarılışında toplam skor güncelleniyor
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddRescuedAxolotls(rescuedAxolotls);
        }
    }

    // Lake ile etkileşimde çağırılacak fonksiyon
    public void TryCompleteLevelWithLake()
    {
        if (levelCompleted)
            return;

        bool isTimeUp = Timer.Instance != null && Timer.Instance.totalTime <= 0;  // Süre bitti mi kontrolü
        bool allAxolotlsRescued = rescuedAxolotls >= totalAxolotlsInLevel;

        if (isTimeUp || allAxolotlsRescued)
        {
            levelCompleted = true;

            if (Timer.Instance != null)
                Timer.Instance.StopTimerAndFreeze();

            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddRescuedAxolotls(rescuedAxolotls);
            }

            ShowResultPanel();
        }
    }

    public void ShowResultPanel()
    {
        if (levelManager != null)
        {
            levelManager.ShowPanel();
        }
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Son sahneydi, ana menüye dönülüyor veya oyun bitiyor.");
            SceneManager.LoadScene(0); // Ana menü ya da oyun sonu sahnesi
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}