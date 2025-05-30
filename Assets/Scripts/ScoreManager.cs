using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public GameObject speechBubbleTrue;
    public TextMeshProUGUI scoreText;
    public AudioSource pointSound;  // Neue Variable für den Sound
    private int score = 0;
    private bool hasScored = false;

    void Start()
    {
        ResetScoreOnRestartOrQuit();  // Korrektes Reset-Verhalten hinzufügen
        LoadScore();
        UpdateScoreText();
    }

    void Update()
    {
        // Punkte nur hinzufügen, wenn das Objekt aktiv ist und noch nicht gewertet wurde
        if (speechBubbleTrue.activeSelf && !hasScored)
        {
            AddPoint();
            hasScored = true;
            PlayPointSound();  // Neuer Sound-Aufruf
        }

        // Setze hasScored zurück, wenn die Sprechblase deaktiviert wurde
        if (!speechBubbleTrue.activeSelf)
        {
            hasScored = false;
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

    // Neue Funktion: Punktestand beim Neustart oder Spielabbruch zurücksetzen
    void ResetScoreOnRestartOrQuit()
    {
        // Punktestand zurücksetzen, wenn die erste Szene (Startscreen) geladen wird -> relevant für Entwicklungsphase
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
        {
            PlayerPrefs.SetInt("CurrentScore", 0);
            PlayerPrefs.Save();
        }

        // Punktestand zurücksetzen, wenn das Spiel beendet wird
        Application.quitting += () =>
        {
            PlayerPrefs.SetInt("CurrentScore", 0);
            PlayerPrefs.Save();
        };
    }

    // Neue Funktion: Sound abspielen
    void PlayPointSound()
    {
        if (pointSound != null)
        {
            pointSound.Play();
        }
    }
}
