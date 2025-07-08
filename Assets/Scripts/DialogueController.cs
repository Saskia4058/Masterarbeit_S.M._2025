using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI dialogText;

    [TextArea(3, 10)]
    public string[] sentences;
    public AudioClip[] audioClips; // <--- NEU: Array mit AudioClips (pro Satz)

    public float delayBetweenLetters = 0.05f;
    public float delayBetweenSentences = 1.5f;

    public GameObject ExerciseInteractionBlocker;

    private int currentSentenceIndex = 0;
    private Coroutine dialogueCoroutine;

    [Header("Replay Button Einstellungen")]
    public Button replayButton;

    private bool isFirstRun = true;
    private bool isPlaying = false;

    private AudioSource audioSource;

    [Header("Klickgeräusch")]
    public AudioClip clickSound;                      // Das Klickgeräusch bei Replay-Button
    public AudioSource clickAudioSource;              // Separate AudioSource für Klicksound

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (replayButton != null)
        {
            replayButton.gameObject.SetActive(false);
            replayButton.onClick.AddListener(OnReplayButtonClicked);
        }
        dialogueCoroutine = StartCoroutine(DisplaySentences());
    }

    void Update()
    {
        if (isPlaying)
        {
            ExerciseInteractionBlocker.SetActive(true);
        }
        else
        {
            ExerciseInteractionBlocker.SetActive(false);
        }
    }

    IEnumerator DisplaySentences()
    {
        isPlaying = true;
        SetReplayButtonPressedColor();

        while (currentSentenceIndex < sentences.Length)
        {
            PlayAudioForSentence(currentSentenceIndex); // <-- NEU: passenden Clip starten
            yield return StartCoroutine(TypeSentence(sentences[currentSentenceIndex]));
            yield return new WaitForSeconds(delayBetweenSentences);
            currentSentenceIndex++;
        }

        isPlaying = false;
        if (isFirstRun)
        {
            ActivateReplayButton();
            isFirstRun = false;
        }

        ResetReplayButtonColor();
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
            audioSource.Stop(); // <-- NEU: Audio stoppen beim Replay
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

    private void ResetReplayButtonColor()
    {
        if (replayButton != null)
        {
            ColorBlock colors = replayButton.colors;
            colors.normalColor = new Color32(0xFF, 0xED, 0x68, 0xFF);
            colors.highlightedColor = new Color32(0xB3, 0xA6, 0x49, 0xFF);
            colors.pressedColor = new Color32(0xB3, 0xA6, 0x49, 0xFF);
            colors.selectedColor = new Color32(0xFF, 0xED, 0x68, 0xFF);
            colors.disabledColor = new Color32(0xB3, 0xA6, 0x49, 0xFF);
            replayButton.colors = colors;

            EventSystem.current.SetSelectedGameObject(null);
            replayButton.OnDeselect(null);
        }
    }

    private void SetReplayButtonPressedColor()
    {
        if (replayButton != null)
        {
            ColorBlock colors = replayButton.colors;
            colors.normalColor = new Color32(0xB3, 0xA6, 0x49, 0xFF);
            colors.highlightedColor = new Color32(0xB3, 0xA6, 0x49, 0xFF);
            colors.pressedColor = new Color32(0xB3, 0xA6, 0x49, 0xFF);
            colors.selectedColor = new Color32(0xB3, 0xA6, 0x49, 0xFF);
            colors.disabledColor = new Color32(0xB3, 0xA6, 0x49, 0xFF);
            replayButton.colors = colors;
        }
    }

    private void ActivateReplayButton()
    {
        if (replayButton != null)
        {
            replayButton.gameObject.SetActive(true);
        }
    }

    private void OnReplayButtonClicked()
    {
        PlayClickSound(); // <--- Klickgeräusch abspielen

        if (!isPlaying)
        {
            ReplayDialogue();
        }
    }

    private void PlayClickSound()
    {
        if (clickAudioSource != null && clickSound != null)
        {
            clickAudioSource.PlayOneShot(clickSound);
        }
    }
}