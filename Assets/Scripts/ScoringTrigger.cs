using UnityEngine;

public class ScoringTrigger : MonoBehaviour
{
    private bool hasScored = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Pastikan hanya Player yang bisa menambah score
        if (other.CompareTag("Player") && !hasScored)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore("BalokEs");
                Debug.Log("Score Added: BalokEs");
                hasScored = true;
            }
            else
            {
                Debug.LogError("GameManager instance tidak ditemukan!");
            }
        }
    }
}