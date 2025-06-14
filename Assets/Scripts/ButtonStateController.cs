using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonStateController : MonoBehaviour
{
    private Button replayButton;
    private GameObject speechBubbles;

    public GameObject speechBubbleExplain; // Im Inspector zuweisen

    private bool hasNextBeenClicked = false;

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

        Button buttonNext = GameObject.Find("ButtonNext")?.GetComponent<Button>();
        if (buttonNext != null)
        {
            buttonNext.onClick.AddListener(() => hasNextBeenClicked = true);
        }
    }

    void Update()
    {
        // Wenn Lösung gerade angezeigt wird, keine Button-Steuerung durch dieses Script
        if (speechBubbleExplain != null && speechBubbleExplain.activeSelf)
        {
            return;
        }

        if (hasNextBeenClicked)
        {
            DisableSelectableButtonsWithoutGreyingOut();
            return;
        }

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

    private void DisableSelectableButtonsWithoutGreyingOut()
    {
        foreach (Button btn in Object.FindObjectsByType<Button>(FindObjectsSortMode.None))
        {
            if (excludedButtons.Contains(btn.name))
                continue;

            var cb = btn.colors;

            bool isSelected = UnityEngine.EventSystems.EventSystem.current != null &&
                              UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == btn.gameObject;

            Color visibleColor = isSelected ? cb.selectedColor : cb.normalColor;

            // Setze alle relevanten Farbzustände auf die aktuelle sichtbare Farbe,
            // damit Buttons optisch unverändert bleiben, obwohl deaktiviert
            cb.normalColor = visibleColor;
            cb.highlightedColor = visibleColor;
            cb.pressedColor = visibleColor;
            cb.selectedColor = visibleColor;
            cb.disabledColor = visibleColor;

            btn.colors = cb;

            btn.interactable = false;
        }
    }
}