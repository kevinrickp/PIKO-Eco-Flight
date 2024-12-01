using UnityEngine;

public class Gas : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    
    [Header("Trigger Settings")]
    public float triggerDelay = 0.5f;
    public float triggerOffset = 0.5f;

    private float leftScreen;
    private float spawnTime;

    void Start()
    {
        // Validasi Main Camera
        if (Camera.main == null)
        {
            Debug.LogError("Main Camera tidak ditemukan!");
            return;
        }

        // Hitung batas layar kiri
        leftScreen = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
        
        // Catat waktu spawn
        spawnTime = Time.time;

        // Debug log posisi spawn
        Debug.Log($"Gas Spawn Position: {transform.position}");
        Debug.Log($"Left Screen Boundary: {leftScreen}");
    }

    void Update()
    {
        // Bergerak ke kiri
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // Hapus objek jika di luar layar
        if (transform.position.x < leftScreen)
        {
            Destroy(gameObject);
        }
    }

    // Hapus OnTriggerEnter2D method
}