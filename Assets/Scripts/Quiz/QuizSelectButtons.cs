// Urheber Soundeffekt (Klickgeräusch):
// Sound Effect by <a href="https://pixabay.com/de/users/u_8g40a9z0la-45586904/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=234708">u_8g40a9z0la</a> from <a href="https://pixabay.com//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=234708">Pixabay</a> 

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class QuizSelectButtons : MonoBehaviour
{
    [System.Serializable]
    public class ButtonColorSet
    {
        public Button button;
        public Color normalColor;
        public Color highlightedColor;
        public Color selectedColor;
    }

    public List<ButtonColorSet> buttons = new List<ButtonColorSet>();
    public AudioClip clickSound;
    public Button buttonNext;

    private AudioSource audioSource;
    private Button selectedButton = null;

    private Coroutine nextButtonHideCoroutine;

    private HashSet<string> triggerButtonNames = new HashSet<string> {
        "ButtonAnswer1", "ButtonAnswer2", "ButtonAnswer3", "ButtonAnswer4"
    };

    // === NEU: Event für andere Scripte ===
    public delegate void ButtonSelectionChanged();
    public event ButtonSelectionChanged OnButtonSelectionChanged;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        foreach (var b in buttons)
        {
            Button capturedButton = b.button;
            b.button.onClick.AddListener(() => ToggleButton(capturedButton));
            ApplyColorState(b.button, b.normalColor, b.highlightedColor);
        }

        // === NEU: Listener für ButtonNext hinzufügen ===
        if (buttonNext != null)
        {
            buttonNext.onClick.AddListener(OnNextButtonClicked);
        }
    }

    void ToggleButton(Button button)
    {
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        var buttonData = buttons.Find(b => b.button == button);
        if (buttonData == null) return;

        // Deselektiere den aktuell ausgewählten Button (falls vorhanden)
        if (selectedButton != null && selectedButton != button)
        {
            var previousButtonData = buttons.Find(b => b.button == selectedButton);
            ApplyColorState(selectedButton, previousButtonData.normalColor, previousButtonData.highlightedColor);
        }

        // Setze den neuen ausgewählten Button
        selectedButton = button;
        ApplyColorState(button, buttonData.selectedColor, buttonData.highlightedColor);

        // === NEU: Event auslösen ===
        OnButtonSelectionChanged?.Invoke();

        // Sichtbarkeit vom ButtonNext steuern
        if (triggerButtonNames.Contains(button.name))
        {
            buttonNext.gameObject.SetActive(true);
        }
    }

    void ApplyColorState(Button button, Color baseColor, Color highlightedColor)
    {
        var colors = button.colors;
        colors.normalColor = baseColor;
        colors.highlightedColor = highlightedColor;
        colors.pressedColor = baseColor;
        colors.selectedColor = baseColor;
        colors.disabledColor = new Color(baseColor.r * 0.5f, baseColor.g * 0.5f, baseColor.b * 0.5f, 0.5f); // Optional
        button.colors = colors;
    }

    // === NEU: Öffentliche Abfrage, ob Button selektiert ist ===
    public bool IsButtonSelected(Button button)
    {
        return selectedButton == button;
    }

    public Button GetSelectedButton()
    {
        return selectedButton;
    }

    // === NEU: ButtonNext wird nach 2 Sekunden deaktiviert, nachdem er geklickt wurde ===
    private void OnNextButtonClicked()
    {
        if (nextButtonHideCoroutine != null)
        {
            StopCoroutine(nextButtonHideCoroutine);
        }
        nextButtonHideCoroutine = StartCoroutine(DisableNextButtonAfterDelay(2f));
    }

    private IEnumerator DisableNextButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (buttonNext != null && buttonNext.gameObject.activeSelf)
        {
            buttonNext.gameObject.SetActive(false);
        }

        nextButtonHideCoroutine = null;
    }
}