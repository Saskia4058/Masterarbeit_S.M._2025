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

    [SerializeField] private GameObject buttonsParent; // "Buttons" GameObject im Inspector zuweisen

    private bool isGamePaused = false;

    // Liste aller Button-Namen, die deaktiviert/aktiviert werden sollen
    private readonly List<string> buttonNamesToToggle = new List<string>
    {
        "ButtonBrA",
        "ButtonEBA",
        "ButtonEFZ",
        "ButtonBM1",
        "ButtonBM2",
        "ButtonWMS",
        "ButtonFMS",
        "ButtonFaMa",
        "ButtonGYM",
        "ButtonPasserelle",
        "ButtonPraxis",
        "ButtonBP",
        "ButtonHFP",
        "ButtonHF",
        "ButtonNDS",
        "ButtonKurs",
        "ButtonUNI",
        "ButtonPH",
        "ButtonFH",
        "ButtonCAS",
        "ButtonPHD",
        "ButtonNext"
    };

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

            SetButtonsInteractable(false); // Buttons deaktivieren
        }
    }

    void ResumeGame()
    {
        if (isGamePaused)
        {
            Time.timeScale = 1f;
            AudioListener.pause = false;
            isGamePaused = false;

            SetButtonsInteractable(true);  // Buttons wieder aktivieren
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
            Debug.LogWarning("ButtonsParent ist nicht zugewiesen.");
            return;
        }

        foreach (var btnName in buttonNamesToToggle)
        {
            Transform btnTransform = buttonsParent.transform.Find(btnName);
            if (btnTransform != null)
            {
                Button btn = btnTransform.GetComponent<Button>();
                if (btn != null)
                {
                    btn.interactable = interactable;
                }
                else
                {
                    Debug.LogWarning($"Button-Komponente fehlt bei '{btnName}'.");
                }
            }
            else
            {
                Debug.LogWarning($"Button mit Namen '{btnName}' wurde nicht gefunden.");
            }
        }
    }
}