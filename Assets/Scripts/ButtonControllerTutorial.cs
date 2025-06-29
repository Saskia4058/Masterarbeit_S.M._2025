using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class ButtonControllerTutorial : MonoBehaviour
{
    public Button ButtonWeiter;
    public AudioClip clickSound;
    public VideoPlayer videoPlayer;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        ButtonWeiter.gameObject.SetActive(false);
        videoPlayer.loopPointReached += OnVideoFinished;

        ButtonWeiter.onClick.AddListener(() => PlayClickSoundAndLoadScene());
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        ButtonWeiter.gameObject.SetActive(true);
    }

    private void PlayClickSoundAndLoadScene()
    {
        audioSource.PlayOneShot(clickSound);
        Invoke(nameof(LoadScene), clickSound.length);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("Level 1");
    }
}