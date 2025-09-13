using UnityEngine;
using TMPro;

public class AssignTimePanel : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject inputPanel;   // Panel objesi (InputField ve butonun olduğu)
    public GameObject timerPanel;   // TimerPanel objesi
    public TextMeshProUGUI timerText; // TimerPanel içindeki Text (Inspector'dan ata)
    private GameObject targetObject;
    private float remainingTime = 0f;
    private bool isCounting = false;

    private void Awake()
    {
        gameObject.SetActive(false);
        if (timerPanel != null)
            timerPanel.SetActive(false);
    }

    public void Open(GameObject obj)
    {
        targetObject = obj;
        inputField.text = "";
        gameObject.SetActive(true);
        if (inputPanel != null)
            inputPanel.SetActive(true);    // input paneli aç
        if (timerPanel != null)
            timerPanel.SetActive(false);   // timer paneli kapat
        isCounting = false;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OnConfirm()
    {
        if (inputField == null || targetObject == null || Timer.Instance == null)
            return;

        float time;
        if (float.TryParse(inputField.text, out time) && Timer.Instance.TryAssignTime(time))
        {
            var destroyScript = targetObject.AddComponent<DestroyAfterTime>();
            destroyScript.SetTime(time);
            if (inputPanel != null)
                inputPanel.SetActive(false); // Sadece input paneli kapat
            // StartTimerPanel(time); // BUNU SİL!
        }
        else
        {
            inputField.text = "Hatalı!";
        }
    }

    // TimerPanel'i başlat
    public void StartTimerPanel(float time)
    {
        if (timerPanel != null && timerText != null)
        {
            timerPanel.SetActive(true);
            remainingTime = time;
            timerText.text = Mathf.CeilToInt(remainingTime).ToString();
            isCounting = true;
        }
    }

    private void Update()
    {
        if (isCounting && timerPanel != null && timerPanel.activeSelf)
        {
            remainingTime -= Time.deltaTime;
            timerText.text = Mathf.CeilToInt(Mathf.Max(remainingTime, 0)).ToString();

            if (remainingTime <= 0f)
            {
                isCounting = false;
                timerText.text = "0";
                timerPanel.SetActive(false); // Süre bitince paneli kapatabilirsin
            }
        }
    }
}