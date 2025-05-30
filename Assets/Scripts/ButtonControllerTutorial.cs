using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonControllerTutorial : MonoBehaviour
{
    public Button ButtonWeiter;
    public AudioClip clickSound;
    private AudioSource audioSource;

    private void Start()
    {
        // AudioSource vom GameObject holen
        audioSource = GetComponent<AudioSource>();

        // Prüfen, ob der Button korrekt zugewiesen ist
        if (ButtonWeiter == null)
        {
            Debug.LogError("ButtonWeiter wurde nicht zugewiesen!");
        }
        else
        {
            Debug.Log("ButtonWeiter ist korrekt zugewiesen.");
        }

        // Button Event Listener hinzufügen
        ButtonWeiter.onClick.AddListener(() => PlayClickSoundAndLoadScene("Rank"));
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

        // Szene nach kurzer Verzögerung laden, um Sound abzuspielen
        Invoke(nameof(LoadScene), clickSound.length);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("Level 1");
    }
}