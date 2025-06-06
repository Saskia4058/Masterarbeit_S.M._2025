// Urheber Soundeffekt (Klickgeräusch):
// Sound Effect by <a href="https://pixabay.com/de/users/u_8g40a9z0la-45586904/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=234708">u_8g40a9z0la</a> from <a href="https://pixabay.com//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=234708">Pixabay</a> 

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MultiSelectButtons : MonoBehaviour
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
    private HashSet<Button> selectedButtons = new HashSet<Button>();

    private Coroutine nextButtonHideCoroutine;

    private HashSet<string> triggerButtonNames = new HashSet<string> {
        "ButtonBrA", "ButtonEBA", "ButtonEFZ", "ButtonBM1", "ButtonBM2",
        "ButtonWMS", "ButtonFMS", "ButtonFaMa", "ButtonGYM", "ButtonPasserelle",
        "ButtonPraxis", "ButtonBP", "ButtonHFP", "ButtonHF", "ButtonNDS",
        "ButtonKurs", "ButtonUNI", "ButtonPH", "ButtonFH", "ButtonCAS", "ButtonPHD"
    };

    // === Event für andere Scripte ===
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

        if (selectedButtons.Contains(button))
        {
            selectedButtons.Remove(button);
            ApplyColorState(button, buttonData.normalColor, buttonData.highlightedColor);
        }
        else
        {
            selectedButtons.Add(button);
            ApplyColorState(button, buttonData.selectedColor, buttonData.highlightedColor);
        }

        OnButtonSelectionChanged?.Invoke();

        // Sichtbarkeit vom ButtonNext steuern
        if (!buttonNext.gameObject.activeSelf && triggerButtonNames.Contains(button.name))
        {
            buttonNext.gameObject.SetActive(true);
        }

        bool anyTriggerSelected = false;
        foreach (var btn in selectedButtons)
        {
            if (triggerButtonNames.Contains(btn.name))
            {
                anyTriggerSelected = true;
                break;
            }
        }

        if (!anyTriggerSelected && buttonNext.gameObject.activeSelf)
        {
            buttonNext.gameObject.SetActive(false);
        }
    }

    void ApplyColorState(Button button, Color baseColor, Color highlightedColor)
    {
        var colors = button.colors;
        colors.normalColor = baseColor;
        colors.highlightedColor = highlightedColor;
        colors.pressedColor = baseColor;
        colors.selectedColor = baseColor;
        colors.disabledColor = new Color(baseColor.r * 0.5f, baseColor.g * 0.5f, baseColor.b * 0.5f, 0.5f);
        button.colors = colors;
    }

    public bool IsButtonSelected(Button button)
    {
        return selectedButtons.Contains(button);
    }

    public List<Button> GetSelectedButtons()
    {
        return new List<Button>(selectedButtons);
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
