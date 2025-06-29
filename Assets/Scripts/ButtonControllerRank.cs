using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonControllerRank : MonoBehaviour
{
    public Button ButtonWeiter;
    public AudioClip clickSound;
    private AudioSource audioSource;

    // 🔹 NEU: Referenz auf HighscoreManager
    public HighscoreManager highscoreManager;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (ButtonWeiter == null)
        {
            Debug.LogError("ButtonWeiter wurde nicht zugewiesen!");
        }
        else
        {
            Debug.Log("ButtonWeiter ist korrekt zugewiesen.");
        }

        // 🔹 ÄNDERN: Jetzt eigene Methode aufrufen
        ButtonWeiter.onClick.AddListener(OnButtonWeiterClicked);
    }

    // 🔹 NEU: Gemeinsame Steuerung
    private void OnButtonWeiterClicked()
    {
        // 🔸 1. Highscore speichern
        if (highscoreManager != null)
        {
            highscoreManager.SaveCurrentScoreWithName();
        }
        else
        {
            Debug.LogWarning("HighscoreManager ist nicht zugewiesen!");
        }

        // 🔸 2. Sound abspielen und danach Szene laden
        PlayClickSoundAndLoadScene("Leaderboard");
    }

    private void PlayClickSound()
    {
        Debug.Log("Button wurde geklickt.");

        if (audioSource == null)
        {
            Debug.LogError("AudioSource ist NULL!");
        }
        else if (clickSound == null)
        {
            Debug.LogError("clickSound ist NULL!");
        }
        else
        {
            Debug.Log($"Sound wird abgespielt. Volume: {audioSource.volume}");
            audioSource.PlayOneShot(clickSound);
        }
    }

    private void PlayClickSoundAndLoadScene(string sceneName)
    {
        if (audioSource == null || clickSound == null)
            return;

        audioSource.PlayOneShot(clickSound);
        Debug.Log("Sound wird abgespielt.");

        Invoke(nameof(LoadScene), clickSound.length);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("Leaderboard");
    }
}
