using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipAndReloadScene : MonoBehaviour
{
    [Header("Geçilecek Sahne Adı")]
    public string nextSceneName; // Inspector'dan ata

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!string.IsNullOrEmpty(nextSceneName))
                SceneManager.LoadScene(nextSceneName);
            else
                Debug.LogWarning("Next scene name is not set in the Inspector!");
        }
    }
}
