using UnityEngine;
using UnityEngine.UI;

public class PlaceObjectButton : MonoBehaviour
{
    public int maxClickCount = 1; // Inspector'dan ayarla
    private int currentClickCount = 0;
    public Button button; // Inspector'dan ata

    public void OnButtonClick()
    {
        currentClickCount++;
        if (currentClickCount >= maxClickCount)
        {
            button.interactable = false;
        }
    }

    public void ResetButton()
    {
        currentClickCount = 0;
        button.interactable = true;
    }
}