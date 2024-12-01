using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject balokEsPrefab;
    public GameObject[] ecoPrefabs;
    public GameObject[] gasPrefabs;
    
    [Tooltip("Jarak minimal antara obstacle")]
    public float minSpawnDistance = 2f;
    
    [Tooltip("Jarak maksimal antara obstacle")]
    public float maxSpawnDistance = 4f;

    [Header("Balok Es Height Settings")]
    [Tooltip("Ketinggian minimal spawn balok es")]
    public float balokEsMinHeight = -2f;
    
    [Tooltip("Ketinggian maksimal spawn balok es")]
    public float balokEsMaxHeight = 2f;

    [Header("Eco Height Settings")]
    [Tooltip("Ketinggian minimal spawn eco")]
    public float ecoMinHeight = -1f;
    
    [Tooltip("Ketinggian maksimal spawn eco")]
    public float ecoMaxHeight = 1f;

    [Header("Gas Height Settings")]
    [Tooltip("Ketinggian minimal spawn gas")]
    public float gasMinHeight = -0.5f;
    
    [Tooltip("Ketinggian maksimal spawn gas")]
    public float gasMaxHeight = 0.5f;

    [Header("Timing Settings")]
    [Tooltip("Waktu jeda antar spawn")]
    public float timeBetweenSpawn = 2f;

    [Header("Spawn Probability")]
    [Tooltip("Probabilitas spawn balok es")]
    [Range(0f, 1f)]
    public float balokEsProbability = 0.6f;

    [Tooltip("Probabilitas spawn eco")]
    [Range(0f, 1f)]
    public float ecoProbability = 0.2f;

    [Tooltip("Probabilitas spawn gas")]
    [Range(0f, 1f)]
    public float gasProbability = 0.2f;

    private Camera mainCamera;
    private float timer;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Tidak ada kamera utama di scene!");
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenSpawn)
        {
            SpawnObstacle();
            timer = 0;
        }
    }

    void SpawnObstacle()
    {
        if (mainCamera == null) return;

        float randomValue = Random.value;
        GameObject spawnPrefab;
        float spawnMinHeight;
        float spawnMaxHeight;

        // Hitung total probabilitas
        float totalProbability = balokEsProbability + ecoProbability + gasProbability;

        // Normalisasi probabilitas
        float normalizedBalokEsProbability = balokEsProbability / totalProbability;
        float normalizedEcoProbability = ecoProbability / totalProbability;
        float normalizedGasProbability = gasProbability / totalProbability;

        if (randomValue < normalizedBalokEsProbability)
        {
            // Spawn balok es
            spawnPrefab = balokEsPrefab;
            spawnMinHeight = balokEsMinHeight;
            spawnMaxHeight = balokEsMaxHeight;
        }
        else if (randomValue < (normalizedBalokEsProbability + normalizedEcoProbability) && ecoPrefabs.Length > 0)
        {
            // Spawn eco
            spawnPrefab = ecoPrefabs[Random.Range(0, ecoPrefabs.Length)];
            spawnMinHeight = ecoMinHeight;
            spawnMaxHeight = ecoMaxHeight;
        }
        else if (gasPrefabs.Length > 0)
        {
            // Spawn gas
            spawnPrefab = gasPrefabs[Random.Range(0, gasPrefabs.Length)];
            spawnMinHeight = gasMinHeight;
            spawnMaxHeight = gasMaxHeight;
        }
        else
        {
            // Fallback ke balok es jika tidak ada prefab lain
            spawnPrefab = balokEsPrefab;
            spawnMinHeight = balokEsMinHeight;
            spawnMaxHeight = balokEsMaxHeight;
        }

        Vector3 spawnPosition = CalculateSpawnPosition(spawnMinHeight, spawnMaxHeight);
        GameObject obstacle = Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);

        Debug.Log($"Spawned {obstacle.name} at: {spawnPosition}");
    }

    Vector3 CalculateSpawnPosition(float minHeight, float maxHeight)
    {
        float cameraRightEdge = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        float spawnX = cameraRightEdge + Random.Range(minSpawnDistance, maxSpawnDistance);
        float spawnY = Random.Range(minHeight, maxHeight);

        return new Vector3(spawnX, spawnY, 0);
    }

    void OnDrawGizmosSelected()
    {
        if (mainCamera != null)
        {
            float cameraRightEdge = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            
            // Balok Es Spawn Area
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(
                new Vector3(cameraRightEdge, balokEsMinHeight, 0),
                new Vector3(cameraRightEdge, balokEsMaxHeight, 0)
            );

            // Eco Spawn Area
            Gizmos.color = Color.green;
            Gizmos.DrawLine(
                new Vector3(cameraRightEdge, ecoMinHeight, 0),
                new Vector3(cameraRightEdge, ecoMaxHeight, 0)
            );

            // Gas Spawn Area
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                new Vector3(cameraRightEdge, gasMinHeight, 0),
                new Vector3(cameraRightEdge, gasMaxHeight, 0)
            );
        }
    }
}