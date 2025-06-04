using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SpeechBubbleAgain : MonoBehaviour
{
    public TextMeshProUGUI dialogText;

    [TextArea(3, 10)]
    public string[] sentences;
    public AudioClip[] audioClips;

    public float delayBetweenLetters = 0.05f;
    public float delayBetweenSentences = 1.5f;

    private int currentSentenceIndex = 0;
    private Coroutine dialogueCoroutine;

    [Header("Ja/Nein Button Einstellungen")]
    public Button buttonYes;
    public Button buttonNo;
    public Button buttonWeiter;

    private bool isFirstRun = true;

    [Header("Speech Bubble Wechsel Einstellungen")]
    public GameObject speechBubbleExplain;
    public GameObject speechBubbleAgain;
    public GameObject speechBubbleFalse;

    private AudioSource audioSource;

    [Header("Andere Speech Bubble")]
    public GameObject speechBubbleExercise;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (speechBubbleExercise != null)
        {
            AudioSource otherAudio = speechBubbleExercise.GetComponent<AudioSource>();
            if (otherAudio != null && otherAudio.isPlaying)
            {
                otherAudio.Stop();
            }

            speechBubbleExercise.SetActive(false);
        }

        if (buttonYes != null)
        {
            buttonYes.gameObject.SetActive(false);
            buttonYes.onClick.AddListener(OnButtonYesClicked);
        }

        if (buttonNo != null)
        {
            buttonNo.gameObject.SetActive(false);
            buttonNo.onClick.AddListener(OnButtonNoClicked);
        }

        if (buttonWeiter != null)
        {
            buttonWeiter.gameObject.SetActive(false);
            buttonWeiter.onClick.AddListener(OnButtonWeiterClicked);
        }

        if (speechBubbleExplain != null)
        {
            speechBubbleExplain.SetActive(false);
        }

        if (speechBubbleAgain != null)
        {
            speechBubbleAgain.SetActive(false);
        }

        if (speechBubbleFalse != null)
        {
            speechBubbleFalse.SetActive(false);
        }

        dialogueCoroutine = StartCoroutine(DisplaySentences());
    }

    IEnumerator DisplaySentences()
    {
        while (currentSentenceIndex < sentences.Length)
        {
            PlayAudioForSentence(currentSentenceIndex);
            yield return StartCoroutine(TypeSentence(sentences[currentSentenceIndex]));
            currentSentenceIndex++;
            yield return new WaitForSeconds(delayBetweenSentences);
        }

        if (isFirstRun)
        {
            ActivateButtons();
            isFirstRun = false;
        }

        ResetButtonColors();
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(delayBetweenLetters);
        }
    }

    public void ReplayDialogue()
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
        }

        if (audioSource != null)
        {
            audioSource.Stop();
        }

        currentSentenceIndex = 0;
        dialogueCoroutine = StartCoroutine(DisplaySentences());
    }

    private void PlayAudioForSentence(int index)
    {
        if (audioSource != null && audioClips != null && index < audioClips.Length)
        {
            audioSource.Stop();
            audioSource.clip = audioClips[index];
            audioSource.Play();
        }
    }

    private void ResetButtonColors()
    {
        if (buttonYes != null)
        {
            ColorBlock colors = buttonYes.colors;
            colors.normalColor = new Color32(0xFF, 0xED, 0x68, 0xFF);
            colors.highlightedColor = new Color32(0xB3, 0xA6, 0x49, 0xFF);
            colors.pressedColor = new Color32(0xB3, 0xA6, 0x49, 0xFF);
            colors.selectedColor = new Color32(0xFF, 0xED, 0x68, 0xFF);
            colors.disabledColor = new Color32(0xB3, 0xA6, 0x49, 0xFF);
            buttonYes.colors = colors;
        }

        if (buttonNo != null)
        {
            ColorBlock colors = buttonNo.colors;
            colors.normalColor = new Color32(0xFF, 0xED, 0x68, 0xFF);
            colors.highlightedColor = new Color32(0xB3, 0xA6, 0x49, 0xFF);
            colors.pressedColor = new Color32(0xB3, 0xA6, 0x49, 0xFF);
            colors.selectedColor = new Color32(0xFF, 0xED, 0x68, 0xFF);
            colors.disabledColor = new Color32(0xB3, 0xA6, 0x49, 0xFF);
            buttonNo.colors = colors;
        }
    }

    private void ActivateButtons()
    {
        if (buttonYes != null) buttonYes.gameObject.SetActive(true);
        if (buttonNo != null) buttonNo.gameObject.SetActive(true);
        if (buttonWeiter != null) buttonWeiter.gameObject.SetActive(true);
    }

    private void OnButtonYesClicked()
    {
        if (speechBubbleExplain != null) speechBubbleExplain.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void OnButtonNoClicked()
    {
        if (speechBubbleFalse != null) speechBubbleFalse.SetActive(false);
        if (speechBubbleAgain != null) speechBubbleAgain.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void OnButtonWeiterClicked()
    {
        if (speechBubbleExercise != null) speechBubbleExercise.SetActive(true);
        this.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        if (audioSource != null) audioSource.Stop();
    }
}