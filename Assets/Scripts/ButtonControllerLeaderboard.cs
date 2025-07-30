// Urheber Soundeffekt (Klickgeräusch):
// Sound Effect by <a href="https://pixabay.com/de/users/u_8g40a9z0la-45586904/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=234708">u_8g40a9z0la</a> from <a href="https://pixabay.com//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=234708">Pixabay</a> 

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonControllerLeaderboard : MonoBehaviour
{
    public Button ButtonWeiter;
    public AudioClip clickSound;
    private AudioSource audioSource;
    private string pendingSceneName;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        ButtonWeiter.onClick.AddListener(() => ResetScoreAndLoadScene("Library"));
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

        pendingSceneName = sceneName;
        audioSource.PlayOneShot(clickSound);
        Debug.Log("Sound wird abgespielt.");

        Invoke(nameof(LoadPendingScene), clickSound.length);
    }

    private void PlayClickSoundAndExitGame()
    {
        if (audioSource == null || clickSound == null)
            return;

        audioSource.PlayOneShot(clickSound);
        Debug.Log("Sound wird abgespielt.");

        Invoke(nameof(ExitGame), clickSound.length);
    }
    private void ResetScoreAndLoadScene(string sceneName)
    {
        PlayerPrefs.SetInt("CurrentScore", 0);
        PlayerPrefs.Save();
        Debug.Log("Punktestand wurde durch ButtonReplay zurückgesetzt.");

        PlayClickSoundAndLoadScene(sceneName);
    }

    private void LoadPendingScene()
    {
        SceneManager.LoadScene(pendingSceneName);
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
