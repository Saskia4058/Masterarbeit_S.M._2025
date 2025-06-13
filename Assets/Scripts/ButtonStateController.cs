using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonStateController : MonoBehaviour
{
    private Button replayButton;
    private GameObject speechBubbles;

    private readonly HashSet<string> excludedButtons = new()
    {
        "ButtonWeiter", "ButtonHome", "ButtonYes", "ButtonNo", "ButtonGoOn", "ButtonFin"
    };

    void Start()
    {
        speechBubbles = GameObject.Find("Content/SpeechBubbles");
        if (speechBubbles == null) return;

        Transform sbExercise = speechBubbles.transform.Find("SpeechBubbleExercise");
        if (sbExercise != null)
        {
            Transform rb = sbExercise.Find("ButtonReplay");
            if (rb != null)
            {
                replayButton = rb.GetComponent<Button>();
            }
        }
    }

    void Update()
    {
        if (replayButton == null || speechBubbles == null) return;

        bool isReplayActive = replayButton.gameObject.activeInHierarchy;

        bool isAnyBubbleActive =
            speechBubbles.transform.Find("SpeechBubbleTrue")?.gameObject.activeInHierarchy == true ||
            speechBubbles.transform.Find("SpeechBubbleFalse")?.gameObject.activeInHierarchy == true ||
            speechBubbles.transform.Find("SpeechBubbleFalse2")?.gameObject.activeInHierarchy == true ||
            speechBubbles.transform.Find("SpeechBubbleAgain")?.gameObject.activeInHierarchy == true;

        bool shouldBeInteractable = isReplayActive && !isAnyBubbleActive;

        foreach (Button btn in Object.FindObjectsByType<Button>(FindObjectsSortMode.None))
        {
            if (excludedButtons.Contains(btn.name))
                continue;

            btn.interactable = shouldBeInteractable;
        }
    }
}