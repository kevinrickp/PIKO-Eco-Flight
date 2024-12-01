using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float strength = 5f;
    public float gravity = -9.8f;
    public float tilt = 5f;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource jumpSoundSource;
    [SerializeField] private AudioSource collisionSoundSource;
    [SerializeField] private AudioClip jumpSoundClip;
    [SerializeField] private AudioClip collisionSoundClip;

    private Vector3 direction;

    private void OnEnable()
    {
        // Reset player position and direction when enabled
        Vector3 position = transform.position;
        position.y = 0;
        transform.position = position;
        direction = Vector3.zero;
    }

    void Update()
    {
        // Jump/Fly mechanics
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
            
            // Play jump sound
            if (jumpSoundSource != null && jumpSoundClip != null)
            {
                jumpSoundSource.PlayOneShot(jumpSoundClip);
            }
        }

        // Apply gravity
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // Rotate based on vertical movement
        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
    }

    // Tambahkan kembali metode untuk Game Over
        private void OnTriggerEnter2D(Collider2D collision)
        {
        Debug.Log($"Trigger detected with: {collision.gameObject.name}, Tag: {collision.gameObject.tag}");

        if (collision.CompareTag("Ground") || 
            collision.CompareTag("Obstacle") || 
            collision.CompareTag("BalokEs"))
        {
            Debug.Log($"Game Over triggered by: {collision.gameObject.name}");

            // Play collision sound
            if (collisionSoundSource != null && collisionSoundClip != null)
            {
                collisionSoundSource.PlayOneShot(collisionSoundClip);
            }

            // Trigger Game Over
            GameManager.instance.GameOver();
        }
    }
}