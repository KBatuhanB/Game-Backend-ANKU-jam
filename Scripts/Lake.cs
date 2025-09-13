using UnityEngine;

public class Lake : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<AxolotlManager>().TryCompleteLevelWithLake();
        }
    }
}