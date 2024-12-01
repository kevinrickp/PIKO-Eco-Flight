using UnityEngine;

public class ScoringTriggerEco : MonoBehaviour
{
    private bool hasScored = false;

    [Header("Scoring Settings")]
    public float triggerDelay = 0.5f;
    public float triggerOffset = 0.5f;

    // Referensi ke parent Eco
    public GameObject ecoParent;

    private float spawnTime;

    void Start()
    {
        spawnTime = Time.time;

        // Jika tidak di-set manual, cari parent
        if (ecoParent == null)
        {
            ecoParent = transform.root.gameObject;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Pastikan hanya Player yang bisa menambah score
        if (other == null) return;

        bool isValidTrigger = 
            other.CompareTag("Player") &&  // Tag Player
            !hasScored &&                  // Belum pernah dikumpulkan
            (Time.time - spawnTime > triggerDelay) && // Melewati delay
            (Mathf.Abs(transform.position.x - other.transform.position.x) > triggerOffset); // Offset jarak

        if (isValidTrigger)
        {
            // Validasi GameManager
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScoreEco("Eco");
                Debug.Log("Score Added: Eco");
                hasScored = true;

                // Hapus parent Eco (seluruh prefab)
                if (ecoParent != null)
                {
                    Destroy(ecoParent);
                }
            }
            else
            {
                Debug.LogError("GameManager instance tidak ditemukan!");
            }
        }
    }
}