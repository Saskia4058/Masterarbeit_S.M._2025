// Urheber Soundeffekt (Klickgeräusch):
// Sound Effect by <a href="https://pixabay.com/de/users/u_8g40a9z0la-45586904/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=234708">u_8g40a9z0la</a> from <a href="https://pixabay.com//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=234708">Pixabay</a> 

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonControllerRank : MonoBehaviour
{
    public Button ButtonWeiter;
    public AudioClip clickSound;
    private AudioSource audioSource;

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

        ButtonWeiter.onClick.AddListener(OnButtonWeiterClicked);
    }

    private void OnButtonWeiterClicked()
    {

        if (highscoreManager != null)
        {
            highscoreManager.SaveCurrentScoreWithName();
        }
        else
        {
            Debug.LogWarning("HighscoreManager ist nicht zugewiesen!");
        }

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
