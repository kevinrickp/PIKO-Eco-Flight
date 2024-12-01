using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player References")]
    private PlayerController playerController;

    [Header("Score Tracking")]
    private int score = 0;
    private int scoreGas = 0;
    private int scoreEco = 0;

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTextGas;
    public TextMeshProUGUI scoreTextEco;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverScreen;
    public GameObject PlayUI;
    public GameObject GameOverUI;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource gameOverSoundSource;
    [SerializeField] private AudioClip backgroundMusicClip;
    [SerializeField] private AudioClip gameOverSoundClip;

    [Header("High Score")]
    private int highScore = 0;
    private const string HIGH_SCORE_KEY = "HighScore";

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        playerController = FindObjectOfType<PlayerController>();
        LoadHighScore();
    }

    void Start()
    {
        SetupAudio();
        Pause();
        UpdateHighScoreDisplay();
    }

    private void SetupAudio()
    {
        if (backgroundMusicSource != null && backgroundMusicClip != null)
        {
            backgroundMusicSource.clip = backgroundMusicClip;
            backgroundMusicSource.loop = true;
        }
    }

    public void GameOver()
    {
        // Stop background music
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.Stop();
        }

        // Play game over sound
        if (gameOverSoundSource != null && gameOverSoundClip != null)
        {
            gameOverSoundSource.PlayOneShot(gameOverSoundClip);
        }

        // Update High Score
        UpdateHighScore();

        gameOverScreen.SetActive(true);
        GameOverUI.SetActive(true);
        Pause();
    }

    private void UpdateHighScore()
    {
        int totalScore = score + scoreGas + scoreEco;
        if (totalScore > highScore)
        {
            highScore = totalScore;
            SaveHighScore();
            UpdateHighScoreDisplay();
        }
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt(HIGH_SCORE_KEY, highScore);
        PlayerPrefs.Save();
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
    }

    private void UpdateHighScoreDisplay()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        playerController.enabled = false;
    }

    public void Play()
    {
        // Reset game state
        ResetScores();

        // Hide UI elements
        PlayUI.SetActive(false);
        gameOverScreen.SetActive(false);

        // Resume game
        Time.timeScale = 1;
        playerController.enabled = true;

        // Play background music
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.Play();
        }

        // Clear all existing obstacles
        ClearObstacles();
    }

    private void ResetScores()
    {
        score = 0;
        scoreGas = 0;
        scoreEco = 0;
        UpdateScoreText();
        UpdateScoreTextGas();
        UpdateScoreTextEco();
    }

    private void ClearObstacles()
    {
        // Remove all existing obstacles
        Es[] esObstacles = FindObjectsOfType<Es>();
        Gas[] gasObstacles = FindObjectsOfType<Gas>();
        Eco[] ecoObstacles = FindObjectsOfType<Eco>();

        foreach (Es obstacle in esObstacles)
            Destroy(obstacle.gameObject);

        foreach (Gas obstacle in gasObstacles)
            Destroy(obstacle.gameObject);

        foreach (Eco obstacle in ecoObstacles)
            Destroy(obstacle.gameObject);
    }

    public void AddScore(string obstacleType = "default")
    {
        score++;
        Debug.Log($"Score increased: {score} (Type: {obstacleType})");
        UpdateScoreText();
    }

    public void AddScoreGas(string obstacleType = "default")
    {
        scoreGas++;
        Debug.Log($"Gas Score increased: {scoreGas} (Type: {obstacleType})");
        UpdateScoreTextGas();
    }

    public void AddScoreEco(string obstacleType = "default")
    {
        scoreEco++;
        Debug.Log($"Eco Score increased: {scoreEco} (Type: {obstacleType})");
        UpdateScoreTextEco();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    private void UpdateScoreTextGas()
    {
        if (scoreTextGas != null)
            scoreTextGas.text = scoreGas.ToString();
    }

    private void UpdateScoreTextEco()
    {
        if (scoreTextEco != null)
            scoreTextEco.text = scoreEco.ToString();
    }
}