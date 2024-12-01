using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    [Header("Scrolling Settings")]
    public float scrollSpeed = 5f;
    public float resetPosition = -18f;
    public float startPosition = 18f;

    private Vector3 startPos;

    void Start()
    {
        // Simpan posisi awal background
        startPos = transform.position;
    }

    void Update()
    {
        // Gerakkan background ke kiri
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // Cek untuk reset posisi
        if (transform.position.x <= resetPosition)
        {
            // Reset posisi ke posisi awal
            Vector3 resetPos = new Vector3(startPosition, startPos.y, startPos.z);
            transform.position = resetPos;
        }
    }
}