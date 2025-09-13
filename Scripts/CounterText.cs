using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Eğer TextMeshPro kullanıyorsanız: using TMPro;

public class CounterText : MonoBehaviour
{
    public int counter = 5; // Başlangıç değeri Inspector'dan ayarlanabilir
    public TextMeshProUGUI counterText; // UI Text referansı (TextMeshPro için: public TextMeshProUGUI counterText;)

    void Start()
    {
        UpdateCounterText();
    }

    // Bu fonksiyonu butona atayın
    public void DecreaseCounter()
    {
        if (counter > 0)
        {
            counter--;
            UpdateCounterText();
        }
    }

    private void UpdateCounterText()
    {
        counterText.text = counter.ToString();
    }
}
