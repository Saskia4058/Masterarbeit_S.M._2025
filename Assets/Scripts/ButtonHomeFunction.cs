// Urheber Soundeffekt (Klickgeräusch):
// Sound Effect by <a href="https://pixabay.com/de/users/u_8g40a9z0la-45586904/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=234708">u_8g40a9z0la</a> from <a href="https://pixabay.com//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=234708">Pixabay</a> 

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(AudioSource))]
public class ButtonHomeFunction : MonoBehaviour
{
    private AudioSource audioSource;
    private Button button;
    [SerializeField] private GameObject panelHome;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        button = GetComponent<Button>();

        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        PlayClickSound();
        ShowPanelHome();
    }

    void PlayClickSound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource oder AudioClip fehlt auf ButtonHome.");
        }
    }

    void ShowPanelHome()
    {
        if (panelHome != null)
        {
            panelHome.SetActive(true);
        }
        else
        {
            Debug.LogWarning("PanelHome ist im Inspector nicht zugewiesen.");
        }
    }
}
