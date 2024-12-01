using UnityEngine;  // Untuk MonoBehaviour
using UnityEngine.Audio; // Untuk AudioClip dan AudioSource
using UnityEngine.Serialization; // Untuk HeaderAttribute

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Scoring Sounds")]
    public AudioClip gasScoreClip;
    public AudioClip ecoScoreClip;
    public AudioSource scoreSoundSource;

    void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayGasScoreSound()
    {
        if (scoreSoundSource != null && gasScoreClip != null)
        {
            scoreSoundSource.PlayOneShot(gasScoreClip);
        }
    }

    public void PlayEcoScoreSound()
    {
        if (scoreSoundSource != null && ecoScoreClip != null)
        {
            scoreSoundSource.PlayOneShot(ecoScoreClip);
        }
    }
}