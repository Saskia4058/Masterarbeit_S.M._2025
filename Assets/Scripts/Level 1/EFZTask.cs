using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EFZTask : MonoBehaviour
{
    public MultiSelectButtons multiSelectButtons;
    public Button buttonEFZ;
    public Button buttonNext;
    public GameObject speechBubbleTrue;
    public GameObject speechBubbleFalse;
    public GameObject speechBubbleFalse2;  // NEU: Zweite "falsche" SpeechBubble

    void Start()
    {
        if (buttonNext != null)
        {
            buttonNext.onClick.AddListener(OnNextClicked);
        }

        if (speechBubbleTrue != null) speechBubbleTrue.SetActive(false);
        if (speechBubbleFalse != null) speechBubbleFalse.SetActive(false);
        if (speechBubbleFalse2 != null) speechBubbleFalse2.SetActive(false);
    }

    void OnNextClicked()
    {
        if (multiSelectButtons == null || buttonEFZ == null)
            return;

        List<Button> selected = multiSelectButtons.GetSelectedButtons();

        bool isOnlyEFZSelected = (selected.Count == 1 && selected.Contains(buttonEFZ));

        // Alle SpeechBubbles ausblenden
        if (speechBubbleTrue != null) speechBubbleTrue.SetActive(false);
        if (speechBubbleFalse != null) speechBubbleFalse.SetActive(false);
        if (speechBubbleFalse2 != null) speechBubbleFalse2.SetActive(false);

        if (isOnlyEFZSelected)
        {
            if (speechBubbleTrue != null) speechBubbleTrue.SetActive(true);
        }
        else
        {
            string currentLevel = SceneManager.GetActiveScene().name;
            bool restarted = LevelRestartTracker.WasLevelRestarted(currentLevel);

            if (restarted)
            {
                if (speechBubbleFalse2 != null) speechBubbleFalse2.SetActive(true);
            }
            else
            {
                if (speechBubbleFalse != null) speechBubbleFalse.SetActive(true);
            }
        }
    }
}