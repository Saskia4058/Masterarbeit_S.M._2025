using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GYMTask : MonoBehaviour, ILevelTask
{
    public MultiSelectButtons multiSelectButtons;

    public Button buttonGYM;
    public Button buttonUNI;
    public Button buttonNext;

    public GameObject speechBubbleTrue;
    public GameObject speechBubbleFalse;
    public GameObject speechBubbleFalse2;

    void Start()
    {
        if (buttonNext != null)
        {
            buttonNext.onClick.AddListener(OnNextClicked);
        }

        if (speechBubbleTrue != null) speechBubbleTrue.SetActive(false);
        if (speechBubbleFalse != null) speechBubbleFalse.SetActive(false);
    }

    void OnNextClicked()
    {
        if (multiSelectButtons == null)
            return;

        List<Button> selected = multiSelectButtons.GetSelectedButtons();

        bool containsGYM = selected.Contains(buttonGYM);
        bool containsUNI = selected.Contains(buttonUNI);

        bool isExactlyTheseTwoSelected =
            selected.Count == 2 &&
            containsGYM && containsUNI;

        if (speechBubbleTrue != null) speechBubbleTrue.SetActive(false);
        if (speechBubbleFalse != null) speechBubbleFalse.SetActive(false);
        if (speechBubbleFalse2 != null) speechBubbleFalse2.SetActive(false);

        if (isExactlyTheseTwoSelected)
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
                public List<Button> GetCorrectButtons()
    {
        return new List<Button> { buttonGYM, buttonUNI };
    }
}