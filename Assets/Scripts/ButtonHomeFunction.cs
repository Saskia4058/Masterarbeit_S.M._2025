// Urheber Soundeffekt (Klickgeräusch):
// Sound Effect by <a href="https://pixabay.com/de/users/u_8g40a9z0la-45586904/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=234708">u_8g40a9z0la</a> from <a href="https://pixabay.com//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=234708">Pixabay</a> 

using UnityEngine;
using UnityEngine.UI;

public class ButtonHomeFunction : MonoBehaviour
{
    [SerializeField] private GameObject panelHome;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        audioSource.ignoreListenerPause = true;

        if (panelHome != null)
        {
            Transform buttonsPanel = panelHome.transform.Find("ButtonsPanel");

            if (buttonsPanel != null)
            {
                Button buttonGoOn = buttonsPanel.Find("ButtonGoOn")?.GetComponent<Button>();
                Button buttonFin = buttonsPanel.Find("ButtonFin")?.GetComponent<Button>();

                if (buttonGoOn != null)
                    buttonGoOn.onClick.AddListener(PlayClickSound);

                if (buttonFin != null)
                    buttonFin.onClick.AddListener(PlayClickSound);
            }
            else
            {
                Debug.LogWarning("ButtonsPanel nicht gefunden!");
            }
        }
    }

    public void OnButtonHomeClick()
    {
        PlayClickSound();
        Invoke(nameof(PauseAndShow), 0.05f);
    }

    public void OnButtonGoOnClick()
    {
        ResumeGame();
    }

    void PlayClickSound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    void PauseAndShow()
    {
        if (panelHome != null)
        {
            panelHome.SetActive(true);
        }

        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;

        if (panelHome != null)
        {
            panelHome.SetActive(false);
        }
    }
}
