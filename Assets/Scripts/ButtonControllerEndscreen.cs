using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonControllerEndscreen : MonoBehaviour
{
    public Button ButtonReplay;
    public Button ButtonExit;
    public AudioClip clickSound;
    private AudioSource audioSource;
    private string pendingSceneName;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        ButtonReplay.onClick.AddListener(() => ResetScoreAndLoadScene("Startscreen"));
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
