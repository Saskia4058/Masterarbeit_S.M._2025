// Urheber Soundeffekt (Klickgeräusch):
// Sound Effect by <a href="https://pixabay.com/de/users/u_8g40a9z0la-45586904/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=234708">u_8g40a9z0la</a> from <a href="https://pixabay.com//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=234708">Pixabay</a> 

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonHomeController : MonoBehaviour
{
    private bool isGamePaused = false;
    private bool isPausing = false;
    public AudioSource audioSource;
  
    // Update is called once per frame
    void Update()
    {
        if (isGamePaused && !isPausing)
        {
            isPausing = true;
            StartCoroutine(PauseGame());
        }
        if (!isGamePaused)
        {
            Time.timeScale = 1f;
            AudioListener.pause = false;
        }
    }

    private IEnumerator PauseGame()
    {
        audioSource.Play();

        while (audioSource.isPlaying)
        {
            yield return null;
        }
        AudioListener.pause = true;
        Time.timeScale = 0f;
        isPausing = false;
    }

    private IEnumerator QuitGame()
    {
        audioSource.Play();

        while (audioSource.isPlaying)
        {
            yield return null;
        }
        SceneManager.LoadScene("Startscreen");
    }

    public void ToogelGamePaused()
    {
        isGamePaused = !isGamePaused;
    }
    public void QuitToMainMenu()
    {
        AudioListener.pause = false;
        Time.timeScale = 1f;
        StartCoroutine(QuitGame());
    }
}