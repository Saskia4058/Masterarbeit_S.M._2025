// Urheber Soundeffekt (Klickgeräusch):
// Sound Effect by <a href="https://pixabay.com/de/users/u_8g40a9z0la-45586904/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=234708">u_8g40a9z0la</a> from <a href="https://pixabay.com//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=234708">Pixabay</a> 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(AudioSource))]
public class ButtonHomeFunction : MonoBehaviour
{
    private AudioSource audioSource;
    private Button button;

    [SerializeField] private GameObject panelHome;

    [SerializeField] private Button buttonGoOn;
    [SerializeField] private Button buttonFin;

    [SerializeField] private GameObject buttonsParent; // GameObject "Buttons" im Inspector zuweisen

    private bool isGamePaused = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        button = GetComponent<Button>();

        button.onClick.AddListener(OnButtonClick);

        if (buttonGoOn != null)
        {
            buttonGoOn.onClick.AddListener(OnButtonGoOnClick);
        }
        else
        {
            Debug.LogWarning("ButtonGoOn ist nicht zugewiesen.");
        }

        if (buttonFin != null)
        {
            buttonFin.onClick.AddListener(OnButtonFinClick);
        }
        else
        {
            Debug.LogWarning("ButtonFin ist nicht zugewiesen.");
        }
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
            PauseGameDelayed();
        }
        else
        {
            Debug.LogWarning("PanelHome ist im Inspector nicht zugewiesen.");
        }
    }

    void PauseGameDelayed()
    {
        StartCoroutine(PauseAfterDelay(0.2f));
    }

    private IEnumerator PauseAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        PauseGame();
    }

    void PauseGame()
    {
        if (!isGamePaused)
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
            isGamePaused = true;

            SetButtonsInteractable(false); // Alle Buttons im GameObject "Buttons" deaktivieren
        }
    }

    void ResumeGame()
    {
        if (isGamePaused)
        {
            Time.timeScale = 1f;
            AudioListener.pause = false;
            isGamePaused = false;

            SetButtonsInteractable(true);  // Alle Buttons im GameObject "Buttons" wieder aktivieren
        }
    }

    void OnButtonGoOnClick()
    {
        if (panelHome != null)
        {
            panelHome.SetActive(false);
            ResumeGame();
        }
    }

    void OnButtonFinClick()
    {
        ResumeGame();
        SceneManager.LoadScene("Startscreen");
    }

    private void SetButtonsInteractable(bool interactable)
    {
        if (buttonsParent == null)
        {
            Debug.LogWarning("buttonsParent (GameObject 'Buttons') ist nicht zugewiesen.");
            return;
        }

        // Alle Button-Komponenten im Child-Hierarchiebaum deaktivieren/aktivieren
        Button[] allButtons = buttonsParent.GetComponentsInChildren<Button>(includeInactive: true);
        foreach (var btn in allButtons)
        {
            btn.interactable = interactable;
        }
    }
}