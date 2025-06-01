using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SceneManagement; // <-- NEU hinzugefügt

public class QuizSpeechBubbleTrue4 : MonoBehaviour
{
    public TextMeshProUGUI dialogText;

    [TextArea(3, 10)]
    public string[] sentences;
    public AudioClip[] audioClips;

    public float delayBetweenLetters = 0.05f;
    public float delayBetweenSentences = 1.5f;

    private int currentSentenceIndex = 0;
    private Coroutine dialogueCoroutine;

    [Header("Weiter Button Einstellungen")]
    public Button weiterButton;

    private bool isFirstRun = true;

    [Header("Speech Bubble Wechsel Einstellungen")]
    public GameObject speechBubbleExplain;

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

        if (weiterButton != null)
        {
            weiterButton.gameObject.SetActive(false);
            weiterButton.onClick.AddListener(OnWeiterButtonClicked);
        }

        if (speechBubbleExplain != null)
        {
            speechBubbleExplain.SetActive(false);
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
            ActivateWeiterButton();
            isFirstRun = false;
        }

        ResetWeiterButtonColor();
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

    private void ResetWeiterButtonColor()
    {
        if (weiterButton != null)
        {
            ColorBlock colors = weiterButton.colors;
            colors.normalColor = new Color32(0xFF, 0xED, 0x68, 0xFF);
            colors.highlightedColor = new Color32(0xB3, 0xA6, 0x49, 0xFF);
            colors.pressedColor = new Color32(0xB3, 0xA6, 0x49, 0xFF);
            colors.selectedColor = new Color32(0xFF, 0xED, 0x68, 0xFF);
            colors.disabledColor = new Color32(0xB3, 0xA6, 0x49, 0xFF);
            weiterButton.colors = colors;
        }
    }

    private void ActivateWeiterButton()
    {
        if (weiterButton != null)
        {
            weiterButton.gameObject.SetActive(true);
        }
    }

    private void OnWeiterButtonClicked()
    {
        SceneManager.LoadScene("Level 5"); // <-- ANPASSUNG: Szene "Quiz2" laden
    }

    void OnDisable()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
