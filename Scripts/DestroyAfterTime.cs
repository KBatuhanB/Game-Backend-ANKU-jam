using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float timeLeft = 10f;

    public void SetTime(float t)
    {
        timeLeft = t;
    }

    void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.isGameStarted)
            return;

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f)
        {
            Destroy(gameObject);
        }
    }
}