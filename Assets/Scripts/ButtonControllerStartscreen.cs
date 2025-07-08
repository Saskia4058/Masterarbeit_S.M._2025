using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonControllerStartscreen : MonoBehaviour
{
    public Button ButtonPlay;
    public Button ButtonExit;
    public AudioClip clickSound;
    private AudioSource audioSource;

    private void Start()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        // AudioSource vom GameObject holen
        audioSource = GetComponent<AudioSource>();

        // Prüfen, ob die Buttons korrekt zugewiesen sind
        if (ButtonPlay == null || ButtonExit == null)
        {
            Debug.LogError("Ein oder beide Buttons wurden nicht zugewiesen!");
        }
        else
        {
            Debug.Log("Buttons sind korrekt zugewiesen.");
        }

        // Button Event Listener hinzufügen
        ButtonPlay.onClick.AddListener(() => PlayClickSoundAndLoadScene("Tutorial"));
        ButtonExit.onClick.AddListener(() => PlayClickSoundAndExitGame());
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

        // Szene nach kurzer Verzögerung laden, um Sound abzuspielen
        Invoke(nameof(LoadScene), clickSound.length);
    }

    private void PlayClickSoundAndExitGame()
    {
        if (audioSource == null || clickSound == null)
            return;
        
        audioSource.PlayOneShot(clickSound);

        // Spiel nach kurzer Verzögerung beenden, um Sound abzuspielen
        Invoke(nameof(ExitGame), clickSound.length);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("Tutorial");
    }

    private void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}