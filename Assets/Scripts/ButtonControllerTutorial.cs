// Urheber Soundeffekt (Klickgeräusch):
// Sound Effect by <a href="https://pixabay.com/de/users/u_8g40a9z0la-45586904/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=234708">u_8g40a9z0la</a> from <a href="https://pixabay.com//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=234708">Pixabay</a> 

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