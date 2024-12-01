using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    
    // Tambahkan [SerializeField] agar bisa diatur di Inspector
    [SerializeField] private float animationSpeed = 3f;
    [SerializeField] private float resetThreshold = 1f;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Vector2 offset = meshRenderer.material.mainTextureOffset;
        offset.x += animationSpeed * Time.deltaTime;

        if (offset.x >= resetThreshold)
        {
            offset.x = 0f;
        }

        meshRenderer.material.mainTextureOffset = offset;
    }
}