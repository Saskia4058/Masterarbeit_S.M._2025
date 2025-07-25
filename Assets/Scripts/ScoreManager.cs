// Urheber Soundeffekt (Fail):
// Sound Effect by u_8g40a9z0la from Pixabay unter https://pixabay.com/de/sound-effects/fail-234710/

// Urheber Soundeffekt (Win):
// Sound Effect by freesound_community from Pixabay unter https://pixabay.com/de/sound-effects/success-bell-6776/

using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public GameObject speechBubbleTrue;
    public GameObject speechBubbleFalse;
    public GameObject speechBubbleFalse2;     

    public TextMeshProUGUI scoreText;
    public AudioSource pointSound;
    public AudioClip falseSoundClip;

    private int score = 0;
    private bool hasScored = false;
    private bool hasPlayedFalse1 = false;
    private bool hasPlayedFalse2 = false;

    void Start()
    {
        ResetScoreOnRestartOrQuit();
        LoadScore();
        UpdateScoreText();
    }

    void Update()
    {
    
        if (speechBubbleTrue.activeSelf && !hasScored)
        {
            AddPoint();
            hasScored = true;
            PlayPointSound();
        }
        if (!speechBubbleTrue.activeSelf)
        {
            hasScored = false;
        }

        if (speechBubbleFalse.activeSelf && !hasPlayedFalse1)
        {
            PlayFalseSound();
            hasPlayedFalse1 = true;
        }
        if (!speechBubbleFalse.activeSelf)
        {
            hasPlayedFalse1 = false;
        }

        if (speechBubbleFalse2 != null)
        {
            if (speechBubbleFalse2.activeSelf && !hasPlayedFalse2)
            {
                PlayFalseSound();
                hasPlayedFalse2 = true;
            }
            if (!speechBubbleFalse2.activeSelf)
            {
                hasPlayedFalse2 = false;
            }
        }
    }

    void AddPoint()
    {
        score++;
        SaveScore();
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Punkte: " + score;
    }

    void SaveScore()
    {
        PlayerPrefs.SetInt("CurrentScore", score);
        PlayerPrefs.Save();
    }

    void LoadScore()
    {
        score = PlayerPrefs.GetInt("CurrentScore", 0);
    }

    void ResetScoreOnRestartOrQuit()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
        {
            PlayerPrefs.SetInt("CurrentScore", 0);
            PlayerPrefs.Save();
        }

        Application.quitting += () =>
        {
            PlayerPrefs.SetInt("CurrentScore", 0);
            PlayerPrefs.Save();
        };
    }

    void PlayPointSound()
    {
        if (pointSound != null)
        {
            pointSound.Play();
        }
    }

    void PlayFalseSound()
    {
        if (pointSound != null && falseSoundClip != null)
        {
            pointSound.clip = falseSoundClip;
            pointSound.Play();
        }
    }
}