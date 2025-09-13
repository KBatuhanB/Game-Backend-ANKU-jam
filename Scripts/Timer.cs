using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Timer : MonoBehaviour
{
    public float totalTime = 120f;
    public float maxTime = 120f; // Başlangıç değeri
    public static Timer Instance;

    public Slider totalTimeBar; // Inspector'dan ata
    public TextMeshProUGUI totalTimeText; // Inspector'dan ata
    public TextMeshProUGUI countdownText; // Inspector'dan ata

    private Coroutine timerCoroutine;
    public LevelManager levelManager;

    private void Awake()
    {
        Instance = this;
        maxTime = totalTime;
    }

    private void Update()
    {
        if (totalTimeBar != null)
            totalTimeBar.value = totalTime / maxTime;

        if (totalTimeText != null)
            totalTimeText.text = Mathf.CeilToInt(totalTime).ToString();
    }

    public bool TryAssignTime(float time)
    {
        if (totalTime >= time)
        {
            totalTime -= time;
            return true;
        }
        return false;
    }

    public void StartTotalTimer()
    {
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);
        timerCoroutine = StartCoroutine(TotalTimerRoutine());
    }

    public void StopTimerAndFreeze()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
        // totalTime olduğu gibi kalır, UI güncellenmeye devam eder

        // Son 5 saniye (LastSeconds) textini gizle
        if (countdownText != null)
            countdownText.gameObject.SetActive(false);
    }

    private IEnumerator TotalTimerRoutine()
    {
        if (countdownText != null)
            countdownText.gameObject.SetActive(false); // Başta gizle

        while (totalTime > 0)
        {
            if (countdownText != null)
            {
                if (totalTime <= 5)
                {
                    countdownText.gameObject.SetActive(true); // Son 5 saniye göster
                    countdownText.text = Mathf.CeilToInt(totalTime).ToString();
                }
                else
                {
                    countdownText.gameObject.SetActive(false); // Diğer zamanlarda gizle
                }
            }

            yield return new WaitForSeconds(1f);
            totalTime -= 1f;
            if (totalTime < 0) totalTime = 0;
        }

        // Süre bittiğinde 0'ı göster
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
            countdownText.text = "0";
            yield return new WaitForSeconds(1f); // 1 saniye 0 yazısını göster
            countdownText.gameObject.SetActive(false); // Sonra gizle
        }

        // Süre bitti, sahneyi yeniden başlat
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        levelManager.ShowPanel();
    }
}
